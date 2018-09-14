using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Caching;
using System.Web.Mvc;
using Newtonsoft.Json;
using PowerNew.Common;
using PowerNew.Model;

namespace PowerNew.Bll
{
    public static class LoginHelper
    {
        /// <summary>
        /// 获取该用户菜单树     父类->子类
        /// </summary>
        /// <returns></returns>
        public static List<RoleForMenuManager.NewTree> GetMenuGroup()
        {
            var userid = SessionHelper.GetSession("userid");
            var list = new List<RoleForMenuManager.NewTree>();
            if (SessionHelper.GetSession("listrole" + "_" + userid) == null)
            {

                var listrole = UserForRoleManager.GetInstance().GetListRole(Convert.ToInt32(SessionHelper.GetSession("userid")));
                if (listrole.Count > 0)
                {
                    list = RoleForMenuManager.GetInstance().GetListMenu(listrole);
                }
                SessionHelper.SetSession("listrole" + "_" + userid, list);
            }
            list = (List<RoleForMenuManager.NewTree>)SessionHelper.GetSession("listrole" + "_" + userid);
            return list;
        }



        public class MenuTree
        {
            public int GroupMenuId { get; set; }
            public string GroupMenuName { get; set; }
        }

        public class MenuChidren
        {
            public int MenuId { get; set; }
            public string MenuName { get; set; }
            public string Url { get; set; }
            public string Icon { get; set; }
        }
    }
}
