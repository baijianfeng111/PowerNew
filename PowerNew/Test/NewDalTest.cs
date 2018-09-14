using System;
using System.Collections.Generic;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PowerNew.Dal;
using PowerNew.Model;

namespace Test
{
    [TestClass]
    public class NewDalTest
    {
        [TestMethod]
        public void TestMethod()
        {
            var m = MathManager.GetDistance(116.38156, 39.906301, 116.441926, 39.948131);
            Console.WriteLine(m);
        }


        [TestMethod]
        public void TestMethod1()
        {
            new NewBllManager().AddAll();
            Console.WriteLine("ok");
        }

        [TestMethod]
        public void TestMethod2()
        {
            for (int i = 0; i < 10; i++)
            {
                new OldBllManager().GetItem(1);
            }
            

          //  new MyTests().RunTest();
        }

        public class MyTests
        {

            public void RunTest()
            {
                Thread th1 = new Thread(ShowNum);
                Thread th2 = new Thread(ShowNum);
                Thread th3 = new Thread(ShowNum);

                th1.Start('1');
                th2.Start('1');
                th3.Start('1');
            }

            private void ShowNum(object name)
            {
                for (int i = 0; i < 3; i++)
                {
                    new OldBllManager().GetItem(Convert.ToInt32(name));
                }

            }
        }



        public class MathManager
        {
            //地球半径，单位米
            private const double EARTH_RADIUS = 6378137;

            /// <summary>
            /// 计算两点位置的距离，返回两点的距离，单位：米
            /// 该公式为GOOGLE提供，误差小于0.2米
            /// </summary>
            /// <param name="lng1">第一点经度</param>
            /// <param name="lat1">第一点纬度</param>        
            /// <param name="lng2">第二点经度</param>
            /// <param name="lat2">第二点纬度</param>
            /// <returns></returns>
            public static double GetDistance(double lng1, double lat1, double lng2, double lat2)
            {
                double radLat1 = Rad(lat1);
                double radLng1 = Rad(lng1);
                double radLat2 = Rad(lat2);
                double radLng2 = Rad(lng2);
                double a = radLat1 - radLat2;
                double b = radLng1 - radLng2;
                double result = 2 * Math.Asin(Math.Sqrt(Math.Pow(Math.Sin(a / 2), 2) +
                                                        Math.Cos(radLat1) * Math.Cos(radLat2) *
                                                        Math.Pow(Math.Sin(b / 2), 2))) * EARTH_RADIUS;
                return result;
            }

            /// <summary>
            /// 经纬度转化成弧度
            /// </summary>
            /// <param name="d"></param>
            /// <returns></returns>
            private static double Rad(double d)
            {
                return (double)d * Math.PI / 180d;
            }
        }





        public class OldBllManager : BaseDal<bjf_article>
        {
            public void GetItem(int id)
            {
                var model= SelectOne(m => m.isdelete == false && m.id == id);
                //model.readcount = 1;
                //Update(model);
                //Save();
            }

            public void AddAll()
            {
                var item = new bjf_article()
                {
                    content = "11",
                    createid = 1,
                    createtime = DateTime.MaxValue,
                    readcount = 0,
                    title = "C#",
                    updateid = 1,
                    updatetime = DateTime.MaxValue,
                };
                var item1 = new bjf_article()
                {
                    content = "11",
                    createid = 1,
                    createtime = DateTime.MaxValue,
                    readcount = 0,
                    title = "Java",
                    updateid = 1,
                    updatetime = DateTime.MaxValue,
                };
                var list = new List<bjf_article>();
                list.Add(item);
                list.Add(item1);
                this.AddRange(list);
            }

        }






        public class NewBllManager : NewBaseDal<bjf_article>
        {
            public bjf_article GetItem()
            {
                return SelectOne(m => m.isdelete == false && m.id == 1);
            }

            public void AddAll()
            {
                var item = new bjf_article()
                {
                    content = "11",
                    createid = 1,
                    createtime = DateTime.MaxValue,
                    readcount = 0,
                    title = "C#",
                    updateid = 1,
                    updatetime = DateTime.MaxValue,
                };
                var item1 = new bjf_article()
                {
                    content = "11",
                    createid = 1,
                    createtime = DateTime.MaxValue,
                    readcount = 0,
                    title = "Java",
                    updateid = 1,
                    updatetime = DateTime.MaxValue,
                };
                var list = new List<bjf_article>();
                list.Add(item);
                list.Add(item1);
                this.AddRange(list);
            }

        }
    }
}
