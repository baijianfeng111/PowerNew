using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PowerNew.Controllers;

namespace PowerNew.Filter
{
    public class UserAuthorizeAttribute : AuthorizeAttribute
    {
        /// <summary>
        /// 限制ip的
        /// </summary>
        /// <param name="filterContext"></param>
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            string ip = filterContext.HttpContext.Request.UserHostAddress;
            if (!string.IsNullOrEmpty(ip) && !ip.Equals("192.168.1.11"))
            {
                filterContext.HttpContext.Response.Status = "301 Moved Permanently";
                filterContext.HttpContext.Response.Redirect("http://www.baidu.com");
                filterContext.HttpContext.Response.End();
                // filterContext.Result = new RedirectResult("www.baidu.com");
            }
        }
    }
}