using System;
using System.Collections.Generic;
using System.Web.Mvc;
using PowerNew.Bll;
using PowerNew.Common;
using PowerNew.Model;

namespace PowerNew.Controllers
{

    public partial class ArticleTypeController
    {

        public ActionResult Create(int parentid = 0)
        {
            if (parentid > 0)
            {
                var parent = ArticleTypeManager.GetInstance().GetModel(parentid);
                ViewBag.ParentName = parent != null ? parent.title : "";
            }
            return View("Edit", new bjf_articletype() { parentid = parentid });
        }
        public ActionResult Edit(int id)
        {
            var model = ArticleTypeManager.GetInstance().GetModel(id);
            if (model.parentid > 0)
            {
                var parent = ArticleTypeManager.GetInstance().GetModel(model.parentid);
                ViewBag.ParentName = parent != null ? parent.title : "";
            }
            return View(model);
        }

        [HttpPost]
        public ActionResult SubmitSave(bjf_articletype item)
        {
            try
            {
                if (item.id == 0) //表示添加
                {

                    item.createid = Convert.ToInt32(SessionHelper.GetSession("userid"));
                    item.updateid = Convert.ToInt32(SessionHelper.GetSession("userid"));
                    item.createtime = DateTime.Now;
                    item.updatetime = DateTime.Now;
                    ArticleTypeManager.GetInstance().Add(item);
                    ArticleTypeManager.GetInstance().Save();
                }
                if (item.id > 0)
                {
                    var model = ArticleTypeManager.GetInstance().GetModel(item.id);
                    model.updateid = Convert.ToInt32(SessionHelper.GetSession("userid"));
                    model.updatetime = DateTime.Now;
                    model.title = item.title;
                    model.parentid = item.parentid;
                    model.comment = item.comment;
                    ArticleTypeManager.GetInstance().Update(model);
                    ArticleTypeManager.GetInstance().Save();
                }
                return Json(new { state = 0, msg = "操作成功" });
            }
            catch (Exception ex)
            {
                return Json(new { state = 1, msg = ex.Message });
            }
        }

        [HttpPost]
        public ActionResult IsParent(int id)
        {
            try
            {
                var model = ArticleTypeManager.GetInstance().GetModel(id);
                if (model == null)
                {
                    return Json(new { state = 1, msg = "未找到该条记录" });
                }
                if (model.parentid == 0)       //表示父类
                {
                    return Json(new { state = 1, msg = 0 });
                }
                return Json(new { state = 0, msg = 1 }); //表示子类
            }
            catch (Exception ex)
            {
                return Json(new { state = 1, msg = ex.Message });
            }
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            try
            {
                var model = ArticleTypeManager.GetInstance().GetModel(id);
                if (model == null)
                {
                    return Json(new { state = 1, msg = "未找到该条记录" });
                }
                if (model.parentid == 0) //表示父类，删除本身和子类
                {
                    //找出子类,删除
                    var childlist = ArticleTypeManager.GetInstance().GetListChildren(model.id);
                    if (childlist.Count > 0)
                    {
                        foreach (var item in childlist)
                        {
                            item.isdelete = true;
                            item.updatetime = DateTime.Now;
                            item.updateid = Convert.ToInt32(Session["userid"]);
                        }
                        ArticleTypeManager.GetInstance().Update(model);
                    }
                    ArticleTypeManager.GetInstance().Save();
                }
                //删除本身
                model.isdelete = true;
                model.updatetime = DateTime.Now;
                model.updateid = Convert.ToInt32(Session["userid"]);
                ArticleTypeManager.GetInstance().Update(model);
                ArticleTypeManager.GetInstance().Save();
                return Json(new { state = 0, msg = "删除成功" });
            }
            catch (Exception ex)
            {
                return Json(new { state = 1, msg = ex.Message });
            }
        }

        [HttpPost]
        public ActionResult GetTwoArticleType(int parentid)
        {
            var listmodel = ArticleTypeManager.GetInstance().GetListChildren(parentid);
            var result = new ContentResult();

            if (listmodel.Count > 0)
            {
                var list = SelectListCenter.GetArticleTypeTwoSelectList(parentid, null);
                result.Content = list.ToHtml();
            }
            else
            {
                var list = new List<SelectListItem>();
                result.Content = list.ToHtml();
            }
            return result;
        }

    }
}