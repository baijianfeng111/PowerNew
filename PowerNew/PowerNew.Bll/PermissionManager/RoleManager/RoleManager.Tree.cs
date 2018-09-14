using System;
using System.Collections.Generic;
using System.Linq;
using LinqKit;
using PowerNew.Common;
using PowerNew.Model;

namespace PowerNew.Bll
{
    public partial class RoleManager
    {
        public class MenuTreeNode
        {
            public int id { get; set; }
            public string name { get; set; }
            public string linkUrl { get; set; }
            public int parentId { get; set; }
            public int sortCode { get; set; }
            public bool isParent { get; set; }
            public bool open { get; set; }
            public bool Checked { get; set; }
            public List<MenuTreeNode> children { get; set; }
        }
        public MenuTreeNode GetRootNode(int roleid = 0)
        {
            var root = new MenuTreeNode             //导航菜单
            {
                id = 0,
                name = "导航菜单",
                isParent = true,
                sortCode = 0,
                linkUrl = "",
                Checked = true,
                open = true
            };
            //该角色对应的菜单和菜单组
            var list = new List<int>();
            if (roleid != 0)
            {
                var listmenu = RoleForMenuManager.GetInstance().SelectList(m => m.roleid == roleid);
                if (listmenu.Count > 0)
                {
                    list = listmenu.Select(m => m.menuid).ToList();
                }
            }
            root.children = GetChildreNodes(root.id, list);
            return root;
        }


        /// <summary>
        /// 树结构对象
        /// </summary>
        /// <param name="parentId"></param>
        /// <param name="list"></param>
        /// <returns></returns>
        private static List<MenuTreeNode> GetChildreNodes(int parentId, List<int> list)
        {
            var nodes = new List<MenuTreeNode>();

            //获取所有的菜单组
            List<bjf_menu> groupmentList = MenuManager.GetInstance()
                .SelectList(m => m.isdelete == false && m.isuse == false && m.parentid == parentId);

            //循环菜单组找出子菜单
            if (groupmentList.Any())
            {
                foreach (var c in groupmentList)
                {
                    bool ischecked = list.Count > 0 && list.Contains(c.id) ? true : false;

                    var childList = GetChildreNodes(c.id, list);      //菜单组下的子菜单
                    var node = new MenuTreeNode
                    {
                        id = c.id,
                        name = c.menuname,
                        isParent = childList.Any(),
                        parentId = Convert.ToInt32(c.parentid),
                        sortCode = c.sortcode,
                        linkUrl = c.menuhref,
                        open = false,
                        Checked = ischecked,
                        children = childList                   //子菜单下的孙子菜单 以此类推
                    };
                    nodes.Add(node);
                }
            }
            return nodes;
        }
    }
}
