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
        public bjf_menu GetItemById(int id)
        {
            return this.SelectOne(m => m.isdelete == false && m.id == id);
        }
        public void SaveItem(bjf_menu submitItem)
        {
            if (submitItem.id == 0)
            {
                submitItem.menuhref = "/" + submitItem.menuhref.ToLower();
                submitItem.createid = Convert.ToInt32(SessionHelper.GetSession("userid")); ;
                submitItem.updateid = Convert.ToInt32(SessionHelper.GetSession("userid")); ;
                submitItem.createtime = DateTime.Now;
                submitItem.updatetime = DateTime.Now;
                this.Add(submitItem);
            }
            else
            {
                var item = this.GetItemById(submitItem.id);
                item.menuname = submitItem.menuname;
                item.menucode = submitItem.menucode;
                item.blockid = submitItem.blockid;
                item.menuhref = "/" + submitItem.menuhref.ToLower();
                item.isgroup = submitItem.isgroup;
                item.parentid = submitItem.parentid;
                item.sortcode = submitItem.sortcode;
                item.isuse = submitItem.isuse;
                submitItem.updateid = Convert.ToInt32(SessionHelper.GetSession("userid")); ;
                submitItem.updatetime = DateTime.Now;
                this.Update(item);
            }
            this.Save();
        }
    }
}
