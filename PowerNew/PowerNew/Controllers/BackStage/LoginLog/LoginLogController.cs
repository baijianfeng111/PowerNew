using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PowerNew.Bll;

namespace PowerNew.Controllers.BackStage.LoginLog
{
    public class LoginLogController : Controller
    {
        // GET: LoginLog
        public ActionResult Index(QueryRole param)
        {
            var list = LoginLogManager.GetInstance().GetPageList(param);
            return View(list);
        }

        public JsonResult GetTable(QueryRole param)
        {
            var list = LoginLogManager.GetInstance().GetShowTable(param);
            return Json(list, JsonRequestBehavior.AllowGet);
        }
    }
}