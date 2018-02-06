using System.Collections.Generic;
using System.Threading.Tasks;
using Fhey.Framework.Redis.Interface;
using Newtonsoft.Json;
using StackExchange.Redis;

namespace Fhey.Framework.Redis
{
    /// <summary>
    /// Redis哈希(Hash)操作
    /// </summary>
    public class RedisHashRepository :RedisRepositoryBase
    {
        public RedisHashRepository(string connectionString = null, IRedisConfiguration configuration = null) : base(connectionString, configuration) { }

        #region 同步方法
        /// <summary>
        /// 判断某个数据是否已经被缓存
        /// </summary>
        /// <param name="key"></param>
        /// <param name="dataKey"></param>
        /// <returns></returns>
        public bool Exists(string key, string dataKey) => _conn.HashExists(key, dataKey);

        /// <summary>
        /// 从hash表获取数据
        /// </summary>
        /// <param name="key"></param>
        /// <param name="dataKey"></param>
        /// <returns></returns>
        public string Get(string key, string dataKey) => _conn.HashGet(key, dataKey);

        /// <summary>
        /// 从hash表获取对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="dataKey"></param>
        /// <returns></returns>
        public T Get<T>(string key, string dataKey)
        {
            var value = _conn.HashGet(key, dataKey);
            return JsonConvert.DeserializeObject<T>((value));
        }

        /// <summary>
        /// 获取hashkey所有Redis key
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        public async Task<List<T>> KeysAsync<T>(string key)
        {
            RedisValue[] values = await _conn.HashKeysAsync(key);
            return values as List<T>;
        }

        /// <summary>
        /// 存储数据到hash表
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="dataKey"></param>
        /// <param name="t"></param>
        /// <returns></returns>
        public bool Set(string key, string dataKey, string value) => _conn.HashSet(key, dataKey, value);

        /// <summary>
        /// 存储对象到hash表
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="dataKey"></param>
        /// <param name="t"></param>
        /// <returns></returns>
        public bool Set<T>(string key, string dataKey, T value)
        {
            string json = JsonConvert.SerializeObject(value);
            return _conn.HashSet(key, dataKey, json);
        }

        /// <summary>
        /// 移除hash中的某值
        /// </summary>
        /// <param name="key"></param>
        /// <param name="dataKey"></param>
        /// <returns></returns>
        public bool Delete(string key, string dataKey) => _conn.HashDelete(key, dataKey);

        /// <summary>
        /// 移除hash中的多个值
        /// </summary>
        /// <param name="key"></param>
        /// <param name="dataKeys"></param>
        /// <returns></returns>
        public long Delete(string key, List<RedisValue> dataKeys) => _conn.HashDelete(key, dataKeys.ToArray());

        #endregion 同步方法

        #region 异步方法
        /// <summary>
        /// 判断某个数据是否已经被缓存
        /// </summary>
        /// <param name="key"></param>
        /// <param name="dataKey"></param>
        /// <returns></returns>
        public async Task<bool> ExistsAsync(string key, string dataKey) => await _conn.HashExistsAsync(key, dataKey);


        /// <summary>
        /// 获取单个key的值
        /// </summary>
        /// <param name="key">Redis Key</param>
        /// <returns></returns>
        public async Task<string> GetAsync(string key, string dataKey)
        {
            return await _conn.HashGetAsync(key, dataKey);
        }

        /// <summary>
        /// 获取单个key的对象
        /// </summary>
        /// <param name="key">Redis Key</param>
        /// <returns></returns>
        public async Task<T> GetAsync<T>(string key, string dataKey)
        {
            string value = await _conn.HashGetAsync(key, dataKey);
            return JsonConvert.DeserializeObject<T>(value);
        }


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
        /// <param name="expiry">过期时间</param>
        /// <returns></returns>
        public async Task<bool> SetAsync<T>(string key, string dataKey, T value)
        {
            string json = JsonConvert.SerializeObject(value);
            return await _conn.HashSetAsync(key, dataKey, json);
        }


        /// <summary>
        /// 移除hash中的某值
        /// </summary>
        /// <param name="key"></param>
        /// <param name="dataKey"></param>
        /// <returns></returns>
        public async Task<bool> HashDeleteAsync(string key, string dataKey) => await _conn.HashDeleteAsync(key, dataKey);

        /// <summary>
        /// 移除hash中的多个值
        /// </summary>
        /// <param name="key"></param>
        /// <param name="dataKeys"></param>
        /// <returns></returns>
        public async Task<long> HashDeleteAsync(string key, List<RedisValue> dataKeys) => await _conn.HashDeleteAsync(key, dataKeys.ToArray());

        #endregion 异步方法
    }
}
