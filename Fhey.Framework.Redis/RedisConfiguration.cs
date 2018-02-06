using Fhey.Framework.Redis.Interface;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fhey.Framework.Redis
{
    internal class RedisConfiguration : ConfigurationSection, IRedisConfiguration
    {
        public const string Redis_Config = "redisConfig";
        public const string Read_Write_Hosts = "readWriteHosts";
        public const string Read_Only_Hosts = "readOnlyHosts";
        public const string Max_Write_Pool_Size = "maxWritePoolSize";
        public const string Max_Read_Pool_Size = "maxReadPoolSize";
        public const string Auto_Start = "autoStart";
        public const string Local_Cache_Time = "localCacheTime";
        public const string Recorde_Log = "recordeLog";
        public const string End_Points = "endPoints";
        public const string Client_Name = "clientName";
        public const string Allow_Admin = "allowAdmin";
        public const string Retry_TimeOut_MilliSeconds = "retryTotalMillisecond";
        public const string Key_Prefix = "keyPrefix";
        public const string SSL = "ssl";

        public static RedisConfiguration Config
        {
            get
            {
                RedisConfiguration section = (RedisConfiguration)ConfigurationManager.GetSection(Redis_Config);
                if (null == section)
                {
                    throw new ConfigurationErrorsException("Section " + Redis_Config + " is not found.");
                }
                return section;
            }
        }

        [ConfigurationProperty(End_Points, IsRequired = false)]
        public string EndPoint
        {
            get { return this[End_Points].ToString(); }
        }

        [ConfigurationProperty(Read_Write_Hosts, IsRequired = false)]
        public string ReadWriteHost
        {
            get { return this[Read_Write_Hosts].ToString(); }
        }

        [ConfigurationProperty(Read_Only_Hosts, IsRequired = false)]
        public string ReadOnlyHost
        {
            get { return this[Read_Only_Hosts].ToString(); }
        }

        [ConfigurationProperty(Client_Name, IsRequired = true)]
        public string ClientName
        {
            get { return this[Client_Name] as string; }
        }
        [ConfigurationProperty(Max_Write_Pool_Size, IsRequired = false, DefaultValue = 5)]
        public int MaxWritePoolSize
        {
            get
            {
                int _maxWritePoolSize = (int)this[Max_Write_Pool_Size];
                return _maxWritePoolSize > 0 ? _maxWritePoolSize : 5;
            }
        }

        [ConfigurationProperty(Max_Read_Pool_Size, IsRequired = false, DefaultValue = 5)]
        public int MaxReadPoolSize
        {
            get
            {
                int _maxReadPoolSize = (int)this[Max_Read_Pool_Size];
                return _maxReadPoolSize > 0 ? _maxReadPoolSize : 5;
            }
        }

        [ConfigurationProperty(Auto_Start, IsRequired = false, DefaultValue = true)]
        public bool AutoStart
        {
            get
            {
                return (bool)this[Auto_Start];
            }
        }

        [ConfigurationProperty(Allow_Admin, IsRequired = false, DefaultValue = false)]
        public bool AllowAdmin
        {
            get
            {
                return (bool)this[Allow_Admin];
            }
        }

        [ConfigurationProperty(Local_Cache_Time, IsRequired = false, DefaultValue = 36000)]
        public int LocalCacheTime
        {
            get
            {
                return (int)this[Local_Cache_Time];
            }
        }

        [ConfigurationProperty(Recorde_Log, IsRequired = false, DefaultValue = false)]
        public bool RecordeLog
        {
            get
            {
                return (bool)this[Recorde_Log];
            }
        }

        [ConfigurationProperty(Retry_TimeOut_MilliSeconds, IsRequired = false)]
        public int RetryTimeOut
        {
            get { return (int)this[Retry_TimeOut_MilliSeconds]; }
        }

        [ConfigurationProperty(Key_Prefix, IsRequired = false)]
        public string KeyPrefix
        {
            get { return (string)this[Key_Prefix]; }
        }

        public string[] EndPoints
        {
            get { return EndPoint.Split(','); }
        }

        public string[] ReadWriteHosts
        {
            get { return ReadOnlyHost.Split(','); }
        }

        public string[] ReadOnlyHosts
        {
            get { return ReadOnlyHost.Split(','); }
        }

        [ConfigurationProperty(SSL, IsRequired = false, DefaultValue = false)]
        public bool Ssl
        {
            get
            {
                return (bool)this[SSL];
            }
        }
    }
}
