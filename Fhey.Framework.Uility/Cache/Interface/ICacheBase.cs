using System;

namespace Fhey.Framework.Uility.Cache.Interface
{
    public interface ICacheBase
    {
        object Get(string key);
        void Set(string key, object value);
        void Set(string key, object value, DateTime expirationTime);
        bool Exists(string key);

        object Get(string key, Type type);
        void Set(string key, object value, Type type);
        void Set(string key, object value, DateTime expirationTime, Type type);

        T Get<T>(string key);
        void Set<T>(string key, T value);
        void Set<T>(string key, T value, DateTime expirationTime);

        void Remove(string key);
        bool Expire(string key, DateTime expiresAt);

        void Clear();
    }
}
