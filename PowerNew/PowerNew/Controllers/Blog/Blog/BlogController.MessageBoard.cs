using System;
using System.Linq;
using System.Web.Mvc;
using PowerNew.Bll;
using PowerNew.Common;
using PowerNew.Model;

namespace PowerNew.Controllers
{
    public partial class BlogController
    {
        #region 留言板
        public ActionResult MessageBoard()
        {

            return View();
        }
        #endregion

        public ActionResult LeaveMsg()
        {
            var list = LeaveMsgManager.GetInstance()
                .SelectList(m => m.message != null)
                .OrderByDescending(m => m.createtime)
                .Skip(0)
                .Take(5)
                .ToList();
            return PartialView(list);
        }


        //public ActionResult MsgForArticle(int articleid)
        //{
        //    return View();
        //}

        [HttpPost]
        public ActionResult SaveMsg(string msg)
        {
            try
            {
                var item = new bjf_leavemessage()
                {
                    type = false,
                    parentid = 0,
                    message = msg,
                    position = "",
                    createtime = DateTime.Now,
                    name = "匿名",
                    browser = OpenHelper.GetSysVersion(),
                    ip = OpenHelper.GetIp()
                };
                var a = Request.Browser.Version;
                LogHelper.log.Error(a);
                LeaveMsgManager.GetInstance().SaveMsg(item);
                return Json(new { state = 0, msg = "操作成功" });
            }
            catch (Exception e)
            {
                LogHelper.log.Debug(e.Message);
                return Json(new { state = 1, msg = e.Message });
            }
        }

        public class MsgInfo
        {
            public string msg { get; set; }
            public string position { get; set; }
        }
    }
}