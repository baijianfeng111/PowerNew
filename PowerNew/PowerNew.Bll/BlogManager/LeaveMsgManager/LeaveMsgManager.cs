using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PowerNew.Dal;
using PowerNew.Model;

namespace PowerNew.Bll
{
    public class LeaveMsgManager : BaseDal<bjf_leavemessage>
    {
        private static LeaveMsgManager _instance;
        private LeaveMsgManager() { }

        public static LeaveMsgManager GetInstance()
        {
            return _instance ?? (_instance = new LeaveMsgManager());
        }


        public void SaveMsg(bjf_leavemessage item)
        {
            this.Add(item);
            this.Save();
        }
    }
}
