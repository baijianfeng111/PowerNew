using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LinqKit;
using PowerNew.Common;
using PowerNew.Model;

namespace PowerNew.Bll
{
    public partial class MenuManager
    {
        public bjf_menu GetGroupModel(int id)
        {
            return this.SelectOne(m => m.isuse == false && m.parentid == 0 && m.isdelete == false && m.id == id);
        }

        /// <summary>
        /// 通过地址获取菜单
        /// </summary>
        /// <param name="href"></param>
        /// <returns></returns>
        public bjf_menu GetByHref(string href)
        {
            var menuhref = String.Format("/{0}/index", href);
            return this.SelectOne(m => m.menuhref == menuhref && m.isuse == false && m.isdelete == false);
        }
        /// <summary>
        /// 获取所有子类id
        /// </summary>
        /// <param name="parentid"></param>
        /// <returns></returns>
        public List<int> GetListChildren(int parentid)
        {
            var list = this.SelectList(m => m.isuse == false && m.isdelete == false && m.parentid == parentid);
            var intlist = new List<int>();
            if (list.Count > 0)
            {
                intlist = list.Select(m => m.id).ToList();
            }
            return intlist;
        }
    }

}
