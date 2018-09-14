using System;
using System.Net.Mail;
using System.Threading;
using KafKaHelper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PowerNew.Bll;
using PowerNew.Common;
using Rym.MonthWage.Bll;

namespace Test
{
    [TestClass]
    public class AbstractTest
    {
        [TestMethod]
        public void TestMethod1()
        {

            var c=new C();
            c.E();
            c.M();
            c.Num = 1;
            c.getNum();
            c.setNum(1);
            //for (int i = 0; i < 10; i++)
            //{
            //    var info = new SendMailManager.MailInfo(new MailAddress("806078508@qq.com"), "2225199512@qq.com", "激活", "激活了，谢谢");
            //    var topic = "emailtopic";
            //    KafkaHelper.ProduceMsg(topic, info);
            //    Console.WriteLine("成功");  
            //}
            //for (int i = 0; i < 50; i++)
            //{
            //    var model = ArticleManager.GetInstance().GetModel(1);
            //    model.readcount += 1;
            //    ArticleManager.GetInstance().Update(model);
            //}
        }

        public abstract class A //抽象类A 
        {
            private int num = 0;
            public int Num //抽象类包含属性 
            {
                get
                {
                    return num;
                }
                set
                {
                    num = value;
                }
            }
            public virtual int getNum() //抽象类包含虚方法 
            {
                return num;
            }
            public void setNum(int n) // //抽象类包含普通方法 
            {
                this.num = n;
            }
            public abstract void E(); //类A中的抽象方法E  
        }
        public abstract class B : A //由于类B继承了类A中的抽象方法E,所以类B也变成了抽象类 
        {
            public override void E()
            {
                throw new NotImplementedException();
            }

            public abstract void M();
        }
        public class C : B
        {
            public override void M()
            {
                throw new NotImplementedException();
            }
        }

        public class D : B
        {

            public override void M()
            {
                throw new NotImplementedException();
            }

        }

        

        public class Info
        {
            public int Age { get; set; }
            public int Sex { get; set; }
            public string Name { get; set; }
            public string EnglishName { get; set; }
        }

    }
}
