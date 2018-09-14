using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PowerNew.Bll;
using PowerNew.Model;
using PowerNew.Common;

namespace PowerNew.Controllers
{
    public partial class BlockController 
    {
        [HttpPost]
        public ActionResult SubmitSave(bjf_block submititem)
        {
            try
            {
                var item = BlockManager.GetInstance().SelectOne(m => m.isdelete == false && m.blockname == submititem.blockname);
                if ((submititem.id == 0 && item != null) || (submititem.id != 0 && item != null && submititem.id != item.id))
                {
                    return Json(new { state = 1, msg = "该模块名称已存在,不能重复添加." });
                }
                BlockManager.GetInstance().SaveItem(submititem);
                return Json(new { state = 0, msg = "保存成功." });
            }
            catch (Exception e)
            {
                LogHelper.log.Error(e.Message);
                return Json(new { state = 1, msg = e.Message });
            }
        }
    }
}