using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NServiceKit.Text;
using PowerNew.Bll;

namespace Test
{
    [TestClass]
    public class ArrayAndHashTest
    {
        [TestMethod]
        public void TestMethod1()
        {

            //重复不添加
            var hashlist = new HashSet<int>();
            hashlist.Add(3);
            hashlist.Add(2);
            hashlist.Add(1);
            hashlist.Add(1);
            string a = String.Join(",", hashlist);
            Console.WriteLine(a);


            //重复也添加
            var list = new List<int>();
            list.Add(3);
            list.Add(2);
            list.Add(1);
            list.Add(1);
            string b = String.Join(",", list);
            Console.WriteLine(b);
        }
    }
}
