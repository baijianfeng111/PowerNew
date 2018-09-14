using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PowerNew.Common;
using PowerNew.Model;

namespace PowerNew.Bll
{
    public partial class UserManager
    {
        public bjf_user GetItemById(int id)
        {
            return this.SelectOne(m => m.isdelete == false && m.id == id) ?? new bjf_user();
        }

        public bjf_user GetItemByLoginName(string loginname)
        {
            return this.SelectOne(m => m.loginname == loginname && m.isdelete == false);
        }

        public void SaveItem(bjf_user submitItem)
        {
            if (submitItem.id == 0)
            {
                submitItem.createid = 1;
                submitItem.updateid = 1;
                submitItem.createtime = DateTime.Now;
                submitItem.updatetime = DateTime.Now;
                submitItem.openid = OpenHelper.CreateOpenId();
                submitItem.password = EncryAndDecryptHelper.Encryption(submitItem.password);
                submitItem.idcard = EncryAndDecryptHelper.Encryption(submitItem.idcard);
                submitItem.mobile = EncryAndDecryptHelper.Encryption(submitItem.mobile);
                this.Add(submitItem);
            }
            else
            {
                var item = this.GetItemById(submitItem.id);
                item.loginname = submitItem.loginname;
                item.username = submitItem.username;
                item.password = EncryAndDecryptHelper.Encryption(submitItem.password);
                item.idcard = EncryAndDecryptHelper.Encryption(submitItem.idcard);
                item.email = submitItem.email;
                item.mobile = EncryAndDecryptHelper.Encryption(submitItem.mobile);
                item.comment = submitItem.password;
                item.isadmin = submitItem.isadmin;
                item.updateid = 1;
                item.updatetime = DateTime.Now;
                this.Update(item);
            }
            this.Save();
        }
    }
}
