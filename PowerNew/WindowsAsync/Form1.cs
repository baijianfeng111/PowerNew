using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using KafkaWindows;
using PowerNew.Common;

namespace WindowsAsync
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btn1_Click(object sender, EventArgs e)
        {
            this.lblname1.Text = One(10).ToString();
            this.lblname2.Text = Two(50).ToString();
        }

        public int One(int a)
        {
            System.Threading.Thread.Sleep(10000);
            int sum = a * a;
            return sum;
        }


        public int Two(int a)
        {
            int sum = a * a;
            return sum;
        }


        public int Three(int a)
        {
            System.Threading.Thread.Sleep(10000);
            int sum = a * a;
            return sum;
        }

        /// <summary>
        /// 定义委托
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        public delegate int Caculate(int x);
        private void btn2_Click(object sender, EventArgs e)
        {

            Caculate a = One;
            //实例化委托
            //var m = new Caculate(One);

            //调用
            var result = a.BeginInvoke(10, null, null);
            string msg = "正在计算,请稍等...";
            this.lblname1.Text = msg;
            this.lblname2.Text = Two(50).ToString();


            var a1 = a.EndInvoke(result);
            this.lblname1.Text = a1.ToString();
        }
    }
}
