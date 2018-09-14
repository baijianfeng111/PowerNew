using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Script.Serialization;
using PowerNew.Bll;
using PowerNew.Common;
using PowerNew.Filter;
using PowerNew.Model;

namespace PowerNew.Controllers
{

    //基类控制器
    public class BaseController : Controller
    {
        // GET: Base
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);

            //LogHelper.log.Error("请求地址：" + Request.Url.AbsolutePath.ToLower());

            //判断session里面的登录用户信息是否存在
            var sessionuser = filterContext.HttpContext.Session["userinfo"];
            if (sessionuser == null)
            {
                filterContext.Result = new RedirectResult(Url.Action("Login", "Login"));
                return;
            }
            //控制器名
            var controller = filterContext.RouteData.GetRequiredString("Controller").ToLower();

            //LogHelper.log.Info(controller);
            //var userinfo = (bjf_user)sessionuser;
            //if (userinfo != null)
            //{
            //    LogHelper.log.Error(userinfo.loginname);
            //}

            //获取用户请求的URL地址.
            var urlmodel = MenuManager.GetInstance().GetByHref(controller);
            if (urlmodel == null)
            {
                filterContext.Result = new HttpStatusCodeResult(404);  //未找到则404
                return;
            }

            //该用户有此菜单权限
            var sessionMenu = filterContext.HttpContext.Session["menulist"];
            if (sessionMenu == null)
            {
                filterContext.Result = new RedirectResult(Url.Action("NoAuth", "Login"));
                return;
            }
            var sessionMenuList = (List<int>)sessionMenu;
            if (!sessionMenuList.Contains(urlmodel.id))
            {
                filterContext.Result = new RedirectResult(Url.Action("NoAuth", "Login"));
            }
        }
    }

}