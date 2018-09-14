using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;

namespace MyTest
{
    class Program
    {
        static void Main(string[] args)
        {
            // Myclass.Message("In Main function.");
            // Test.Function1();



            //OhClass.OldMethod();

            Rectangle r = new Rectangle(4.5, 7.5);
            r.Display();
            Type type = typeof(Rectangle);
            // 遍历 Rectangle 类的特性
            foreach (Object attributes in type.GetCustomAttributes(false))
            {
                DeBugInfo dbi = (DeBugInfo)attributes;
                if (null != dbi)
                {
                    Console.WriteLine("Bug no: {0}", dbi.BugNo);
                    Console.WriteLine("Developer: {0}", dbi.Developer);
                    Console.WriteLine("Last Reviewed: {0}",
                        dbi.LastReview);
                    Console.WriteLine("Remarks: {0}", dbi.Message);
                }
            }

            // 遍历方法特性
            foreach (MethodInfo m in type.GetMethods())
            {
                foreach (Attribute a in m.GetCustomAttributes(true))
                {
                    DeBugInfo dbi = (DeBugInfo)a;
                    if (null != dbi)
                    {
                        Console.WriteLine("Bug no: {0}, for Method: {1}",
                            dbi.BugNo, m.Name);
                        Console.WriteLine("Developer: {0}", dbi.Developer);
                        Console.WriteLine("Last Reviewed: {0}",
                            dbi.LastReview);
                        Console.WriteLine("Remarks: {0}", dbi.Message);
                    }
                }
            }
            Console.ReadLine();
            Console.ReadKey();



        }


    }

    #region c#特性练习之  Conditional（忽略特性<Debug/Trace>）
    public class Myclass
    {
        [Conditional("Trace")]
        public static void Message(string msg)
        {
            Console.WriteLine(msg);
        }
    }

    public static class Test
    {
        public static void Function1()
        {
            Myclass.Message("In Function 1.");
            Function2();
        }
        public static void Function2()
        {
            Myclass.Message("In Function 2.");
        }
    }
    #endregion


    #region c#特性练习之  Obsolete（不应被使用的程序<reson/bool>）
    public static class OhClass
    {
        [Obsolete("Don't use OldMethod, use NewMethod instead", true)]
        public static void OldMethod()
        {
            Console.WriteLine("It is the old method");
        }
        public static void NewMethod()
        {
            Console.WriteLine("It is the new method");
        }
    }
    #endregion


    // 一个自定义特性 BugFix 被赋给类及其成员
    [AttributeUsage(AttributeTargets.Class |
                    AttributeTargets.Constructor |
                    AttributeTargets.Field |
                    AttributeTargets.Method |
                    AttributeTargets.Property,
        AllowMultiple = true)]

    public class DeBugInfo : Attribute
    {
        private int bugNo;
        private string developer;
        private string lastReview;
        public string message;

        public DeBugInfo(int bg, string dev, string d)
        {
            this.bugNo = bg;
            this.developer = dev;
            this.lastReview = d;
        }

        public int BugNo
        {
            get
            {
                return bugNo;
            }
        }
        public string Developer
        {
            get
            {
                return developer;
            }
        }
        public string LastReview
        {
            get
            {
                return lastReview;
            }
        }
        public string Message
        {
            get
            {
                return message;
            }
            set
            {
                message = value;
            }
        }
    }


    [DeBugInfo(45, "Zara Ali", "12/8/2012", Message = "Return type mismatch")]
    [DeBugInfo(49, "Nuha Ali", "10/10/2012", Message = "Unused variable")]
    class Rectangle
    {
        // 成员变量
        protected double length;
        protected double width;
        public Rectangle(double l, double w)
        {
            length = l;
            width = w;
        }
        [DeBugInfo(55, "Zara Ali", "19/10/2012", Message = "Return type mismatch")]
        public double GetArea()
        {
            return length * width;
        }
        [DeBugInfo(56, "Zara Ali", "19/10/2012")]
        public void Display()
        {
            Console.WriteLine("Length: {0}", length);
            Console.WriteLine("Width: {0}", width);
            Console.WriteLine("Area: {0}", GetArea());
        }
    }



    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)] //规定特性类的用途，用于类或者方法
    public class ValidStr : Attribute             //特性类，需要传入的参数  info，msg，以及error
    {
        private string _info;
        private string _msg;

        public string error;
        public ValidStr(string info, string msg)
        {
            this._info = info;
            this._msg = msg;
        }

        public string Info
        {
            get { return _info; }
        }

        public string Msg
        {
            get { return _msg; }
        }

        public string Error
        {
            get { return error; }
            set { error = value; }
        }
    }


    [ValidStr("God", "in sky", error = "not in sky")]
    class GodInSKy
    {

    }

}
