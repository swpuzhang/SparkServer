using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StackExchange.Redis;
using Newtonsoft.Json;


namespace Commons.Extenssions
{

    public class RedisHelper
    {
        private ConnectionMultiplexer redis { get; set; }
        private IDatabase db { get; set; }
        public RedisHelper(string connection)
        {
            redis = ConnectionMultiplexer.Connect(connection);
            db = redis.GetDatabase();
           
        }

        #region 锁
        public bool LockTake(string key, string token, TimeSpan expireMs)
        {
            return db.LockTake(key, token, expireMs);
        }

        public Task<bool> LockTakeAsync(string key, string token, TimeSpan expireMs)
        {
            return db.LockTakeAsync(key, token, expireMs);
        }

        public void LockRelease(string key, string token)
        {
            db.LockRelease(key, token);
        }

        public async Task LockReleaseAsync(string key, string token)
        {
            await db.LockReleaseAsync(key, token);
   
        }

        #endregion

        #region key相关操作

        public bool IsKeyExist(string key)
        {
            return db.KeyExists(key);
        }

        public Task<bool> IsKeyExistAsync(string key)
        {
            return db.KeyExistsAsync(key);
        }

        #endregion 

        #region string类型操作
        public bool SetString(string key, string value, TimeSpan? expiry = null)
        {
            return db.StringSet(key, value, expiry);
        }

        public void SetStringNoWait(string key, string value, TimeSpan? expiry = null)
        {
            db.StringSet(key, value, expiry, flags: CommandFlags.FireAndForget);
        }

        public Task<bool> SetStringAsync(string key, string value, TimeSpan? expiry = null)
        {
            return db.StringSetAsync(key, value, expiry);
        }


        public bool SetString<T>(string key, T obj, TimeSpan? expiry = default(TimeSpan?)) 
        {
            string json = JsonConvert.SerializeObject(obj);
            return db.StringSet(key, json, expiry);
        }

        public Task<bool> SetStringAsync<T>(string key, T obj, TimeSpan? expiry = default(TimeSpan?))
        {
            string json = JsonConvert.SerializeObject(obj);
            return db.StringSetAsync(key, json, expiry);
        }

        public void SetStringNoWait<T>(string key, T obj, TimeSpan? expiry = default(TimeSpan?))
        {
            string json = JsonConvert.SerializeObject(obj);
            db.StringSet(key, json, expiry, flags: CommandFlags.FireAndForget);
        }

        /// <summary>
        /// 获取一个key的对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        public T GetString<T>(string key)
        {
            var result = db.StringGet(key);
            if (string.IsNullOrEmpty(result))
            {
                return default(T);
            }
            try
            {
                T t = JsonConvert.DeserializeObject<T>(result);
                return t;
            }
            catch (Exception )
            {
                return default(T);
            }

        }

        public async Task<T> GetStringAsync<T>(string key)
        {
            var result = await db.StringGetAsync(key);
            if (string.IsNullOrEmpty(result))
            {
                return default(T);
            }
            try
            {
                T t = JsonConvert.DeserializeObject<T>(result);
                return t;
            }
            catch (Exception)
            {
                return default(T);
            }

        }

        /// <summary>
        /// get the value for string key 
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public string GetString(string key)
        {
            return db.StringGet(key);
        }

        public async Task<string> GetStringAsync(string key)
        {
            return await db.StringGetAsync(key);
           
        }

        /// <summary>
        /// Delete the value for string key 
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public bool DeleteKey(string key)
        {
            return db.KeyDelete(key);
        }

        public Task<bool> DeleteKeyAsync(string key)
        {
            return db.KeyDeleteAsync(key);
        }

        public void DeleteKeyNoWait(string key)
        {
            db.KeyDelete(key, flags: CommandFlags.FireAndForget);
        }

        #endregion

        #region 哈希类型操作
        /// <summary>
        /// set or update the HashValue for string key 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="hashkey"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool SetHashValue(string key, string hashkey, string value)
        {
            return db.HashSet(key, hashkey, value);
        }

        /// <summary>
        /// set or update the HashValue for string key 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="hashkey"></param>
        /// <param name="t">defined class</param>
        /// <returns></returns>
        public bool SetHashValue<T>(String key, string hashkey, T t)
        {
            var json = JsonConvert.SerializeObject(t);
            return db.HashSet(key, hashkey, json);
        }

        public Task<bool> SetHashValueAsunc<T>(String key, string hashkey, T t)
        {
            var json = JsonConvert.SerializeObject(t);
            return db.HashSetAsync(key, hashkey, json);
        }

