using System;
using System.Collections.Concurrent;

namespace Fhey.Framework.Uility.Pooling
{
    public class PoolRepository : IPoolRepository
    {
        protected ConcurrentDictionary<string, IDisposable> _pools;

        public PoolRepository()
        {
            _pools = new ConcurrentDictionary<string, IDisposable>();
        }

        public IPool<T> Create<T>(IPoolConfiguration<T> config) where T : class , IDisposable
        {
            if (null!=config && string.IsNullOrEmpty(config.Key))
            {
                throw new ArgumentException("The key is invalid.");
            }
            IPool<T> pool = Get<T>(config.Key);
            if (null == pool)
            {
                pool = new Pool<T>(config);
            }
            return pool;
        }

        public IPool<T> Get<T>(string key) where T : class , IDisposable
        {
            IDisposable pool = null;
            if (_pools.TryGetValue(key, out pool))
            {
                return (IPool<T>)pool;
            }
            return null;
        }

        public void Remove(string key)
        {
            if (string.IsNullOrWhiteSpace(key))
            {
                throw new ArgumentException("The key is invalid.");
            }

            IDisposable pool;
            if (!_pools.TryRemove(key, out pool))
            {
                throw new ArgumentException("The key does not represent a valid object pool.");
            }
            pool.Dispose();
        }

        public void Dispose()
        {
            foreach (var pool in _pools)
            {
                if (null != pool.Value)
                {
                    pool.Value.Dispose();
                }
            }
            _pools.Clear();
        }
    }
}
