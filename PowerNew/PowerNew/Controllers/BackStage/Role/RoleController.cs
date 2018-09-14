using System;
using System.Collections.Generic;
using System.Web.Mvc;
using PowerNew.Bll;
using PowerNew.Common;
using PowerNew.Model;

namespace PowerNew.Controllers
{
    public partial class RoleController : BaseController
    {
        // GET: Role
        public ActionResult Index(QueryRole query)
        {
            var list = RoleManager.GetInstance().GetPageList(query);
            ViewBag.param = query;
            return View(list);
        }

        public ActionResult Create()
        {
            return View(new bjf_role());
        }

        public ActionResult Edit(int id)
        {
            var item = RoleManager.GetInstance().GetItemById(id);
            return View("Create", item);
        }

        public ActionResult Tree(int id)
        {
            var item = RoleManager.GetInstance().GetItemById(id);
            return View(item);
        }

        [HttpPost]
        public ActionResult GetTreeJson(int roleid)
        {
            var treeNode = RoleManager.GetInstance().GetRootNode(roleid);
            return Json(new { data = treeNode });
        }

        [HttpPost]
        public ActionResult SaveAuth(TreeInfo info)
        {
            try
            {
                //删除之前的角色菜单关系
                var list = RoleForMenuManager.GetInstance().SelectList(m => m.roleid == info.roleid);
                if (list.Count > 0)
                {
                    RoleForMenuManager.GetInstance().RemoveList(list);
                    RoleForMenuManager.GetInstance().Save();
                }
                foreach (var item in info.list)
                {
                    if (item.id > 0)            //导航菜单排除在外
                    {
                        var model = new bjf_roleformenu()
                        {
                            roleid = info.roleid,
                            menuid = item.id,
                            createid = Convert.ToInt32(SessionHelper.GetSession("userid")),
                            updateid = Convert.ToInt32(SessionHelper.GetSession("userid")),
                            createtime = DateTime.Now,
                            updatetime = DateTime.Now
                        };
                        RoleForMenuManager.GetInstance().Add(model);
                    }
                }
                RoleForMenuManager.GetInstance().Save();
                return Json(new { state = 0, msg = "保存成功" });

            }
            catch (Exception e)
            {
                LogHelper.log.Error(e.Message);
                return Json(new { state = 1, msg = e.Message });
            }
        }

        public class TreeInfo
        {
            public int roleid { get; set; }
            public List<AuthItem> list { get; set; }
        }

        public class Info
        {
            public int roleid { get; set; }
            public List<AuthItem> list { get; set; }
        }
        public class AuthItem
        {
            public int id { get; set; }
            public string name { get; set; }
            public int isParent { get; set; }
            public int sortCode { get; set; }
            public string linkUrl { get; set; }
        }
        [HttpPost]
        public ActionResult Delete(int id)
        {
            try
            {
                var item = RoleManager.GetInstance().SelectOne(m => m.id == id && m.isdelete == false);
                if (item == null)
                {
                    return Json(new { state = 1, msg = "未找到该条纪录," });
                }
                item.isdelete = true;
                item.updateid = Convert.ToInt32(Session["userid"]);
                item.updatetime = DateTime.Now;
                RoleManager.GetInstance().Update(item);
                RoleManager.GetInstance().Save();
                return Json(new { state = 0, msg = "删除成功" });
            }
            catch (Exception ex)
            {
                LogHelper.log.Error(ex.Message);
                return Json(new { state = 1, msg = ex.Message });
            }
        }
    }
}