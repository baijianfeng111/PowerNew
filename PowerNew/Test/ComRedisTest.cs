using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PowerNew.Common;

namespace Test
{
    [TestClass]
    public class ComRedisTest
    {
        [TestMethod]
        public void TestMethod1()
        {
            //RedisManager.ChooseDb(5);
            //var database = RedisManager.GetDataBase();
            //RedisManager.SetText("1", "1");
            RedisManager.SetText("1", "2");
            // Console.WriteLine(database);
            //if (!RedisManager.GetAllKeys().Contains("NewFirst1"))
            //{
            //    var name = new People()
            //    {
            //        Age = 1,
            //        Name = "A",
            //        Time = DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss")
            //    };
            //    RedisManager.SetEntity<People>("NewFirst", name, 30);
            //}
            //var item = RedisManager.GetEntities<People>("NewFirst");
            //Console.WriteLine("NewFirst的值：Age:{0},Name：{1},Time:{2}", item.Age, item.Name, item.Time);
        }

        [TestMethod]
        public void TestRemove()
        {

            RedisManager.Clear();
        }

        public class People
        {
            public int Age { get; set; }
            public string Name { get; set; }
            public string Time { get; set; }
        }
    }
}
