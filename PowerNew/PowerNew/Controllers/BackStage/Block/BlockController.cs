using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PowerNew.Bll;
using PowerNew.Common;
using PowerNew.Model;
using PowerNew.Filter;

namespace PowerNew.Controllers
{
    public partial class BlockController : BaseController
    {
        
        // GET: Role
        public ActionResult Index(QueryBlock query)
        {
            var list = BlockManager.GetInstance().GetPageList(query);
            ViewBag.param = query;
            return View(list);
        }

        [HttpPost]
        public ActionResult PageList(QueryBlock query)
        {
            var list = BlockManager.GetInstance().GetPageList(query);
            return Json(new { data = list.DataList, pagehtml = list.PagerHtml(), index = list.Index });
        }
        public ActionResult Create()
        {
            return View(new bjf_block());
        }

        public ActionResult Edit(int id)
        {
            var item = BlockManager.GetInstance().GetItemById(id);
            return View("Create", item);
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            try
            {
                var item = BlockManager.GetInstance().SelectOne(m => m.id == id && m.isdelete == false);
                if (item == null)
                {
                    return Json(new { state = 1, msg = "未找到该条纪录," });
                }
                item.isdelete = true;
                item.updateid = Convert.ToInt32(Session["userid"]);
                item.updatetime = DateTime.Now;
                BlockManager.GetInstance().Update(item);
                BlockManager.GetInstance().Save();
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