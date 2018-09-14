using System;
using System.Web.Mvc;
using PowerNew.Bll;
using PowerNew.Common;
using PowerNew.Model;

namespace PowerNew.Controllers
{
    public partial class AccountBaseController : BaseController
    {
        // GET: Account
        public ActionResult Index(QueryAccount query)
        {
            var list = AccountManager.GetInstance().GetPageList(query);
            ViewBag.param = query;
            return View(list);
        }

        public ActionResult Create()
        {
            return View("Edit", new bjf_account());
        }

        public ActionResult Edit(int id)
        {
            var item = AccountManager.GetInstance().GetItem(id);
            return View(item);
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            try
            {
                var item = AccountManager.GetInstance().GetItem(id);
                if (item == null)
                {
                    return Json(new { state = 1, msg = "未找到该条纪录," });
                }
                item.isdelete = true;
                item.updateid = Convert.ToInt32(Session["userid"]);
                item.updatetime = DateTime.Now;
                AccountManager.GetInstance().Update(item);
                AccountManager.GetInstance().Save();
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