using System;
using PowerNew.Common;
using PowerNew.Model;

namespace PowerNew.Bll
{
    public partial class LoginLogManager
    {
        public void SaveItem()
        {
            var submitItem = new bjf_loginlog();
            submitItem.ip = OpenHelper.GetIp();
            submitItem.userid = Convert.ToInt32(SessionHelper.GetSession("userid"));
            submitItem.logintime = DateTime.Now;
            submitItem.createtime=DateTime.Now;
            submitItem.updatetime = DateTime.Now;
            this.Add(submitItem);
            this.Save();
        }
    }
}
