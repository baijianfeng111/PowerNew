using System;
using System.Net.Mail;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
//using KafKaHelper;
using PowerNew.Common;
using Rym.MonthWage.Bll;

namespace PowerNew.Controllers.Welcome
{
    public class WelcomeController : BaseController
    {
        // GET: Welcome
        public ActionResult Index()
        {

            //发邮件
            //var info = new SendMailManager.MailInfo(new MailAddress("806078508@qq.com"), "2225199512@qq.com", "激活", "激活了，谢谢");
            //SendMailManager.SendMail(info);

            //try
            //{
            //    Thread t = new Thread(new ThreadStart(delegate()
            //    {
            //        int sum = 0;
            //        for (int i = 0; i < 100; i++)
            //        {
            //            //sum += i;
            //            var info = new SendMailManager.MailInfo(new MailAddress("806078508@qq.com"), "2225199512@qq.com", "激活", "激活了，谢谢");
            //            var topic = "emailtopic";
            //           // KafkaHelper.ProduceMsg(topic, info);
            //        }
            //        Thread.Sleep(10 * 1000);
            //        LogHelper.log.Error("总数:" + sum);
            //    }));
            //    t.Start();
            //    t.IsBackground = true;
            //}
            //catch (Exception e)
            //{
            //    LogHelper.log.Error(e.Message);
            //}


            //var routeRequest = new RequestRouter(HttpContext);
            //routeRequest.ToRoute("Default", new { controller = "VisitView", action = "Index" });
            //return new EmptyResult();


            return View();
        }

        public ActionResult Chat()
        {
            return View();
        }

        public class RequestRouter
        {
            readonly HttpContextBase _Context;

            public HttpContextBase Context
            {
                get { return this._Context; }
            }
            public RequestRouter(HttpContextBase context)
            {
                this._Context = context;
            }

            void Route(RouteData routeData)
            {
                var requestContext = new RequestContext(Context, routeData);

                //重写内部请求路径
                var newPath = routeData.Route.GetVirtualPath(requestContext, null).VirtualPath;
                requestContext.HttpContext.RewritePath(newPath);

                //获取处理程序，处理请求
                IHttpHandler handler = routeData.RouteHandler.GetHttpHandler(requestContext);
                if (handler == null)
                    throw new Exception("未能从指定路由中获取到 IHttpHandler");
                handler.ProcessRequest(System.Web.HttpContext.Current);
            }

            public void ToRoute(string routeName, object values)
            {
                //获得路由实例
                var route = RouteTable.Routes[routeName];
                if (route == null)
                    throw new Exception(string.Format("路由表中不存在名为 \"{0}\" 的路由", routeName));
                //创建路由数据
                var routeData = new RouteData(route, new MvcRouteHandler());
                //添加路由参数/值
                foreach (var pair in new RouteValueDictionary(values))
                {
                    routeData.Values[pair.Key] = pair.Value;
                }
                Route(routeData);
            }

            public void ToUrl(string url, object values)
            {
                //创建路由处理程序实例
                var routeHandler = new MvcRouteHandler();
                //创建路由数据
                var routeData = new RouteData(new Route(url, routeHandler), routeHandler);
                //添加路由参数/值
                foreach (var pair in new RouteValueDictionary(values))
                {
                    routeData.Values[pair.Key] = pair.Value;
                }
                Route(routeData);
            }
        }



    }
}