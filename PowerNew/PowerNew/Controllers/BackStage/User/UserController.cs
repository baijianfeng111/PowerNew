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
    public partial class UserController : BaseController
    {
        // GET: Role
        public ActionResult Index(QueryUser query)
        {
            var list = UserManager.GetInstance().GetPageList(query);
            ViewBag.param = query;
            return View(list);
        }

        public ActionResult Create()
        {
            return View(new bjf_user());
        }

        public ActionResult Edit(int id)
        {
            var item = UserManager.GetInstance().GetItemById(id);
            return View("Create", item);
        }


        [HttpPost]
        public ActionResult Delete(int id)
        {
            try
            {
                var item = UserManager.GetInstance().SelectOne(m => m.id == id && m.isdelete == false);
                if (item == null)
                {
                    return Json(new { state = 1, msg = "未找到该条纪录," });
                }
                item.isdelete = true;
                item.updateid = Convert.ToInt32(Session["userid"]);
                item.updatetime = DateTime.Now;
                UserManager.GetInstance().Update(item);
                UserManager.GetInstance().Save();
                return Json(new { state = 0, msg = "删除成功" });
            }
            catch (Exception ex)
            {
                LogHelper.log.Error(ex.Message);
                return Json(new { state = 1, msg = ex.Message });
            }
        }

        public ActionResult ChooseRole(int userid)
        {
            var model = UserManager.GetInstance().GetItemById(userid);
            //获取所有角色
            var listrole = RoleManager.GetInstance().SelectList(m => m.isdelete == false && m.isuse == 0);
            //获取该用户对应的角色信息
            var listroleforuser = UserForRoleManager.GetInstance().SelectList(m => m.isdelete == false && m.userid == userid);
            var roleIdList = new List<int>();
            if (listroleforuser.Count > 0)
            {
                roleIdList = listroleforuser.Select(m => m.roleid).ToList();
            }
            ViewBag.ListRole = listrole;
            ViewBag.ListRoleId = roleIdList;
            return View(model);
        }
    }
}