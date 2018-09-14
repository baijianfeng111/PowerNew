using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContainerIOC
{
    public class IocTest
    {
        /// <summary>
        /// 订餐
        /// </summary>
        public class BookFood
        {
            public void BookRice()
            {
                Console.WriteLine("订米饭");
            }
        }

        /// <summary>
        /// 人的动作
        /// </summary>
        public class PeopleBehavior
        {
            public void EatFood()                     //--依赖订餐类
            {
                var book = new BookFood();
                book.BookRice();
            }
        }



        //--------------降低这种耦合(ioc模式)    
        // -->[普通用法]：一个类要是调用另一个类的方法，必须在这个类的内部创建一个【被调用类】的依赖，然后调用里面的方法        
        // -->[控制反转]：把依赖的创建移到使用这些依赖的类的外部
        public interface ILifeBehavior
        {
            void BookFood(); //订餐
            void BookFight();//订票
        }

        public class LifeBehavior : ILifeBehavior
        {
            public void BookFood()
            {
                Console.WriteLine("订饭");
            }

            public void BookFight()
            {
                Console.WriteLine("订票");
            }
        }


        public class HumenBehavior
        {
            private ILifeBehavior _food;

            public HumenBehavior(ILifeBehavior food)
            {
                _food = food;
            }

            public void DoSomeThing(int state)
            {
                if (state == 1)
                {
                    _food.BookFood();
                }
                else
                {
                    _food.BookFight();
                }
            }
        }
    }
}
