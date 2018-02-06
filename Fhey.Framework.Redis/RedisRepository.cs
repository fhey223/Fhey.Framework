using Newtonsoft.Json;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace Fhey.Framework.Redis
{
    /// <summary>
    /// Redis操作（仅封装了String操作）
    /// </summary>
    public class RedisRepository
    {
        private int DbNum { get; }
        private readonly IDatabase _conn;
        public string CustomKey;

        #region 构造函数

        public RedisRepository() : this(null)
        {
        }


        public RedisRepository(string readWriteHosts)
        {
            _conn =RedisProvider.Instance 
                string.IsNullOrWhiteSpace(readWriteHosts) ?
                RedisProvider.Instance :
                RedisProvider.GetCacheConnection(readWriteHosts);
        }

        #endregion 构造函数

        #region 同步方法
        /// <summary>
        /// 获取单个key的值
        /// </summary>
        /// <param name="key">Redis Key</param>
        /// <returns></returns>
        public string Get(string key)
        {
            return _conn.StringGet(key);
        }

        /// <summary>
        /// 获取多个Key
        /// </summary>
        /// <param name="listKey">Redis Key集合</param>
        /// <returns></returns>
        public RedisValue[] Get(List<string> listKey)
        {
            return _conn.StringGet(listKey.Select(redisKey => (RedisKey)redisKey).ToArray());
        }

        /// <summary>
        /// 获取一个key指定类型的对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        public T Get<T>(string key)
        {
            return JsonConvert.DeserializeObject<T>(_conn.StringGet(key));
        }

        /// <summary>
        /// 保存单个key value
        /// </summary>
        /// <param name="key">Redis Key</param>
        /// <param name="value">保存的值</param>
        /// <param name="expiry">过期时间</param>
        /// <returns></returns>
        public bool Set(string key, string value, DateTime? expiresTime = default(DateTime?))
        {
            return _conn.StringSet(key, value, expiresTime - DateTime.Now);
        }

        /// 保存单个key value
        /// </summary>
        /// <param name="key">Redis Key</param>
        /// <param name="value">保存的值</param>
        /// <param name="expiry">过期时间</param>
        /// <returns></returns>
        public bool Set(string key, string value, TimeSpan? expiry = default(TimeSpan?))
        {
            return _conn.StringSet(key, value, expiry);
        }

        //public  bool Set<T>(string key, T value)
        //{
        //    return _conn.StringSet(key, JsonConvert.SerializeObject(value));
        //}


        /// <summary>
        /// 保存多个key value
        /// </summary>
        /// <param name="keyValues">键值对</param>
        /// <returns></returns>
        public bool Set(List<KeyValuePair<RedisKey, RedisValue>> keyValues)
        {
            return _conn.StringSet(keyValues.ToArray());
        }

        /// <summary>
        /// 保存单个对象
        /// </summary>
        /// <param name="key">Redis Key</param>
        /// <param name="value">保存的值</param>
        /// <param name="expiry">过期时间</param>
        /// <returns></returns>
        public bool Set<T>(string key, T value, DateTime? expiresTime = default(DateTime?))
        {
            return _conn.StringSet(key, JsonConvert.SerializeObject(value), expiresTime - DateTime.Now);
        }

        /// <summary>
        /// 保存单个对象
        /// </summary>
        /// <param name="key">Redis Key</param>
        /// <param name="value">保存的值</param>
        /// <param name="expiry">过期时间</param>
        /// <returns></returns>
        public bool Set<T>(string key, T value, TimeSpan? expiry = default(TimeSpan?))
        {
            return _conn.StringSet(key, JsonConvert.SerializeObject(value), expiry);
        }
        #endregion 同步方法

        #region 异步方法

        /// <summary>
        /// 保存单个key value
        /// </summary>
        /// <param name="key">Redis Key</param>
        /// <param name="value">保存的值</param>
        /// <param name="expiry">过期时间</param>
        /// <returns></returns>
        public async Task<bool> SetAsync(string key, string value, TimeSpan? expiry = default(TimeSpan?))
        {
            return await _conn.StringSetAsync(key, value, expiry);
        }

        /// <summary>
        /// 保存单个key value
        /// </summary>
        /// <param name="key">Redis Key</param>
        /// <param name="value">保存的值</param>
        /// <param name="expiry">过期时间</param>
        /// <returns></returns>
        public async Task<bool> SetAsync(string key, string value, DateTime? expiresTime = default(DateTime?))
        {
            return await _conn.StringSetAsync(key, value, expiresTime - DateTime.Now);
        }

        /// <summary>
        /// 保存多个key value
        /// </summary>
        /// <param name="keyValues">键值对</param>
        /// <returns></returns>
        public async Task<bool> SetAsync(List<KeyValuePair<RedisKey, RedisValue>> keyValues)
        {
            return await _conn.StringSetAsync(keyValues.ToArray());
        }


        /// <summary>
        /// 保存单个对象
        /// </summary>
        /// <param name="key">Redis Key</param>
        /// <param name="value">保存的值</param>
        /// <param name="expiresTime">过期时间</param>
        /// <returns></returns>
        public async Task<bool> SetAsync<T>(string key, T value, DateTime? expiresTime = default(DateTime?))
        {
            return await _conn.StringSetAsync(key, JsonConvert.SerializeObject(value), expiresTime - DateTime.Now);
        }

        /// <summary>
        /// 保存单个对象
        /// </summary>
        /// <param name="key">Redis Key</param>
        /// <param name="value">保存的值</param>
        /// <param name="expiry">过期时间</param>
        /// <returns></returns>
        public async Task<bool> SetAsync<T>(string key, T value, TimeSpan? expiry = default(TimeSpan?))
        {
            return await _conn.StringSetAsync(key, JsonConvert.SerializeObject(value), expiry);
        }

        /// <summary>
        /// 获取单个key的值
        /// </summary>
        /// <param name="key">Redis Key</param>
        /// <returns></returns>
        public async Task<string> GetAsync(string key)
        {
            return await _conn.StringGetAsync(key);
        }

        /// <summary>
        /// 获取多个Key
        /// </summary>
        /// <param name="listKey">Redis Key集合</param>
        /// <returns></returns>
        public async Task<RedisValue[]> GetAsync(List<string> listKey)
        {
            return await _conn.StringGetAsync(listKey.Select(redisKey => (RedisKey)redisKey).ToArray());
        }

        /// <summary>
        /// 获取一个key的对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        public async Task<T> GetAsync<T>(string key)
        {
            string result = await _conn.StringGetAsync(key);
            return JsonConvert.DeserializeObject<T>(result);
        }

        /// <summary>
        /// 为数字增长val
        /// </summary>
        /// <param name="key"></param>
        /// <param name="val">可以为负</param>
        /// <returns>增长后的值</returns>
        public async Task<double> StringIncrementAsync(string key, double val = 1)
        {
            return await _conn.StringIncrementAsync(key, val);
        }

        /// <summary>
        /// 为数字减少val
        /// </summary>
        /// <param name="key"></param>
        /// <param name="val">可以为负</param>
        /// <returns>减少后的值</returns>
        public async Task<double> StringDecrementAsync(string key, double val = 1)
        {
            return await _conn.StringDecrementAsync(key, val);
        }

        #endregion 异步方法

        /// <summary>
        /// 销毁Redis连接
        /// </summary>
        protected void Dispose()
        {
            if (null != _conn)
            {
                _conn.Multiplexer.Close();
                _conn.Multiplexer.Dispose();
            }
        }

        /// <summary>
        /// 关闭Redis连接
        /// </summary>
        public void Close()
        {
            _conn.Multiplexer.Close();
        }

        /// <summary>
        /// Key是否存在
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public bool Exists(string key)
        {
            return _conn.KeyExists(key);
        }
        
        /// <summary>
        /// 手动过期
        /// </summary>
        /// <param name="key"></param>
        /// <param name="timeSpan"></param>
        /// <returns></returns>
        public bool Expire(string key, TimeSpan timeSpan)
        {
            return _conn.KeyExpire(key, timeSpan);
        }

        /// <summary>
        /// 手动过期
        /// </summary>
        /// <param name="key"></param>
        /// <param name="expiresTime"></param>
        /// <returns></returns>
        public bool Expire(string key, DateTime expiresTime)
        {
            return _conn.KeyExpire(key, expiresTime);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public bool Remove(string key)
        {
            return _conn.KeyDelete(key);
        }
    }
}
