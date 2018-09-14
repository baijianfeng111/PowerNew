using System;
using System.Web.Mvc;
using PowerNew.Bll;
using PowerNew.Common;
using PowerNew.Model;

namespace PowerNew.Controllers
{
    public partial class RoleController
    {

        [HttpPost]
        public ActionResult SubmitSave(bjf_role submitItem)
        {
            try
            {
                var item = RoleManager.GetInstance().SelectOne(m => m.isdelete == false && m.rolecode == submitItem.rolecode);
                if ((submitItem.id == 0 && item != null) || (submitItem.id != 0 && item != null && submitItem.id != item.id))
                {
                    return Json(new { state = 1, msg = "该角色编码已存在,不能重复添加." });
                }
                RoleManager.GetInstance().SaveItem(submitItem);
                return Json(new { state = 0, msg = "保存成功." });
            }
            catch (Exception ex)
            {
                LogHelper.log.Error(ex.Message);
                return Json(new { state = 1, msg = ex.Message });
            }
        }
    }
}