using System;
using System.Collections.Generic;
using PowerNew.Dal;
using PowerNew.Model;

namespace PowerNew.Bll
{
    public class RoleForMenuManager : BaseDal<bjf_roleformenu>
    {
        private static RoleForMenuManager _instance;
        private RoleForMenuManager() { }

        public static RoleForMenuManager GetInstance()
        {
            return _instance ?? (_instance = new RoleForMenuManager());
        }

        /// <summary>
        /// 根据用户的角色获取新的菜单树
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public List<NewTree> GetListMenu(List<bjf_roleforuser> list)
        {
            var plist = new List<NewTree>(); //新树
            var listMenuParent = new List<int>();  //不重复一级类
            var listMenuChild = new List<int>(); //不重复二级类
            if (list.Count == 0)
            {
                return plist;
            }
            foreach (var item in list) //循环角色
            {
                var menulist = this.SelectList(m => m.roleid == item.roleid); //角色对应的菜单集合
                if (menulist.Count <= 0) continue;
                foreach (var menu in menulist)
                {
                    var groupmodel = MenuManager.GetInstance().GetGroupModel(menu.menuid); //找一级菜单
                    if (groupmodel != null && !listMenuParent.Contains(menu.menuid))
                    {
                        listMenuParent.Add(menu.menuid);
                    }
                    else
                    {
                        if (listMenuChild.Contains(menu.menuid)) continue;
                        listMenuChild.Add(menu.menuid);
                    }
                }
            }


            if (listMenuParent.Count == 0)
            {
                return plist;
            }
            foreach (var parentid in listMenuParent)
            {
                if (listMenuChild.Count == 0)
                {
                    plist.Add(new NewTree()
                    {
                        Parentid = parentid,
                        ChildList = null
                    });
                }
                var childlist = MenuManager.GetInstance().GetListChildren(parentid); //父类下所有的子类
                var listcld = new List<int>();
                foreach (var childid in listMenuChild)
                {
                    if (childlist.Count > 0 && childlist.Contains(childid) && !listcld.Contains(childid))
                    {
                        listcld.Add(childid);
                    }
                }
                plist.Add(new NewTree()
                {
                    Parentid = parentid,
                    ChildList = listcld
                });
            }
            return plist;
        }


        public class NewTree
        {
            public int Parentid { get; set; }
            public List<int> ChildList { get; set; }
        }

        public List<int> GetListMenuId(List<bjf_roleforuser> list)
        {
            var listmenuid = new List<int>();
            foreach (var item in list)
            {
                var menulist = this.SelectList(m => m.roleid == item.roleid);      //获取角色对应的菜单集合
                if (menulist.Count <= 0) continue;
                foreach (var menu in menulist)
                {
                    if (!listmenuid.Contains(menu.menuid))
                    {
                        listmenuid.Add(menu.menuid);
                    }
                }
            }
            return listmenuid;
        }
    }
}
