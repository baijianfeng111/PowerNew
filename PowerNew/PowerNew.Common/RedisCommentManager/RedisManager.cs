using System;
using System.Collections.Generic;
using ServiceStack.Redis;

namespace PowerNew.Common
{
    /// <summary>
    /// Redis通用类
    /// </summary>
    public class RedisManager
    {
        private static PooledRedisClientManager pooledredis;

        private static long db = 0;


        //设置操作的数据可
        public static void ChooseDb(long dbase = 0)
        {
            db = dbase;
        }

        static RedisManager()
        {
            RedisConfig redisConfig = RedisConfigPool.GetRedisConfig();
            List<string> readWriteHosts = new List<string>();
            List<string> readOnlyHosts = new List<string>();
            redisConfig.ReadWriteHosts.ForEach(delegate(RedisConfig.RedisServer p)
            {
                readWriteHosts.Add(p.Host);
            });
            redisConfig.ReadOnlyHosts.ForEach(delegate(RedisConfig.RedisServer p)
            {
                readOnlyHosts.Add(p.Host);
            });
            pooledredis = new PooledRedisClientManager(readWriteHosts.ToArray(), readOnlyHosts.ToArray(), redisConfig.RedisClientManagerConfig);
        }

        public static IRedisClient GetClient()
        {
            RedisConfig redisConfig = RedisConfigPool.GetRedisConfig();
            //IRedisClient client = new RedisClient();
            //if (redisConfig.ConnectTimeout > 0)
            //{
            //    client.ConnectTimeout = redisConfig.ConnectTimeout;
            //}
            //if (redisConfig.RetryCount > 0)
            //{
            //    client.RetryCount = redisConfig.RetryCount;
            //}
            //if (redisConfig.RetryTimeout > 0)
            //{
            //    client.RetryTimeout = redisConfig.RetryTimeout;
            //}
            //if (redisConfig.SendTimeout > 0)
            //{
            //    client.SendTimeout = redisConfig.SendTimeout;
            //}
            //client.Password = redisConfig.Password;
            //return client;
            var client = new RedisClient("127.0.0.1", 6379);//redis服务IP和端口
            client.Password = "123456";
            client.Db = db;
            return client;
        }

        public static bool SetListEntity<T>(string key, List<T> entities)
        {
            bool result;
            using (IRedisClient client = GetClient())
            {
                result = client.Set<List<T>>(key, entities);
            }
            return result;
        }

        /// <summary>
        /// key---List<T>---timeout
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="entities"></param>
        /// <param name="expiry"></param>
        /// <returns></returns>
        public static bool SetListEntity<T>(string key, List<T> entities, int expiry)
        {
            bool result;
            using (IRedisClient client = GetClient())
            {
                result = client.Set<List<T>>(key, entities, new TimeSpan(0, 0, expiry));
            }
            return result;
        }

        public static bool SetEntity<T>(string key, T entity)
        {
            bool result;
            using (IRedisClient client = GetClient())
            {
                result = client.Set<T>(key, entity);
            }
            return result;
        }

        /// <summary>
        /// key---Item---timeout
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="entity"></param>
        /// <param name="expiry"></param>
        /// <returns></returns>
        public static bool SetEntity<T>(string key, T entity, int expiry)
        {
            bool result;
            using (IRedisClient client = GetClient())
            {
                result = client.Set<T>(key, entity, new TimeSpan(0, 0, expiry));
            }
            return result;
        }


        public static bool SetText(string key, string text)
        {
            bool result;
            using (IRedisClient client = GetClient())
            {
                result = client.Set<string>(key, text);
            }
            return result;
        }

        public static T GetEntities<T>(string key)
        {
            T result;
            using (IRedisClient client = GetClient())
            {
                result = client.Get<T>(key);
            }
            return result;
        }

        public static List<T> GetListEntities<T>(string key)
        {
            List<T> result;
            using (IRedisClient client = GetClient())
            {
                result = client.Get<List<T>>(key);
            }
            return result;
        }

        public static string GetText(string key)
        {
            string result;
            using (IRedisClient client = GetClient())
            {
                result = (client.Get<string>(key) ?? string.Empty);
            }
            return result;
        }

        public static List<string> GetAllKeys()
        {
            List<string> allKeys;
            using (IRedisClient client = GetClient())
            {
                allKeys = client.GetAllKeys();
            }
            return allKeys;
        }

        public static Dictionary<string, T> GetItems<T>(List<string> keys)
        {
            Dictionary<string, T> result;
            if (keys != null && keys.Count > 0)
            {
                using (IRedisClient client = GetClient())
                {
                    result = (Dictionary<string, T>)client.GetAll<T>(keys);
                    return result;
                }
            }
            result = new Dictionary<string, T>();
            return result;
        }

        public static bool Remove(string key)
        {
            bool result;
            using (IRedisClient client = GetClient())
            {
                result = client.Remove(key);
            }
            return result;
        }

        public static void RemoveAll(List<string> keys)
        {
            if (keys != null && keys.Count > 0)
            {
                using (IRedisClient client = GetClient())
                {
                    client.RemoveAll(keys);
                }
            }
        }

        public static void Clear()
        {
            using (GetClient())
            {
                List<string> allKeys = GetAllKeys();
                RemoveAll(allKeys);
            }
        }

        public static string GetDataBase()
        {
            using (IRedisClient client = GetClient())
            {
                return client.Db.ToString();
            }
        }
    }
}
