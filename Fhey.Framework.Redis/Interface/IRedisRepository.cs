using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fhey.Framework.Redis.Interface
{
    public interface IRedisRepository : IDisposable
    {
       IRedisConfiguration Configuration { get; set; }

        string KeyPrefix { get; set; }

        void Open();
        void Close();
        T Get<T>(string key);
        bool Set<T>(string key, T value);
        bool Set<T>(string key, T value, DateTime expiresAt);
        bool Remove(string key);
        bool Expire(string key, DateTime expiresAt);
        bool Expire(string key, double timeInSeconds);
        bool Expire(string key, TimeSpan timeSpan);
        object Eval(string script, string[] keyArgs, object[] valueArgs);
        bool Exists(string key);
    }
}
