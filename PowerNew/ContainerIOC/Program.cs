using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity;

namespace ContainerIOC
{


    //ioc解释 控制反转/依赖注入（Inversion of Control） 作用：减少程序中耦合



    /// <summary>
    /// Unity IOC容器的简单应用
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            //理解：
            //1.把依赖的创建移到使用这些依赖的类的外部，这称为控制反转模式
            //2.之所以这样命名，是因为反转的是依赖的创建，正因为如此，才消除了消费者类对依赖创建的控制。

            IocTest.LifeBehavior behavior = new IocTest.LifeBehavior();//在外部创建依赖对象
            IocTest.HumenBehavior humen = new IocTest.HumenBehavior(behavior);//通过构造函数注入依赖
            humen.DoSomeThing(1);
            Console.Read();

            ////实例化一个控制器  
            //IUnityContainer unityContainer = new UnityContainer();
            ////实现注入  
            //unityContainer.RegisterType<IBird, ShowSay>("ShowSay");         //--括号里面是别名
            //unityContainer.RegisterType<IBird, NewSay>("NewSay");


            //IBird showsay = unityContainer.Resolve<IBird>("ShowSay");
            //IBird newsay = unityContainer.Resolve<IBird>("NewSay");

            //showsay.Say();
            //newsay.Say();

            //Console.Read();

        }
    }

    public interface IBird
    {
        void Say();
    }

    public class ShowSay : IBird
    {
        public void Say()
        {
            Console.WriteLine("燕子在叫~~~~~~~~~~");
        }
    }

    public class NewSay : IBird
    {
        public void Say()
        {
            Console.WriteLine("麻雀在叫~~~~~~~~~");
        }
    }


}
