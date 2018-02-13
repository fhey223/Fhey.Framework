using System;
using System.Collections;
using System.Web;

namespace Fhey.Framework.Uility.Cache
{

    public class HttpCache: CacheBase
    {
        protected System.Web.Caching.Cache _Cache;

        public HttpCache()
        {
            _Cache = HttpRuntime.Cache;
        }

        public override object Get(string key)
        {
            return _Cache.Get(key);
        }

        public override void Set(string key, object value)
        {
            if (null == value) return;
            _Cache.Insert(key, value, null, DateTime.Now.AddMinutes(30), System.Web.Caching.Cache.NoSlidingExpiration);
        }

        public override void Set(string key, object value, DateTime expirationTime)
        {
            if (null == value) return;
            _Cache.Insert(key, value, null, expirationTime, System.Web.Caching.Cache.NoSlidingExpiration);
        }

        public override bool Exists(string key)
        {
            return null != Get(key);
        }

        public override object Get(string key, Type type)
        {
            throw new NotImplementedException();
        }

        public override void Set(string key, object value, Type type)
        {
            throw new NotImplementedException();
        }

        public override void Set(string key, object value, DateTime expirationTime, Type type)
        {
            throw new NotImplementedException();
        }

        public override T Get<T>(string key)
        {
            T result = default(T);
            object value = Get(key);
            if (null != value)
            {
                result = (T)value;
            }
            return result;
        }

        public override void Set<T>(string key, T value)
        {
            throw new NotImplementedException();
        }

        public override void Set<T>(string key, T value, DateTime expirationTime)
        {
            if (null == value) return;
            _Cache.Insert(key, value, null, expirationTime, System.Web.Caching.Cache.NoSlidingExpiration);
        }

        public override void Remove(string key)
        {
            _Cache.Remove(key);
        }

        public override bool Expire(string key, DateTime expiresAt)
        {
            throw new NotImplementedException();
        }

        public override void Clear()
        {
            IDictionaryEnumerator iDictionaryEnumerator = HttpRuntime.Cache.GetEnumerator();
            while (iDictionaryEnumerator.MoveNext())
            {
                _Cache.Remove(Convert.ToString(iDictionaryEnumerator.Key));
            }
        }
    }
}
