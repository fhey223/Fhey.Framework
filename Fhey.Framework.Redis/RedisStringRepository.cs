using Fhey.Framework.Redis.Interface;
using Newtonsoft.Json;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fhey.Framework.Redis
{
    /// <summary>
    /// Redis字符串(String)操作
    /// </summary>
    public class RedisStringRepository: RedisRepositoryBase
    {
        public RedisStringRepository(string connectionString = null, IRedisConfiguration configuration = null) : base(connectionString, configuration){}

        /// <summary>
        /// Key是否存在
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public override bool Exists(string key)
        {
            return _conn.KeyExists(key);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public override bool Remove(string key)
        {
            return _conn.KeyDelete(key);
        }

        #region 同步方法
        /// <summary>
        /// 获取单个key的值
        /// </summary>
        /// <param name="key">Redis Key</param>
        /// <returns></returns>
        public string Get(string key) => _conn.StringGet(key);

        /// <summary>
        /// 获取多个Key
        /// </summary>
        /// <param name="listKey">Redis Key集合</param>
        /// <returns></returns>
        public RedisValue[] Get(List<string> listKey) => _conn.StringGet(listKey.Select(redisKey => (RedisKey)redisKey).ToArray());

        /// <summary>
        /// 获取一个key指定类型的对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        public T Get<T>(string key) => JsonConvert.DeserializeObject<T>(_conn.StringGet(key));

        /// <summary>
        /// 保存单个key value
        /// </summary>
        /// <param name="key">Redis Key</param>
        /// <param name="value">保存的值</param>
        /// <param name="expiry">过期时间</param>
        /// <returns></returns>
        public bool Set(string key, string value, DateTime? expiresTime = default(DateTime?)) => _conn.StringSet(key, value, expiresTime - DateTime.Now);

        /// 保存单个key value
        /// </summary>
        /// <param name="key">Redis Key</param>
        /// <param name="value">保存的值</param>
        /// <param name="expiry">过期时间</param>
        /// <returns></returns>
        public bool Set(string key, string value, TimeSpan? expiry = default(TimeSpan?))=> _conn.StringSet(key, value, expiry);

        /// <summary>
        /// 保存多个key value
        /// </summary>
        /// <param name="keyValues">键值对</param>
        /// <returns></returns>
        public bool Set(List<KeyValuePair<RedisKey, RedisValue>> keyValues) => _conn.StringSet(keyValues.ToArray());

        /// <summary>
        /// 保存单个对象
        /// </summary>
        /// <param name="key">Redis Key</param>
        /// <param name="value">保存的值</param>
        /// <param name="expiry">过期时间</param>
        /// <returns></returns>
        public bool Set<T>(string key, T value, DateTime? expiresTime = default(DateTime?)) => _conn.StringSet(key, JsonConvert.SerializeObject(value), expiresTime - DateTime.Now);

        /// <summary>
        /// 保存单个对象
        /// </summary>
        /// <param name="key">Redis Key</param>
        /// <param name="value">保存的值</param>
        /// <param name="expiry">过期时间</param>
        /// <returns></returns>
        public bool Set<T>(string key, T value, TimeSpan? expiry = default(TimeSpan?)) => _conn.StringSet(key, JsonConvert.SerializeObject(value), expiry);
        #endregion 同步方法

        #region 异步方法

        /// <summary>
        /// 保存单个key value
        /// </summary>
        /// <param name="key">Redis Key</param>
        /// <param name="value">保存的值</param>
        /// <param name="expiry">过期时间</param>
        /// <returns></returns>
        public async Task<bool> SetAsync(string key, string value, TimeSpan? expiry = default(TimeSpan?)) => await _conn.StringSetAsync(key, value, expiry);

        /// <summary>
        /// 保存单个key value
        /// </summary>
        /// <param name="key">Redis Key</param>
        /// <param name="value">保存的值</param>
        /// <param name="expiry">过期时间</param>
        /// <returns></returns>
        public async Task<bool> SetAsync(string key, string value, DateTime? expiresTime = default(DateTime?)) => await _conn.StringSetAsync(key, value, expiresTime - DateTime.Now);

        /// <summary>
        /// 保存多个key value
        /// </summary>
        /// <param name="keyValues">键值对</param>
        /// <returns></returns>
        public async Task<bool> SetAsync(List<KeyValuePair<RedisKey, RedisValue>> keyValues) => await _conn.StringSetAsync(keyValues.ToArray());

        /// <summary>
        /// 保存单个对象
        /// </summary>
        /// <param name="key">Redis Key</param>
        /// <param name="value">保存的值</param>
        /// <param name="expiresTime">过期时间</param>
        /// <returns></returns>
        public async Task<bool> SetAsync<T>(string key, T value) => await _conn.StringSetAsync(key, JsonConvert.SerializeObject(value));

        /// <summary>
        /// 保存单个对象
        /// </summary>
        /// <param name="key">Redis Key</param>
        /// <param name="value">保存的值</param>
        /// <param name="expiresTime">过期时间</param>
        /// <returns></returns>
        public async Task<bool> SetAsync<T>(string key, T value, DateTime? expiresTime = default(DateTime?)) => await _conn.StringSetAsync(key, JsonConvert.SerializeObject(value), expiresTime - DateTime.Now);

        /// <summary>
        /// 保存单个对象
        /// </summary>
        /// <param name="key">Redis Key</param>
        /// <param name="value">保存的值</param>
        /// <param name="expiry">过期时间</param>
        /// <returns></returns>
        public async Task<bool> SetAsync<T>(string key, T value, TimeSpan? expiry = default(TimeSpan?)) => await _conn.StringSetAsync(key, JsonConvert.SerializeObject(value), expiry);

        /// <summary>
        /// 获取单个key的值
        /// </summary>
        /// <param name="key">Redis Key</param>
        /// <returns></returns>
        public async Task<string> GetAsync(string key) => await _conn.StringGetAsync(key);

        /// <summary>
        /// 获取多个Key
        /// </summary>
        /// <param name="listKey">Redis Key集合</param>
        /// <returns></returns>
        public async Task<RedisValue[]> GetAsync(List<string> listKey) => await _conn.StringGetAsync(listKey.Select(redisKey => (RedisKey)redisKey).ToArray());

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

        #endregion 异步方法
    }
}