        public void SetHashValueNoWait<T>(String key, string hashkey, T t)
        { 
            var json = JsonConvert.SerializeObject(t);
            db.HashSet(key, hashkey, json, flags: CommandFlags.FireAndForget);
        }

        /// <summary>
        /// 保存一个集合
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key">Redis Key</param>
        /// <param name="list">数据集合</param>
        /// <param name="getModelId"></param>
        public void SetHashList<T>(string key, List<T> list, Func<T, string> getModelId)
        {
            List<HashEntry> listHashEntry = new List<HashEntry>();
            foreach (var item in list)
            {
                string json = JsonConvert.SerializeObject(item);
                listHashEntry.Add(new HashEntry(getModelId(item), json));
            }
            db.HashSet(key, listHashEntry.ToArray(), CommandFlags.FireAndForget);
        }

        public Task SetHashListAsync<T>(string key, List<T> list, Func<T, string> getModelId)
        {
            List<HashEntry> listHashEntry = new List<HashEntry>();
            foreach (var item in list)
            {
                string json = JsonConvert.SerializeObject(item);
                listHashEntry.Add(new HashEntry(getModelId(item), json));
            }
            db.HashSet(key, listHashEntry.ToArray());
            return Task.CompletedTask;
        }

        /// <summary>
        /// 获取hashkey所有的值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        public List<T> HashGetAll<T>(string key)
        {
            List<T> result = new List<T>();
            HashEntry[] arr = db.HashGetAll(key);
            foreach (var item in arr)
            {
                if (!item.Value.IsNullOrEmpty)
                {
                    try
                    {
                        T t = JsonConvert.DeserializeObject<T>(item.Value);
                        result.Add(t);
                    }
                    catch(Exception )
                    {
                        
                    }
                  
                }
            }
            return result;
            
        }

        public async Task<List<T>> HashGetAllAsync<T>(string key)
        {
            List<T> result = new List<T>();
            HashEntry[] arr = await db.HashGetAllAsync(key);
            foreach (var item in arr)
            {
                if (!item.Value.IsNullOrEmpty)
                {
                    try
                    {
                        T t = JsonConvert.DeserializeObject<T>(item.Value);
                        result.Add(t);
                    }
                    catch (Exception)
                    {

                    }

                }
            }
            return result;

        }

        /// <summary>
        /// get the HashValue for string key  and hashkey
        /// </summary>
        /// <param name="key">Represents a key that can be stored in redis</param>
        /// <param name="hashkey"></param>
        /// <returns></returns>
        public string GetHashValue(string key, string hashkey)
        {
            return db.HashGet(key, hashkey);
        }

        public async Task<string> GetHashValueAsync(string key, string hashkey)
        {
            return await db.HashGetAsync(key, hashkey);
            
        }

        /// <summary>
        /// get the HashValue for string key  and hashkey
        /// </summary>
        /// <param name="key">Represents a key that can be stored in redis</param>
        /// <param name="hashkey"></param>
        /// <returns></returns>
        public T GetHashValue<T>(string key, string hashkey)
        {
            string result = db.HashGet(key, hashkey);
            if (string.IsNullOrEmpty(result))
            {
                return default(T);
            }
            try
            {
                var t = JsonConvert.DeserializeObject<T> (result);
                return t;
            }
            catch(Exception)
            {
                return default(T);
            }
           
        }

        public async Task<T> GetHashValueAsync<T>(string key, string hashkey)
        {
            string result = await db.HashGetAsync(key, hashkey);
            if (string.IsNullOrEmpty(result))
            {
                return default(T);
            }
            try
            {
                var t = JsonConvert.DeserializeObject<T>(result);
                return t;
            }
            catch (Exception)
            {
                return default(T);
            }

        }

        /// <summary>
        /// delete the HashValue for string key  and hashkey
        /// </summary>
        /// <param name="key"></param>
        /// <param name="hashkey"></param>
        /// <returns></returns>
        public bool DeleteHashValue(string key, string hashkey)
        {
            return db.HashDelete(key, hashkey);
        }

        public Task<bool> DeleteHashValueAsync(string key, string hashkey)
        {
            return db.HashDeleteAsync(key, hashkey);
        }

        public void DeleteHashValueNoWait(string key, string hashkey)
        {
             db.HashDelete(key, hashkey, flags: CommandFlags.FireAndForget);
        }

        #endregion
    }
}
