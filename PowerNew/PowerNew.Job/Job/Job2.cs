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
    public class Job2 : IJob
    {

        /// <summary>
        /// 任务执行的内容
        /// </summary>
        /// <param name="context"></param>
        public void Execute(IJobExecutionContext context)//必须实现IJob接口下的Execute方法
        {
            try
            {
                var menuitem = new bjf_block()
                {
                    blockname = "任务调度",
                    isuse = 1,
                    sortcode = new Random().Next(22, 100),
                    createid = 1,
                    createtime = DateTime.Now,
                    updateid = 1,
                    updatetime = DateTime.Now,
                };
                BlockManager.GetInstance().Add(menuitem);
                BlockManager.GetInstance().Save();
                LogHelper.log.Error("定时任务执行成功");
            }
            catch (Exception e)
            {
                LogHelper.log.Error(e.Message);
            }

        }
    }
}
