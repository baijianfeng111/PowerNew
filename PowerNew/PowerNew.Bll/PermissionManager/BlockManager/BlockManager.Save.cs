using System;
using PowerNew.Common;
using PowerNew.Model;

namespace PowerNew.Bll
{
    public partial class BlockManager
    {
        public bjf_block GetItemById(int id)
        {
            return this.SelectOne(m => m.isdelete == false && m.id == id) ?? new bjf_block();
        }

        public void SaveItem(bjf_block submitItem)
        {
            if (submitItem.id == 0)
            {
                submitItem.createid = Convert.ToInt32(SessionHelper.GetSession("userid"));
                submitItem.updateid = Convert.ToInt32(SessionHelper.GetSession("userid"));
                submitItem.createtime = DateTime.Now;
                submitItem.updatetime = DateTime.Now;
                this.Add(submitItem);
            }
            else
            {
                var item = this.GetItemById(submitItem.id);
                item.blockname = submitItem.blockname;
                item.isuse = submitItem.isuse;
                item.sortcode = submitItem.sortcode;
                item.updateid = Convert.ToInt32(SessionHelper.GetSession("userid"));
                item.updatetime = DateTime.Now;
                this.Update(item);
            }
            this.Save();
        }
    }
}
