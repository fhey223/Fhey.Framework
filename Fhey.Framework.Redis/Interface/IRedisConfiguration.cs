using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fhey.Framework.Redis.Interface
{
    public interface IRedisConfiguration
    {
        string[] EndPoints { get; }
        string[] ReadWriteHosts { get; }
        string[] ReadOnlyHosts { get; }
        string ClientName { get; }
        int MaxWritePoolSize { get; }
        int MaxReadPoolSize { get; }
        bool AutoStart { get; }
        bool AllowAdmin { get; }
        int LocalCacheTime { get; }
        bool RecordeLog { get; }
        int RetryTimeOut { get; }
        string KeyPrefix { get; }
        bool Ssl { get; }
    }
}
