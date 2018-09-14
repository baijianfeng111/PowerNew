using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PowerNew.Common;
using PowerNew.Model;

namespace PowerNew.Bll
{
    public partial class RoleManager
    {
        public bjf_role GetItemById(int id)
        {
            return this.SelectOne(m => m.isdelete == false && m.id == id) ?? new bjf_role();
        }

        public void SaveItem(bjf_role submitItem)
        {
            if (submitItem.id == 0)
            {
                submitItem.createid = 1;
                submitItem.updateid = 1;
                submitItem.createtime = DateTime.Now;
                submitItem.updatetime = DateTime.Now;
                submitItem.openid = OpenHelper.CreateOpenId();
                this.Add(submitItem);
            }
            else
            {
                var item = this.GetItemById(submitItem.id);
                item.rolename = submitItem.rolename;
                item.rolecode = submitItem.rolecode;
                item.isuse = submitItem.isuse;
                item.updateid = 1;
                item.updatetime = DateTime.Now;
                this.Update(item);
            }
            this.Save();
        }
    }
}
