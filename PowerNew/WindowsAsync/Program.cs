using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsAsync
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {



            //1.----------------主程序入口
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());

            //2.----------------获取用户的磁盘集合
            //foreach (DriveInfo driver in System.IO.DriveInfo.GetDrives())
            //{
            //    Console.WriteLine(driver.Name);
            //}

            //3.----------------ref 和 out 的用法

            //--------------------------------ref
            //Gump doit = new Gump();
            //double a = 3;
            //double b = 0;
            //Console.WriteLine("Before a={0},b={1}", a, b);
            //b = doit.Square(ref a);
            //Console.WriteLine("After a={0},b={1}", a, b);
            //Console.ReadLine();

            //--------------------------------out
            //GumpOut doit = new GumpOut();
            //double x1 = 600;
            //double cubed1 = 0;
            //double squared1 = 0;
            //double half1 = 0;

            //Console.WriteLine("Before method->x1={0}", x1);
            //Console.WriteLine("Before method->half1={0}", half1);
            //Console.WriteLine("Before method->squared1={0}", squared1);
            //Console.WriteLine("Before method->cubed1={0}", cubed1);

            //doit.math_routines(x1, out half1, out squared1, out cubed1);

            //Console.WriteLine("After method->x1={0}", x1);
            //Console.WriteLine("After method->half1={0}", half1);
            //Console.WriteLine("After method->squared1={0}", squared1);
            //Console.WriteLine("After method->cubed1={0}", cubed1);

            //Console.ReadLine();

        }

        class Gump
        {
            public double Square(ref double x)
            {
                x = x * x;
                return x;
            }
        }

        class GumpOut
        {
            //可以是：public void math_rotines(//ref double x,out double half,out double squared,out double cubed)
            //但是，不可以这样：public void math_routines(out double x,out double half,out double squared,out double cubed)
            //对本例子来说，因为输出的值要靠X赋值，所以X不能再为输出值
            public void math_routines(double x, out double half, out double squared, out double cubed)
            {
                half = x / 2;
                squared = x * x;
                cubed = x * x * x;
            }
        }
    }
}
