using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MongoDB;
using PowerNew.Models;

namespace PowerNew.Controllers
{
    //public class MongoDbController : Controller
    //{
    //    UserModel userModel = new UserModel();
    //    public ActionResult Index()
    //    {
    //        return View();
    //    }

    //    /// <summary>
    //    /// 获取全部用户列表，通过json将数据提供给jqGrid
    //    /// </summary>
    //    public JsonResult UserList(string sord, string sidx, string rows, string page)
    //    {
    //        var list = userModel.FindAll();
    //        int i = 0;
    //        var query = from u in list
    //                    select new
    //                    {
    //                        id = i++,
    //                        cell = new string[]{
    //                            u["UserId"].ToString(),
    //                            u["UserName"].ToString(),
    //                            u["Age"].ToString(),
    //                            u["Tel"].ToString(),
    //                            u["Email"].ToString(),
    //                            "-"
    //                        }
    //                    };

    //        var data = new
    //        {
    //            total = query.Count() / Convert.ToInt32(rows) + 1,
    //            page = Convert.ToInt32(page),
    //            records = query.Count(),
    //            rows = query.Skip(Convert.ToInt32(rows) * (Convert.ToInt32(page) - 1)).Take(Convert.ToInt32(rows))
    //        };

    //        return Json(data, JsonRequestBehavior.AllowGet);
    //    }

    //    public ContentResult Add(string UserId, string UserName, int Age, string Tel, string Email)
    //    {
    //        Document doc = new Document();
    //        doc["UserId"] = UserId;
    //        doc["UserName"] = UserName;
    //        doc["Age"] = Age;
    //        doc["Tel"] = Tel;
    //        doc["Email"] = Email;

    //        try
    //        {
    //            userModel.Add(doc);
    //            return Content("添加成功");
    //        }
    //        catch
    //        {
    //            return Content("添加失败");
    //        }
    //    }

    //    public ContentResult Delete(string UserId)
    //    {
    //        try
    //        {
    //            userModel.Delete(UserId);
    //            return Content("删除成功");
    //        }
    //        catch
    //        {
    //            return Content("删除失败");
    //        }
    //    }

    //    public ContentResult Update(string UserId, string UserName, int Age, string Tel, string Email)
    //    {
    //        Document doc = new Document();
    //        doc["UserId"] = UserId;
    //        doc["UserName"] = UserName;
    //        doc["Age"] = Age;
    //        doc["Tel"] = Tel;
    //        doc["Email"] = Email;
    //        try
    //        {
    //            userModel.Update(doc);
    //            return Content("修改成功");
    //        }
    //        catch
    //        {
    //            return Content("修改失败");
    //        }
    //    }
    //}
}