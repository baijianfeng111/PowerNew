using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PowerNew.Dal;
using PowerNew.Model;

namespace PowerNew.Bll
{
    public class UserForRoleManager : BaseDal<bjf_roleforuser>
    {
        private static UserForRoleManager _instance;
        private UserForRoleManager() { }

        public static UserForRoleManager GetInstance()
        {
            return _instance ?? (_instance = new UserForRoleManager());
        }


        /// <summary>
        /// 用户对应的角色集合
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        public List<bjf_roleforuser> GetListRole(int userid)
        {
            return this.SelectList(m => m.isdelete == false && m.userid == userid);
        }
    }
}
