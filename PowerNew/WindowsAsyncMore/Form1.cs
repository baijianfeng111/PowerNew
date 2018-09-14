using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsAsyncMore
{

    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            this.objca = new Caculate(One);
        }


        public delegate int Caculate(int a, int ms);

        Caculate objca = null;


        /// <summary>
        /// 同步执行
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnold_Click(object sender, EventArgs e)
        {
            for (int i = 1; i < 11; i++)
            {
                int sum = One(10 * i, 1000 * i);
                Console.WriteLine(string.Format("第{0}个运算结果为{1}", i, sum));
            }
        }

        /// <summary>
        /// 异步执行
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            for (int i = 1; i < 11; i++)
            {
                objca.BeginInvoke(10 * i, 1000 * i, MyCallBack, i);
            }
        }

        /// <summary>
        /// 回调函数
        /// </summary>
        /// <param name="result"></param>
        private void MyCallBack(IAsyncResult result)
        {
            int res = objca.EndInvoke(result);
            Console.WriteLine(string.Format("第{0}个运算结果为{1}", result.AsyncState, res));
        }


        public int One(int a, int ms)
        {
            System.Threading.Thread.Sleep(ms);
            return a * a;
        }
    }
}
