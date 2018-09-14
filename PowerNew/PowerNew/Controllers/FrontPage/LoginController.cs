using System;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using PowerNew.Bll;
using PowerNew.Common;
using PowerNew.Filter;

namespace PowerNew.Controllers
{
    public class LoginController : Controller
    {

        //QQ三方登录
        //https://blog.csdn.net/u010678947/article/details/50472033

        [IsLoginAuthorize]
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Login()
        {
            return View();
        }
        public ActionResult CheckCode()
        {
            var validManager = new ValidateCode();
            string code = validManager.GetValidCode(4);
            //将验证码放入session，设置有效时间
            Session["ValidateCode"] = code;
            Session.Timeout = 3;
            byte[] bytes = validManager.CreateValidateGraphic(code);
            return File(bytes, @"image/jpeg");
        }

        [HttpPost]
        public ActionResult Login(LoginQuery query)
        {
            try
            {
                if (Session["ValidateCode"] == null)
                {
                    return Json(new { state = 1, msg = "验证码已过期，请重新输入" });
                }
                if (Session["ValidateCode"].ToString().ToLower() != query.ValidCode.ToLower())  //验证码忽略大小写
                {
                    return Json(new { state = 1, msg = "验证码错误" });
                }

                //判断用户信息
                var item = UserManager.GetInstance().GetItemByLoginName(query.LoginName);
                if (item == null)
                {
                    return Json(new { state = 1, msg = "此用户不存在." });
                }
                if (query.PassWord != EncryAndDecryptHelper.Decryption(item.password))
                {
                    return Json(new { state = 1, msg = "用户密码不对." });
                }
                var roleList = UserForRoleManager.GetInstance().GetListRole(item.id);
                if (roleList.Count == 0)
                {
                    return Json(new { state = 1, msg = "当前用户暂未分配角色，不能使用系统." });
                }

                //用户全部角色集合
                string arrRoleid = "";
                roleList.ForEach(m => arrRoleid += m.roleid + ",");

                //用户可查看菜单集合
                var menulist = RoleForMenuManager.GetInstance().GetListMenuId(roleList);
                string arrMenuid = "";
                menulist.ForEach(m => arrMenuid += m.ToString() + ",");

                //记住用户名和密码
                if (query.Rember != null)
                {
                    //放进cookie中
                    Response.Cookies.Add(new HttpCookie("cookieLoginname", query.LoginName));
                    Response.Cookies.Add(new HttpCookie("cookiePassword", query.PassWord));
                    Response.Cookies["cookieLoginname"].Expires = DateTime.Now.AddDays(3);
                    Response.Cookies["cookiePassword"].Expires = DateTime.Now.AddDays(3);
                }
                //验证通过将用户信息记录进session
                SessionHelper.SetSession("userinfo", item);
                SessionHelper.SetSession("userid", item.id);
                SessionHelper.SetSession("username", item.username);
                SessionHelper.SetSession("rolelist", arrRoleid);                //角色集合
                SessionHelper.SetSession("menulist", menulist);                 //菜单集合
                SessionHelper.SetSessionTimeout(20);

                //写登录日志
                LoginLogManager.GetInstance().SaveItem();
                return Json(new { state = 0, msg = "登录成功." });
            }
            catch (Exception e)
            {
                LogHelper.log.Error(e.Message);
                return Json(new { state = 1, msg = e.Message });
            }
        }

        public ActionResult SignOut()
        {
            SessionHelper.ClearSession();
            FormsAuthentication.SignOut();
            return RedirectToAction("Index");
        }

        public ActionResult NoAuth()
        {
            return View();
        }
        public JsonState CheckLogin(LoginQuery query)
        {
            var json = new JsonState();
            if (Session["ValidateCode"] == null)
            {
                json.state = 1; json.msg = "验证码已过期，请重新输入";
            }
            if (Session["ValidateCode"] != null && Session["ValidateCode"].ToString().ToLower() != query.ValidCode.ToLower())  //验证码忽略大小写
            {
                json.state = 1; json.msg = "验证码错误";
            }
            var item = UserManager.GetInstance().GetItemByLoginName(query.LoginName);
            if (item == null)
            {
                json.state = 1; json.msg = "此用户不存在";
            }
            if (query.PassWord != EncryAndDecryptHelper.Decryption(item.password))
            {
                json.state = 1; json.msg = "用户密码不对";
            }
            return json;
        }

        public class JsonState
        {
            public int state { get; set; }
            public string msg { get; set; }
        }

        public class LoginQuery
        {
            public string LoginName { get; set; }
            public string PassWord { get; set; }
            public string ValidCode { get; set; }
            public string Rember { get; set; }
        }
    }
}