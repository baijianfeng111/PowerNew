using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using PowerNew.Bll;
using PowerNew.Common;
using PowerNew.Model;
using Quartz;

namespace PowerNew.Job
{
    public class ReportJob : IJob
    {

        /// <summary>
        /// 任务执行的内容
        /// </summary>
        /// <param name="context"></param>
        public void Execute(IJobExecutionContext context)//必须实现IJob接口下的Execute方法
        {
            try
            {
                var menuitem = new bjf_menu()
                {
                    menuname = "任务调度",
                    menucode = "A" + Guid.NewGuid(),
                    menuhref = "www.baidu.com",
                    createid = 1,
                    createtime = DateTime.Now,
                    updateid = 1,
                    updatetime = DateTime.Now,
                };
                MenuManager.GetInstance().Add(menuitem);
                MenuManager.GetInstance().Save();
                LogHelper.log.Error("定时任务执行成功");
            }
            catch (Exception e)
            {
                LogHelper.log.Error(e.Message);
            }

        }
    }
}
