using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PowerNew.Bll;
using PowerNew.Common;

namespace PowerNew.Controllers
{
    public partial class ArticleInfoController : BaseController
    {
        // GET: ArticleType
        public ActionResult Index(QueryArticleInfo query)
        {
            var list = ArticleInfoManager.GetInstance().GetPageList(query);
            ViewBag.Param = query;
            return View(list);
        }

        public ActionResult Create()
        {
            return View();
        }

        public ActionResult Edit(int id)
        {
            var model = ArticleInfoManager.GetInstance().GetModel(id);
            return View(model);
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            try
            {
                var item = ArticleInfoManager.GetInstance().SelectOne(m => m.id == id && m.isdelete == false);
                if (item == null)
                {
                    return Json(new { state = 1, msg = "未找到该条纪录," });
                }
                item.isdelete = true;
                item.updateid = Convert.ToInt32(Session["userid"]);
                item.updatetime = DateTime.Now;
                ArticleInfoManager.GetInstance().Update(item);
                ArticleInfoManager.GetInstance().Save();
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