using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PowerNew.Model;

namespace PowerNew.Controllers
{
    public class ChooseBaseController : Controller
    {
        // GET: ChooseBase
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);
            var cityName = RouteData.Values["company"];
            if (cityName.Equals("www"))
            {
                new powerbjfEntities();
            }
        }
    }
}