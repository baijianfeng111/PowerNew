using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Newtonsoft.Json;
using PowerNew.Bll;

namespace PowerNew.Controllers
{
    public partial class BlogController : Controller
    {
        public ActionResult Index()
        {
            //var cityName = RouteData.Values["company"];
            //if (!cityName.Equals("www"))
            //{
            //    return View("Login");
            //}
            return View();
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult GetArticleList(QueryArticleInfo query)
        {
            var list = ArticleInfoManager.GetInstance().GetBlogPageList(query);
            var data = list.DataList.Select(m => new ArticleInfoItem()
            {
                id = m.id,
                parentid = m.parentid,
                title = m.title,
                typeid = m.typeid,
                photourl = m.photourl,
                updatetime = m.updatetime,
                readcount = m.readcount,
                content = m.content,
                createtime = m.createtime,
                UserName = UserManager.GetInstance().GetItemById(m.userid).username,
            });
            return Json(new { data = data, pagehtml = list.PagerHtml(), index = list.Index });
        }
        /// <summary>
        /// 右边通用分布视图
        /// </summary>
        /// <returns></returns>
        public ActionResult GetRightView()
        {
            //获取管理员用户
            ViewBag.UserInfo = UserManager.GetInstance().SelectOne(p => p.username == "admin");
            //获取文章分类
            var list = ArticleTypeManager.GetInstance().SelectList(m => m.isdelete == false);
            var zNodes = list.Select(m => new NodeInfo()
            {
                id = m.id,
                pId = m.parentid,
                name = m.title,
                open = true
            });
            ViewBag.zNodes = JsonConvert.SerializeObject(zNodes);
            return PartialView();
        }


        public ActionResult SearchArticle()
        {
            var articleInfoList = new List<ArticleInfoController.ArticleInfo>();

            return View();
        }

        public ActionResult Detail(int id)
        {
            var model = ArticleInfoManager.GetInstance().GetModel(id);
            ViewBag.UserName = "";
            if (model != null)  //阅读次数+1
            {
                model.readcount += 1;
                ArticleInfoManager.GetInstance().Update(model);
                ArticleInfoManager.GetInstance().Save();
                ViewBag.UserName = UserManager.GetInstance().GetItemById(model.userid).username;
            }
            
            return View(model);
        }

        public class NodeInfo
        {
            public int id { get; set; }
            public int pId { get; set; }
            public string name { get; set; }
            public bool open { get; set; }
        }
    }
}