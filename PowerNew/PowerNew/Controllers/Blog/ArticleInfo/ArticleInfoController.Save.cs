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
    public partial class ArticleInfoController
    {
        [ValidateInput(false)]
        [HttpPost]
        public ActionResult SubmitSave(ArticleInfo info)
        {
            try
            {
                var model = new bjf_articleinfo();
                model.typeid = info.typeid;
                model.parentid = info.parentid;
                model.title = info.title;
                model.photourl = info.ImageUrl;
                model.readcount = 0;
                model.content = info.Editor;
                model.createtime = model.updatetime = DateTime.Now;
                model.createid = model.updateid = Convert.ToInt32(SessionHelper.GetSession("userid"));
                model.userid = Convert.ToInt32(SessionHelper.GetSession("userid"));
                ArticleInfoManager.GetInstance().Add(model);
                ArticleInfoManager.GetInstance().Save();
                return Json(new { state = 0, msg = "操作成功" });
            }
            catch (Exception e)
            {
                return Json(new { state = 1, msg = e.Message });
            }
        }


        [ValidateInput(false)]
        [HttpPost]
        public ActionResult SubmitEdit(ArticleInfo info)
        {
            try
            {
                var item = ArticleInfoManager.GetInstance().GetModel(info.ID);
                if (item == null)
                {
                    return Json(new {state = 1, msg = "未找到该条记录"});
                }
                item.typeid = info.typeid;
                item.parentid = info.parentid;
                item.title = info.title;
                item.photourl = info.ImageUrl;
                item.content = info.Editor;
                item.updateid = Convert.ToInt32(SessionHelper.GetSession("userid"));
                item.userid = Convert.ToInt32(SessionHelper.GetSession("userid"));
                ArticleInfoManager.GetInstance().Update(item);
                ArticleInfoManager.GetInstance().Save();
                return Json(new { state = 0, msg = "操作成功" });
            }
            catch (Exception e)
            {
                return Json(new { state = 1, msg = e.Message });
            }
        }

        public class ArticleInfo
        {
            public int ID { get; set; }
            public int parentid { get; set; }
            public int typeid { get; set; }

            public string title { get; set; }
            public string ImageUrl { get; set; }
            public string Editor { get; set; }
        }
    }
}