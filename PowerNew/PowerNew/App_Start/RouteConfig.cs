using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using PowerNew.Mvc;

namespace PowerNew
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");   //忽略对扩展名.axd文件的请求

            //routes.Add("DomainRoute", new DomainRoute(
            //    "{company}.login.com",     // Domain with parameters 
            //    "{controller}/{action}/{id}",    // URL with parameters 
            //    new { company = "", controller = "Blog", action = "Index", id = "" }  // Parameter defaults 
            //));

            //加载到路由的配置文件中        maproute为添加路由映射、路由的匹配是自上而下的
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Blog", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
