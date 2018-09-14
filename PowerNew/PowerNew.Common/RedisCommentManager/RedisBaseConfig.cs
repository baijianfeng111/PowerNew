using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Caching;
using System.Xml;
using System.Xml.Serialization;

namespace PowerNew.Common
{
    public class RedisBaseConfig
    {
        private static ObjectCache cache = MemoryCache.Default;

        private static object GetCache(string key)
        {
            return RedisBaseConfig.cache[key];
        }

        private static void SetCache(string key, object value, string dependencyFilePath)
        {
            CacheItemPolicy cacheItemPolicy = new CacheItemPolicy();
            cacheItemPolicy.ChangeMonitors.Add(new HostFileChangeMonitor(new System.Collections.Generic.List<string>
            {
                dependencyFilePath
            }));
            RedisBaseConfig.cache.Set(key, value, cacheItemPolicy, null);
        }

        private static string GetConfigPath<T>()
        {
            string str = System.AppDomain.CurrentDomain.BaseDirectory + "/RedisConfig/";             //获取XML文件地址
            return str + typeof(T).Name + ".config";
        }

        internal static T GetConfig<T>(string key) where T : class, new()
        {
            System.Type typeFromHandle = typeof(T);
            object obj = RedisBaseConfig.GetCache(key);
            if (obj == null)
            {
                string configPath = RedisBaseConfig.GetConfigPath<T>();       
                if (System.IO.File.Exists(configPath))             //判断文件是否存在
                {
                    using (XmlTextReader xmlTextReader = new XmlTextReader(configPath)) //读取XML文件内容
                    {
                        XmlSerializer xmlSerializer = new XmlSerializer(typeFromHandle);
                        obj = xmlSerializer.Deserialize(xmlTextReader);
                    }
                    RedisBaseConfig.SetCache(key, obj, configPath);
                }
            }
            T t = obj as T;
            T result;
            if (t == null)
            {
                result = System.Activator.CreateInstance<T>();
            }
            else
            {
                result = t;
            }
            return result;
        }
    }
}
