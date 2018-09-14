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
    public partial class UserController
    {

        [HttpPost]
        public ActionResult SubmitSave(bjf_user submitItem)
        {
            try
            {
                var item = UserManager.GetInstance().SelectOne(m => m.isdelete == false && m.loginname == submitItem.username);
                if ((submitItem.id == 0 && item != null) || (submitItem.id != 0 && item != null && submitItem.id != item.id))
                {
                    return Json(new { state = 1, msg = "该登录名已存在,不能重复添加." });
                }
                UserManager.GetInstance().SaveItem(submitItem);
                return Json(new { state = 0, msg = "保存成功." });
            }
            catch (Exception ex)
            {
                LogHelper.log.Error(ex.Message);
                return Json(new { state = 1, msg = ex.Message });
            }
        }

        [HttpPost]
        public ActionResult SaveChoose(Info info)
        {
            try
            {
                if (info.list.Count == 0)
                {
                    return Json(new { state = 1, msg = "请至少选择一个角色" });
                    ;
                }
                var usermodel = UserManager.GetInstance().GetItemById(info.userid);
                if (usermodel == null)
                {
                    return Json(new { state = 1, msg = "未找到该用户信息" });
                }

                //如果该用户已选择角色后则删除
                var list = UserForRoleManager.GetInstance().SelectList(m => m.userid == info.userid);
                if (list.Count > 0)
                {
                    foreach (var item in list)
                    {
                        item.isdelete = true;
                        item.updatetime = DateTime.Now;
                        UserForRoleManager.GetInstance().Update(item);
                    }
                    UserForRoleManager.GetInstance().Save();
                }
                //保存
                foreach (var item in info.list.ToList())
                {
                    var model = new bjf_roleforuser()
                    {
                        userid = info.userid,
                        roleid = item.roleid,
                        createid = 1,
                        updateid = 1,
                        createtime = DateTime.Now,
                        updatetime = DateTime.Now
                    };
                    UserForRoleManager.GetInstance().Add(model);
                }
                UserForRoleManager.GetInstance().Save();
                return Json(new { state = 0, msg = "保存成功" });
            }
            catch (Exception ex)
            {
                LogHelper.log.Error(ex.Message);
                return Json(new { state = 1, msg = ex.Message });
            }
        }

        public class Info
        {
            public int userid { get; set; }
            public List<roleinfo> list { get; set; }
        }

        public class roleinfo
        {
            public int roleid { get; set; }
        }

    }
}