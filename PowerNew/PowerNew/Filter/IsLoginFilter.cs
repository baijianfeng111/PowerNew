using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Web;
using System.Web.Mvc;

namespace PowerNew.Filter
{
    public class IsLoginAuthorizeAttribute : AuthorizeAttribute
    {
        /// <summary>
        /// 判断是否登录
        /// </summary>
        /// <param name="filterContext"></param>
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            //session不存在直接跳转到登录页面
            var sessionuser = filterContext.HttpContext.Session["userinfo"];
            if (sessionuser == null)
            {
                filterContext.Result = new RedirectResult("/Login/Login");
            }
        }
    }
}