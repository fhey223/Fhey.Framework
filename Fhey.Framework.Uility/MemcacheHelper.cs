using Memcached.ClientLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class MemcacheHelper 
    {
        private readonly MemcachedClient client;
        public MemcacheHelper()
        {
            string[] ips = System.Configuration.ConfigurationManager.AppSettings["MemcachedServers"].Split(',');
            //初始化池
            SockIOPool pool = SockIOPool.GetInstance();
            pool.SetServers(ips);
            //pool.InitConnections = 3;
            //pool.MinConnections = 3;
            //pool.MaxConnections = 5;
            //pool.SocketConnectTimeout = 1000;
            //pool.SocketTimeout = 3000;
            //pool.MaintenanceSleep = 30;
            //pool.Failover = true;
            //pool.Nagle = false;
            pool.Initialize();

            //获取客户端实例
            client = new MemcachedClient();
            client.EnableCompression = true;
        }

        /// <summary>
        /// 向Memcache存储数据
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public  void Set(string key, object value)
        {
            client.Set(key, value);
        }
        public void Set(string key, object value, DateTime expiryTime)
        {
            client.Set(key, value, expiryTime);
        }
        /// <summary>
        /// 获取Memcache中的数据
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public object Get(string key)
        {
            return client.Get(key);
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public bool Delete(string key)
        {
            if (client.KeyExists(key)) return client.Delete(key);
            return false;
        }
    }
}
