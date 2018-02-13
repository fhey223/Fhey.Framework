using Fhey.Framework.Uility.Cache.Interface;
using System;

namespace Fhey.Framework.Uility.Cache
{
    public abstract class CacheBase : ICacheBase
    {
        public virtual object Get(string key)
        {
            throw new NotImplementedException();
        }

        public virtual object DoGet(string key)
        {
            throw new NotImplementedException();
        }

        public virtual void Set(string key, object value)
        {
            throw new NotImplementedException();
        }

        public virtual void Set(string key, object value, DateTime expirationTime)
        {
            throw new NotImplementedException();
        }

        public virtual bool Exists(string key)
        {
            throw new NotImplementedException();
        }

        public virtual object Get(string key, Type type)
        {
            return Get(key);
        }

        public virtual void Set(string key, object value, Type type)
        {
            Set(key, value);
        }

        public virtual void Set(string key, object value, DateTime expirationTime, Type type)
        {
            Set(key, value, expirationTime);
        }

        public virtual T Get<T>(string key)
        {
            throw new NotImplementedException();
        }

        public virtual void Set<T>(string key, T value)
        {
            throw new NotImplementedException();
        }

        public virtual void Set<T>(string key, T value, DateTime expirationTime)
        {
            throw new NotImplementedException();
        }

        public virtual void Remove(string key)
        {
            throw new NotImplementedException();
        }

        public virtual bool Expire(string key, DateTime expiresAt)
        {
            throw new NotImplementedException();
        }

        public virtual void Clear()
        {
            throw new NotImplementedException();
        }
    }
}
