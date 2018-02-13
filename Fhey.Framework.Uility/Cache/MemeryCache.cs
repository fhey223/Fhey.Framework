using System;
using System.Collections.Generic;

namespace Fhey.Framework.Uility.Cache
{
    public class MemeryCache : CacheBase
    {
        protected Dictionary<string, object> _Cache;

        public MemeryCache()
        {
            _Cache = new Dictionary<string, object>();
        }

        public override object Get(string key)
        {
            return _Cache[key];
        }

        public override void Set<T>(string key, T value)
        {
            if (Exists(key))
            {
                _Cache[key] = value;
            }
            else
            {
                _Cache.Add(key, value);
            }
        }
        public override void Set(string key, object value, DateTime expirationTime)
        {
            Set(key, value);
        }


        public override void Set<T>(string key, T value, DateTime expirationTime)
        {
            Set(key, value);
        }
        public override bool Exists(string key)
        {
            return _Cache.ContainsKey(key);
        }

        public override void Set(string key, object value)
        {
            if (Exists(key))
            {
                _Cache[key] = value;
            }
            else
            {
                _Cache.Add(key, value);
            }
        }

        public override void Remove(string key)
        {
            _Cache.Remove(key);
        }
    }
}
