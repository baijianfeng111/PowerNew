using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using PowerNew.Bll;

namespace ThreadTask
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                var t = new Thread(new ThreadStart(Update));
                t.Start();
                Console.WriteLine("t线程ID:" + t.ManagedThreadId);

                var t1 = new Thread(new ThreadStart(Update));
                t1.Start();
                Console.WriteLine("t1线程ID:" + t1.ManagedThreadId);

                var t2 = new Thread(new ThreadStart(Update));
                t2.Start();
                Console.WriteLine("t2线程ID:" + t2.ManagedThreadId);
                //Update();
                Console.WriteLine("完毕");
                Console.ReadKey();
            }
            catch (DbUpdateConcurrencyException e)//using System.Data.Entity.Infrastructure;
            {
                e.Entries.Single().Reload();//获取没保存成功的，重新装载一下，把数据库最新版本再装载一下并执行,让并行的都得以执行，不会产生覆盖
            }

        }


        public static void Update()
        {

            var model = ArticleManager.GetInstance().GetModel(1);
            model.readcount += 1;
            model.title = "Good";
            model.updatetime = DateTime.Now;
            ArticleManager.GetInstance().Update(model);
            ArticleManager.GetInstance().Save();
        }
    }
}
