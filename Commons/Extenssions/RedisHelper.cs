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

        public Task<bool> IsKeyExistAsync(string key, TimeSpan expiry)
        {
            return db.KeyExpireAsync(key, expiry);
        }

        public bool Expiry(string key, TimeSpan? expiry = default(TimeSpan?))
        {
            return db.KeyExpire(key, expiry);
        }

        public Task ExpiryAsync(string key, TimeSpan? expiry = default(TimeSpan?))
        {
            return db.KeyExpireAsync(key, expiry);
        }

        public bool ExpiryNoWait(string key, TimeSpan? expiry = default(TimeSpan?))
        {
            return db.KeyExpire(key, expiry, flags: CommandFlags.PreferMaster);
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
        public long GetHashCount(string key)
        {
            return db.HashLength(key);
        }
        public Task<long> GetHashCountAsync(string key)
        {
            return db.HashLengthAsync(key);
        }

        public bool AddHashValue(string key, string hashkey, string value)
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
        public bool AddHashValue<T>(String key, string hashkey, T t)
        {
            var json = JsonConvert.SerializeObject(t);
            return db.HashSet(key, hashkey, json);
        }

        public Task<bool> AddHashValueAsync<T>(String key, string hashkey, T t, TimeSpan? expiry = default(TimeSpan?))
        {
            var json = JsonConvert.SerializeObject(t);
           
            var ret = db.HashSetAsync(key, hashkey, json);
            if (expiry != null)
            {
                ExpiryNoWait(key, expiry);
            }
            
            return ret;
        }

        public void AddHashValueNoWait<T>(String key, string hashkey, T t)
        { 
            var json = JsonConvert.SerializeObject(t);
            db.HashSet(key, hashkey, json, flags: CommandFlags.FireAndForget);
        }

        public void AddHashList<T>(string key, Dictionary<string, T> dic)
        {
            List<HashEntry> listHashEntry = new List<HashEntry>();
            foreach (var item in dic)
            {
                string json = JsonConvert.SerializeObject(item.Value);
                listHashEntry.Add(new HashEntry(item.Key, json));
            }
            db.HashSet(key, listHashEntry.ToArray());
        }

        public async Task AddHashListAsync<Tkey, T>(string key, Dictionary<Tkey, T> dic, TimeSpan? expiry = default(TimeSpan?))
        {
            List<HashEntry> listHashEntry = new List<HashEntry>();
            foreach (var item in dic)
            {
                string hashkey = JsonConvert.SerializeObject(item.Key);
                string json = JsonConvert.SerializeObject(item.Value);
                listHashEntry.Add(new HashEntry(hashkey, json));
            }
            
            await db.HashSetAsync(key, listHashEntry.ToArray());
            ExpiryNoWait(key, expiry);
        }

        public void AddHashList<T>(string key, List<T> list, Func<T, string> getModelId)
        {
            List<HashEntry> listHashEntry = new List<HashEntry>();
            foreach (var item in list)
            {
                string json = JsonConvert.SerializeObject(item);
                listHashEntry.Add(new HashEntry(getModelId(item), json));
            }
            db.HashSet(key, listHashEntry.ToArray());
        }

        public Task AddHashListAsync<T>(string key, List<T> list, Func<T, string> getModelId)
        {
            List<HashEntry> listHashEntry = new List<HashEntry>();
            foreach (var item in list)
            {
                string json = JsonConvert.SerializeObject(item);
                listHashEntry.Add(new HashEntry(getModelId(item), json));
            }
            return db.HashSetAsync(key, listHashEntry.ToArray());
       
        }

        /// <summary>
        /// 获取hashkey所有的值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        public List<T> GetHashAllValue<T>(string key)
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

        public async Task<List<T>> GetHashAllValueAsync<T>(string key)
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

        public async Task<Dictionary<TKey, TValue>> GetHashAllAsync<TKey, TValue>(string key)
        {
            Dictionary<TKey, TValue> result = new Dictionary<TKey, TValue>();
            HashEntry[] arr = await db.HashGetAllAsync(key);

            foreach (var item in arr)
            {
                if (!item.Value.IsNullOrEmpty)
                {
                    TKey tk = JsonConvert.DeserializeObject<TKey>(item.Name);
                    TValue tv = JsonConvert.DeserializeObject<TValue>(item.Value);
                    result.Add(tk, tv);
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

        public Task DeleteHashValuesAsync(string key, IEnumerable<string> hashkeys)
        {
            return db.HashDeleteAsync(key, hashkeys.Select(x => (RedisValue)x).ToArray());
        }

        public void DeleteHashValueNoWait(string key, string hashkey)
        {
             db.HashDelete(key, hashkey, flags: CommandFlags.FireAndForget);
        }

        #endregion


        #region zset类型操作
      
        public bool AddZsetValue(string key, string zsetkey, double score, TimeSpan? expiry = default(TimeSpan?))
        {
            bool ret = db.SortedSetAdd(key, zsetkey, score);
            db.KeyExpire(key, expiry);
            return ret;
        }


        public async Task<bool> AddZsetValueAsync(String key, string zsetkey, double score, TimeSpan? expiry = default(TimeSpan?))
        {
            bool ret = await db.SortedSetAddAsync(key, zsetkey, score);
            await db.KeyExpireAsync(key, expiry);
            return ret;
        }

        public void AddZsetValueNoWait(String key, string zsetkey, double score)
        {
            db.HashSet(key, zsetkey, score, flags: CommandFlags.FireAndForget);
        }


        public List<KeyValuePair<string, double>> GetZsetAll (string key)
        {
            List<KeyValuePair<string, double>> result = new List<KeyValuePair<string, double>>();
            var arr = db.SortedSetRangeByRankWithScores(key, 0, -1);

            foreach (var item in arr)
            {
                result.Add(new KeyValuePair<string, double>(item.Element, item.Score));
            }
            return result;

        }

        public async Task<List<KeyValuePair<string, double>>> GetZsetAllAsync(string key)
        {
            List<KeyValuePair<string, double>> result = new List<KeyValuePair<string, double>>();
            var arr = await db.SortedSetRangeByRankWithScoresAsync(key, 0, -1);

            foreach (var item in arr)
            {
                result.Add(new KeyValuePair<string, double>(item.Element, item.Score));
            }
            return result;

        }


        public async Task<List<string>> GetZsetAllKeyAsync(string key)
        {
            List<string> result = new List<string>();
            var arr = await db.SortedSetRangeByRankWithScoresAsync(key, 0, -1);

            foreach (var item in arr)
            {
                result.Add(item.Element);
            }
            return result;

        }

        public double? GetZsetValue(string key, string zsetkey)
        {
            return db.SortedSetScore(key, zsetkey);
        }

        public Task<double?>GetZsetValueAsync(string key, string zsetkey)
        {
            return  db.SortedSetScoreAsync(key, zsetkey);

        }

        public long DeleteZsetValueRange(string key, double minScore, double maxScore)
        {
            return db.SortedSetRemoveRangeByScore(key, minScore, maxScore);
        }

        public Task<long> DeleteZsetValueRangeAsync(string key, double minScore, double maxScore)
        {
            return db.SortedSetRemoveRangeByScoreAsync(key, minScore, maxScore);
        }

        public async Task<List<string>> DeleteZsetReturnValueRangeAsync(string key, 
            double minScore, 
            double maxScore)
        {
            var deletedKey = await db.SortedSetRangeByScoreAsync(key, minScore, maxScore);
            await db.SortedSetRemoveRangeByScoreAsync(key, minScore, maxScore);
            return deletedKey.Select(x => x.ToString()).ToList();
        }

        public bool DeleteZsetValue(string key, string zsetkey)
        {
            return db.SortedSetRemove(key, zsetkey);
        }

        public Task<bool> DeleteZsetValueAsync(string key, string zsetkey)
        {
            return db.SortedSetRemoveAsync(key, zsetkey);
        }

        public void DeleteZsetValueNoWait(string key, string zsetkey)
        {
            db.SortedSetRemove(key, zsetkey, flags: CommandFlags.FireAndForget);
        }

        #endregion


        #region set类型操作
        
        public long GetSetCount(string key)
        {
            return db.SetLength(key);
        }
        public Task<long> GetSetCountAsync(string key)
        {
            return db.SetLengthAsync(key);
        }

        public bool AddSetValue(string key, string value)
        {
            return db.SetAdd(key, value);
        }

        public async Task<bool> AddSetValueAsync(string key, string value, TimeSpan? expiry = default(TimeSpan?))
        {
            var ret = await db.SetAddAsync(key, value);
            ExpiryNoWait(key, expiry);
            return ret;
        }


        public bool AddSetValue<T>(String key, T t)
        {
            var json = JsonConvert.SerializeObject(t);
            return db.SetAdd(key, json);
        }

        public Task<bool> AddSetValueAsync<T>(String key, T t)
        {
            var json = JsonConvert.SerializeObject(t);
            return db.SetAddAsync(key, json);
        }

        public void AddSetValueNoWait<T>(String key, T t)
        {
            var json = JsonConvert.SerializeObject(t);
            db.SetAdd(key, json, flags: CommandFlags.FireAndForget);
        }

        
        public void AddSetList<T>(string key, List<T> list, Func<T, string> getModelId)
        {
            List<RedisValue> listSet = new List<RedisValue>();
            foreach (var item in list)
            {
                string json = JsonConvert.SerializeObject(item);
                listSet.Add(json);
            }
            db.SetAdd(key, listSet.ToArray(), CommandFlags.FireAndForget);
        }

        public Task AddSetListAsync<T>(string key, List<T> list, Func<T, string> getModelId)
        {
            List<RedisValue> listSet = new List<RedisValue>();
            foreach (var item in list)
            {
                string str = getModelId(item);
                listSet.Add(str);
            }
            return db.SetAddAsync(key, listSet.ToArray(), CommandFlags.FireAndForget);
          
        }

        public async Task AddSetListAsync<T>(string key, List<T> list, TimeSpan? expiry = default(TimeSpan?))
        {
            List<RedisValue> listSet = new List<RedisValue>();
            foreach (var item in list)
            {
                string json = JsonConvert.SerializeObject(item);
                listSet.Add(json);
            }
            await db.SetAddAsync(key, listSet.ToArray());
            ExpiryNoWait(key, expiry);
        }

        public Task AddSetListAsync(string key, List<string> list)
        {
            List<RedisValue> listSet = new List<RedisValue>();
            foreach (var item in list)
            {
                listSet.Add(item);
            }
            return db.SetAddAsync(key, listSet.ToArray(), CommandFlags.FireAndForget);
        }

        public List<T> GetSetAllValue<T>(string key)
        {
            List<T> result = new List<T>();
            RedisValue[] arr = db.SetMembers(key);
            foreach (var item in arr)
            {
                
                try
                {
                    T t = JsonConvert.DeserializeObject<T>(item.ToString());
                    result.Add(t);
                }
                catch (Exception)
                {

                }

                
            }
            return result;

        }



        public async Task<List<T>> GetSetAllValueAsync<T>(string key)
        {
            List<T> result = new List<T>();
            RedisValue[] arr = await db.SetMembersAsync(key);
            foreach (var item in arr)
            {
                try
                {
                    T t = JsonConvert.DeserializeObject<T>(item.ToString());
                    result.Add(t);
                }
                catch (Exception)
                {

                }
            }
            return result;
        }


        public bool IsSetContains(string key, string value)
        {
            return db.SetContains(key, value);
        }

        public async Task<bool> IsSetContainsAsync(string key, string value)
        {
            return await db.SetContainsAsync(key, value);

        }

       
        public bool IsSetContains<T>(string key, T value)
        {
            string strvalue = JsonConvert.SerializeObject(value);
            return db.SetContains(key, strvalue);

        }

        public async Task<bool> IsSetContainsAsync<T>(string key, T value)
        {
            string strvalue = JsonConvert.SerializeObject(value);
            return await db.SetContainsAsync(key, strvalue);
           

        }

        public bool DeleteSetValue(string key, string value)
        {
            return db.SetRemove(key, value);
        }

        public Task<bool> DeleteSetValueAsync(string key, string value)
        {
            return db.SetRemoveAsync(key, value);
        }

        public void DeleteSetValueNoWait(string key, string value)
        {
            db.SetRemove(key, value, flags: CommandFlags.FireAndForget);
        }

        #endregion
    }
}
