using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PowerNew.Bll;
using PowerNew.Common;
using PowerNew.Model;

namespace PowerNew.Controllers
{
    public partial class MenuController : BaseController
    {
        // GET: Role
        public ActionResult Index(QueryMenu query)
        {
            var list = MenuManager.GetInstance().GetPageList(query);
            ViewBag.param = query;
            return View(list);
        }

        public ActionResult Create()
        {
            return View(new bjf_menu());
        }

        public ActionResult Edit(int id)
        {
            var item = MenuManager.GetInstance().SelectOne(m => m.id == id);
            return View("Create", item);
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            try
            {
                var item = MenuManager.GetInstance().SelectOne(m => m.id == id && m.isdelete == false);
                if (item == null)
                {
                    return Json(new { state = 1, msg = "未找到该条纪录," });
                }
                item.isdelete = true;
                item.updateid = Convert.ToInt32(Session["userid"]);
                item.updatetime = DateTime.Now;
                MenuManager.GetInstance().Update(item);
                MenuManager.GetInstance().Save();
                return Json(new { state = 0, msg = "删除成功" });
            }
            catch (System.Exception ex)
            {
                LogHelper.log.Error(ex.Message);
                return Json(new { state = 1, msg = ex.Message });
            }
        }
    }
}