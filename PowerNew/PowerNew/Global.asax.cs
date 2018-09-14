using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using PowerNew.Job.Trigger;
using StackExchange.Profiling;

namespace PowerNew
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            //log4net的配置文件
            log4net.Config.XmlConfigurator.Configure(new FileInfo(Server.MapPath("~/log4net.config")));

            //用Scripts和Styles将脚本和样式表引入页面时，无需修改任何代码就可以将脚本和样式表编译压缩输入到客户端，这样不仅可以有效的增加JSHack的难度以及降低文件的大小。
            //BundleTable.EnableOptimizations = true;
            //BundleConfig.RegisterBundles(BundleTable.Bundles);

            //EF调试器
            //MiniProfilerEF.Initialize();

            //定时任务启动
            //StartScheduler();
        }

        public static void StartScheduler()
        {
            ReportJobScheduler.Start();
        }

        //protected void Application_BeginRequest()
        //{
        //    MiniProfiler.Start();
        //}
        //protected void Application_EndRequest()
        //{
        //    MiniProfiler.Stop();
        //}
    }
}
