using Fhey.Framework.Redis.Interface;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Fhey.Framework.Redis
{
    public abstract class RedisRepositoryBase
    {
        protected readonly IDatabase _conn;

        #region 构造函数
        public RedisRepositoryBase(string connectionString = null, IRedisConfiguration redisConfiguration = null)
        {

            _conn = RedisProvider.Instance(connectionString, redisConfiguration);
        }
        #endregion 构造函数

        /// <summary>
        /// 关闭Redis连接
        /// </summary>
        public void Close()
        {
            _conn.Multiplexer.Close();
        }

        /// <summary>
        /// 销毁Redis连接
        /// </summary>
        protected virtual void Dispose()
        {
            if (null != _conn)
            {
                _conn.Multiplexer.Close();
                _conn.Multiplexer.Dispose();
                GC.SuppressFinalize(this);
            }
        }

        #region key

        /// <summary>
        /// 删除单个key
        /// </summary>
        /// <param name="key">redis key</param>
        /// <returns>是否删除成功</returns>
        public virtual bool Remove(string key) => _conn.KeyDelete(key);

        /// <summary>
        /// 删除多个key
        /// </summary>
        /// <param name="keys">rediskey</param>
        /// <returns>成功删除的个数</returns>
        public virtual long Remove(List<string> keys) => _conn.KeyDelete(keys.Select(redisKey => (RedisKey)redisKey).ToArray());

        /// <summary>
        /// 判断key是否存储
        /// </summary>
        /// <param name="key">redis key</param>
        /// <returns></returns>
        public virtual bool Exists(string key) => _conn.KeyExists(key);

        /// <summary>
        /// 重新命名key
        /// </summary>
        /// <param name="key">就的redis key</param>
        /// <param name="newKey">新的redis key</param>
        /// <returns></returns>
        public bool KeyRename(string key, string newKey) => _conn.KeyRename(key, newKey);


        /// <summary>
        /// 设置Key的时间
        /// </summary>
        /// <param name="key"></param>
        /// <param name="timeSpan"></param>
        /// <returns></returns>
        public virtual bool Expire(string key, DateTime expiresTime)
        {
            return _conn.KeyExpire(key, expiresTime);
        }

        /// <summary>
        /// 设置Key的时间
        /// </summary>
        /// <param name="key"></param>
        /// <param name="timeSpan"></param>
        /// <returns></returns>
        public virtual bool Expire(string key, double timeInSeconds)
        {
            return Expire(key, DateTime.Now.AddSeconds(timeInSeconds));
        }

        /// <summary>
        /// 设置Key的时间
        /// </summary>
        /// <param name="key"></param>
        /// <param name="timeSpan"></param>
        /// <returns></returns>
        public virtual bool Expire(string key, TimeSpan timeSpan)
        {
            return _conn.KeyExpire(key, timeSpan);
        }
        #endregion key
    }
}
