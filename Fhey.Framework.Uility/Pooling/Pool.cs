using System;
using System.Collections.Concurrent;
using System.Threading;

namespace Fhey.Framework.Uility.Pooling
{
    public class Pool<T> : IPool<T> where T : class, IDisposable
    {
        protected readonly IPoolConfiguration<T> _config;
        protected readonly ConcurrentQueue<T> _objectQueue;
        protected readonly ReaderWriterLockSlim _locker;
        protected readonly AutoResetEvent _objectAutoResetEvent;

        public Pool(IPoolConfiguration<T> config)
        {
            if (config.ObjectFactory == null)
            {
                throw new InvalidOperationException("The object factory cannot be instantiated.");
            }
            if (config.MaxSize > 0 && config.MaxSize < config.InitialSize)
            {
                throw new ArgumentException("The maximum size of the pool shall not be smaller than its initial size.");
            }
            if (string.IsNullOrEmpty(config.Key))
            {
                throw new ArgumentException("The key is invalid.");
            }
            if (config.InitialSize < 0)
            {
                throw new ArgumentException("The pool initial size is invalid");
            }
            if (config.MaxSize != -1 && config.MaxSize < config.InitialSize)
            {
                throw new ArgumentException("The pool max size is invalid");
            }
            _config = config;

            _objectAutoResetEvent = new AutoResetEvent(false);
            _locker = new ReaderWriterLockSlim();
            _objectQueue = new ConcurrentQueue<T>();
            for (var i = 0; i < config.InitialSize; i++)
            {
                _objectQueue.Enqueue(_config.ObjectFactory.Create());
            }
        }

        public virtual void Put(T obj)
        {
            _locker.EnterUpgradeableReadLock();
            try
            {
                _locker.EnterWriteLock();
                try
                {
                    _objectQueue.Enqueue(obj);
                }
                finally
                {
                    _locker.ExitWriteLock();
                }
            }
            finally
            {
                _locker.ExitUpgradeableReadLock();
            }
            _objectAutoResetEvent.Set();
        }

        public virtual T Get()
        {
            T obj;
            var spin = new SpinWait();
            while (!TryGet(-1, out obj))
            {
                spin.SpinOnce();
            }
            return obj;
        }

        public int Size { get { return _objectQueue.Count; } }

        public IPoolConfiguration<T> Config { get { return _config; } }

        protected virtual bool TryGet(int timeout, out T obj)
        {
            var reault = false;
            var localTimeout = timeout < 0 ? -1 : timeout;
            reault = _objectQueue.TryPeek(out obj);
            if (!reault)
            {
                _locker.EnterWriteLock();
                try
                {
                    if (_config.MaxSize == -1 || Size < _config.MaxSize)
                    {
                        obj = _config.ObjectFactory.Create();
                        _objectQueue.Enqueue(obj);
                        reault = true;
                    }
                }
                finally
                {
                    _locker.ExitWriteLock();
                }
                if (!reault)
                {
                    if (_objectAutoResetEvent.WaitOne(localTimeout))
                    {
                        _locker.EnterWriteLock();
                        try
                        {
                            reault = _objectQueue.TryPeek(out obj);
                        }
                        finally
                        {
                            _locker.ExitWriteLock();
                        }
                    }
                }
            }

            return reault;
        }

        public virtual void Dispose()
        {
            _locker.EnterWriteLock();
            try
            {
                T obj;
                while (_objectQueue.TryDequeue(out obj))
                {
                    _config.ObjectFactory.Destroy(obj);
                }
                _objectAutoResetEvent.Dispose();
            }
            finally
            {
                _locker.ExitWriteLock();
            }
        }
    }
}
