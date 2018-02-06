using Fhey.Framework.Redis.Interface;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Fhey.Framework.Redis
{
    /// <summary>
    ///  Redis有序集合(sorted set)操作
    /// </summary>
    public class RedisSortedSetRepository : RedisRepositoryBase
    {
        public RedisSortedSetRepository(string connectionString = null, IRedisConfiguration configuration = null) : base(connectionString, configuration) { }

        #region 同步方法

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="score"></param>
        public bool Set<T>(string key, T value, double score) => _conn.SortedSetAdd(key, JsonConvert.SerializeObject(value), score);

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public bool Remove<T>(string key, T value) => _conn.SortedSetRemove(key, JsonConvert.SerializeObject(value));

        /// <summary>
        /// 获取全部
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public List<T> SortedSetRangeByRank<T>(string key)
        {
            var values = _conn.SortedSetRangeByRank(key);
            return values as List<T>;
        }

        /// <summary>
        /// 获取集合中的数量
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public long GetSortedSetLength(string key) => _conn.SortedSetLength(key);

        #endregion 同步方法

        #region 异步方法

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="score"></param>
        public async Task<bool> SetAsync<T>(string key, T value, double score)
            => await _conn.SortedSetAddAsync(key, JsonConvert.SerializeObject(value), score);

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public async Task<bool> RemoveAsync<T>(string key, T value) 
            => await _conn.SortedSetRemoveAsync(key, JsonConvert.SerializeObject(value));

        /// <summary>
        /// 获取全部
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public async Task<List<T>> GetAsync<T>(string key)
        {
            var values = await _conn.SortedSetRangeByRankAsync(key);
            return values as List<T>;
        }

        /// <summary>
        /// 获取集合中的数量
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public async Task<long> GetSortedSetLengthAsync(string key) => await _conn.SortedSetLengthAsync(key);

        #endregion 异步方法
    }
}
