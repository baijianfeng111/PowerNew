using System;
using System.Configuration;
using System.Threading;
using System.Web;

namespace PowerNew.Common
{

    public class OpenHelper
    {
        public static string CreateOpenId()
        {
            return Guid.NewGuid().ToString("N");
        }




        public static string GetIp()
        {
            string ip = string.Empty; if (!string.IsNullOrEmpty(System.Web.HttpContext.Current.Request.ServerVariables["HTTP_VIA"])) ip = Convert.ToString(System.Web.HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"]); if (string.IsNullOrEmpty(ip)) ip = Convert.ToString(System.Web.HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"]); return ip;
        }

        public static string GetSysVersion()
        {
            string agent = HttpContext.Current.Request.ServerVariables["HTTP_USER_AGENT"];

            if (agent.IndexOf("NT 4.0") > 0)
            {
                return "Windows NT ";
            }
            else if (agent.IndexOf("NT 5.0") > 0)
            {
                return "Windows 2000";
            }
            else if (agent.IndexOf("NT 5.1") > 0)
            {
                return "Windows XP";
            }
            else if (agent.IndexOf("NT 5.2") > 0)
            {
                return "Windows 2003";
            }
            else if (agent.IndexOf("NT 6.0") > 0)
            {
                return "Windows Vista";
            }
            else if (agent.IndexOf("WindowsCE") > 0)
            {
                return "Windows CE";
            }
            else if (agent.IndexOf("NT") > 0)
            {
                return "Windows NT ";
            }
            else if (agent.IndexOf("9x") > 0)
            {
                return "Windows ME";
            }
            else if (agent.IndexOf("98") > 0)
            {
                return "Windows 98";
            }
            else if (agent.IndexOf("95") > 0)
            {
                return "Windows 95";
            }
            else if (agent.IndexOf("Win32") > 0)
            {
                return "Win32";
            }
            else if (agent.IndexOf("Linux") > 0)
            {
                return "Linux";
            }
            else if (agent.IndexOf("SunOS") > 0)
            {
                return "SunOS";
            }
            else if (agent.IndexOf("Mac") > 0)
            {
                return "Mac";
            }
            else if (agent.IndexOf("Linux") > 0)
            {
                return "Linux";
            }
            else if (agent.IndexOf("Windows") > 0)
            {
                return "Windows";
            }
            return "unknow";

        }
    }

}
