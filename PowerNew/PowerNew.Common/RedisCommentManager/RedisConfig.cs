using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServiceStack.Redis;

namespace PowerNew.Common
{
    public class RedisConfig
    {
        public class RedisServer
        {
            public string Host { get; set; }
        }

        public List<RedisServer> ReadWriteHosts{ get; set; }

        public List<RedisServer> ReadOnlyHosts { get; set; }

        public string Password { get; set; }

        public int ConnectTimeout { get; set; }

        public int RetryCount { get; set; }

        public int RetryTimeout { get; set; }

        public int SendTimeout { get; set; }

        public RedisClientManagerConfig RedisClientManagerConfig
        {
            get;
            set;
        }
    }
}
