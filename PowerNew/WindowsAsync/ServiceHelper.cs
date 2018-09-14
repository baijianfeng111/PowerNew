using System.Web.Script.Serialization;
using WindowsAsync.ServiceReference1;

namespace KafkaWindows
{

    /// <summary>
    /// WCF帮助类
    /// </summary>
    static class ServiceHelper
    {
        private static string _username = "dyjuid9864";

        private static string _password = "hgjpin3406";

        /// <summary>
        /// 获取redis
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string GetKeyValue(string key)
        {
          
            Service1Client s = new Service1Client();
            s.ClientCredentials.UserName.UserName = _username;
            s.ClientCredentials.UserName.Password = _password;
            var value = s.GetRedisKeyValue(key);
            s.Close();
            return value;
        }

        /// <summary>
        /// 设置redis的key-value
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public static void SetKeyValue(string key, string value)
        {
            Service1Client s = new Service1Client();
            s.ClientCredentials.UserName.UserName = _username;
            s.ClientCredentials.UserName.Password = _password;
            s.SetRedisKeyValue(key, value);
            s.Close();
        }

        /// <summary>
        /// 激活某个客户盒子
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static bool SetRedisKey(string key)
        {
            Service1Client s = new Service1Client();
            s.ClientCredentials.UserName.UserName = _username;
            s.ClientCredentials.UserName.Password = _password;
            bool flag = s.SetRedisKey(key);
            s.Close();
            return flag;
        }

        /// <summary>
        /// 删除redis
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static bool RemoveRedisKey(string key)
        {
            Service1Client s = new Service1Client();
            s.ClientCredentials.UserName.UserName = _username;
            s.ClientCredentials.UserName.Password = _password;
            bool flag = s.RemoveKey(key);
            s.Close();
            return flag;
        }

        /// <summary>
        /// 上传录音
        /// </summary>
        /// <param name="bytes"></param>
        /// <param name="filename"></param>
        /// <returns></returns>
        public static string UploadRecord(byte[] bytes, string filename)
        {
            Service1Client s = new Service1Client();
            s.ClientCredentials.UserName.UserName = _username;
            s.ClientCredentials.UserName.Password = _password;
            string url = s.UploadRecord(bytes, filename);
            s.Close();
            return url;
        }


        public static bool AddBase(int customerid, int createid, string url)
        {
            Service1Client s = new Service1Client();
            s.ClientCredentials.UserName.UserName = _username;
            s.ClientCredentials.UserName.Password = _password;
            string value = s.GetAddCallPhoneWav(customerid, createid, url);

            var obj = new JavaScriptSerializer().Deserialize<Info>(value);
            int flag = obj.state;
            s.Close();
            return flag == 0 ? true : false;
        }

        public static bool SetEntity(string key, ClassInfo info)
        {
            Service1Client s = new Service1Client();
            s.ClientCredentials.UserName.UserName = _username;
            s.ClientCredentials.UserName.Password = _password;
            bool flag = s.SetEntity(key, info);
            s.Close();
            return flag;
        }

        public static ClassInfo GetEntity(string key)
        {
            Service1Client s = new Service1Client();
            s.ClientCredentials.UserName.UserName = _username;
            s.ClientCredentials.UserName.Password = _password;
            var flag = s.GetEntity(key);
            s.Close();
            return flag;
        }

        public class Info
        {
            public int state { get; set; }
            public string msg { get; set; }
        }

        //public class ClassInfo
        //{
        //    public int userid { get; set; }
        //    public int customerid { get; set; }
        //    public string phone { get; set; }
        //    public string login { get; set; }
        //    public int phonestate { get; set; }   //当前电话的状态（0未激活，1激活，2呼叫，3挂机）
        //    public int openstate { get; set; }    //cs程序状态（0关闭，1开启）
        //}
    }
}
