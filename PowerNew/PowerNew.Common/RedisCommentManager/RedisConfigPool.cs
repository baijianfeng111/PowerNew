namespace PowerNew.Common
{
    public class RedisConfigPool : RedisBaseConfig
    {
        private static readonly string REDIS_REDISCONFIG = "REDIS_REDISCONFIG";

        public static RedisConfig GetRedisConfig()
        {
            return RedisBaseConfig.GetConfig<RedisConfig>(RedisConfigPool.REDIS_REDISCONFIG);
        }
    }
}
