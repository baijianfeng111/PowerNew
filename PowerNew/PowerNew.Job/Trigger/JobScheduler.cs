using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PowerNew.Common;
using PowerNewMain;
using Quartz;
using Quartz.Impl;

namespace PowerNew.Job.Trigger
{

    /// <summary>
    /// 任务调度（把作业，触发器加入调度器）触发操作可以使用Cron表达式
    /// </summary>
    public class ReportJobScheduler
    {
        public static void Start()
        {
            try
            {
                IScheduler scheduler = StdSchedulerFactory.GetDefaultScheduler(); //从工厂中获取一个调度器实例化
                scheduler.Start();   //开始调度器
                IJobDetail job = JobBuilder.Create<ReportJob>().Build();          //创建一个作业Menu
                ITrigger trigger = TriggerBuilder.Create()
                    .WithSimpleSchedule(t =>
                        t.WithIntervalInSeconds(10) //触发执行，60s一次
                            .RepeatForever())          //重复执行
                    .Build();
                scheduler.ScheduleJob(job, trigger);       //把作业，触发器加入调度器。 
            }
            catch (Exception e)
            {
                LogHelper.log.Error(e.Message);
            }

        }
    }
}
