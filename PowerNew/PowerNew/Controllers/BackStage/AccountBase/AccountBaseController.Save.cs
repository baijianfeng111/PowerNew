using System;
using System.Web.Mvc;
using PowerNew.Bll;
using PowerNew.Common;
using PowerNew.Model;

namespace PowerNew.Controllers
{
    public partial class AccountBaseController 
    {


        [HttpPost]
        public ActionResult SubmitSave(bjf_account submitItem)
        {
            try
            {
                var item = AccountManager.GetInstance().GetByMobile(submitItem.mobile);
                if (submitItem.id == 0 && item != null)
                {
                    return Json(new { state = 1, msg = "该手机号已存在，不允许重复." });
                }
                else if (submitItem.id != 0 && item != null && item.id != submitItem.id)
                {
                    return Json(new { state = 1, msg = "该手机号已存在，不允许重复." });
                }
                AccountManager.GetInstance().SaveItem(submitItem);
                return Json(new { state = 0, msg = "操作成功." });
            }
            catch (Exception ex)
            {
                LogHelper.log.Error(ex.Message);
                return Json(new { state = 1, msg = ex.Message });
            }
        }

        [HttpPost]
        public ActionResult CreateApplication(int id)
        {
            try
            {
                var item = AccountManager.GetInstance().GetItem(id);
                if (item == null)
                {
                    return Json(new { state = 1, msg = "未找到该条记录" });
                }
                AccountManager.GetInstance().CreateDataBase(item);
                return Json(new { state = 0, msg = "创建成功" });
            }
            catch (Exception ex)
            {
                LogHelper.log.Error(ex.Message);
                return Json(new { state = 1, msg = ex.Message });
            }
        }

    }
}