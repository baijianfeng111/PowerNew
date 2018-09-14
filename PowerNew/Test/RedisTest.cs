using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NServiceKit.Redis;
using NServiceKit.Redis.Support;
using PowerNew.Common;

namespace Test
{
    [TestClass]
    public class RedisTest
    {
        [TestMethod]
        public void TestMethod3()
        {
            //RedisClient redis = new RedisClient("127.0.0.1", 6379);//redis服务IP和端口
            //AddString(redis);
            //AddHash(redis);
            //AddList(redis);
            //AddSet(redis);
            //List(redis);

        }

        private static void AddString(RedisClient client)
        {
            var timeOut = new TimeSpan(0, 0, 0, 30);
            client.Add("Test", "Learninghard", timeOut);

            if (client.ContainsKey("Test"))
            {
                Console.WriteLine("String Key: Test -Value: {0}, 当前时间: {1}", client.Get<string>("Test"), DateTime.Now);
            }
            else
            {
                Console.WriteLine("Value 已经过期了，当前时间：{0}", DateTime.Now);
            }
            var person = new Person() { Name = "Learninghard", Age = 26 };
            client.Add("lh", person);
            var cachePerson = client.Get<Person>("lh");
            Console.WriteLine("Person's Name is : {0}, Age: {1}", cachePerson.Name, cachePerson.Age);

        }

        private static void AddHash(RedisClient client)
        {
            if (client == null) throw new ArgumentNullException("client");

            client.SetEntryInHash("HashId", "Name", "Learninghard");
            client.SetEntryInHash("HashId", "Age", "26");
            client.SetEntryInHash("HashId", "Sex", "男");

            var hashKeys = client.GetHashKeys("HashId");
            foreach (var key in hashKeys)
            {
                Console.WriteLine("HashId--Key:{0}", key);
            }

            var haskValues = client.GetHashValues("HashId");
            foreach (var value in haskValues)
            {
                Console.WriteLine("HashId--Value:{0}", value);
            }

            var allKeys = client.GetAllKeys(); //获取所有的key。
            foreach (var key in allKeys)
            {
                Console.WriteLine("AllKey--Key:{0}", key);
            }
        }

        private static void AddList(RedisClient client)
        {
            var timeOut = new TimeSpan(0, 0, 0, 30);
            if (!client.ContainsKey("FirstList"))
            {
                var list = new List<Person>();
                var per1 = new Person()
                {
                    Age = 1,
                    Name = "a"
                };
                var per2 = new Person()
                {
                    Age = 2,
                    Name = "b"
                };
                list.Add(per1);
                list.Add(per2);
                client.Set<List<Person>>("FirstList", list, timeOut);
            }
            var newlist = client.Get<List<Person>>("FirstList");
            foreach (var item in newlist)
            {
                var index = newlist.IndexOf(item);
                Console.WriteLine("集合FirstList{0}的值:Age:{1},Name:{2}", index, item.Age, item.Name);
            }
        }

        //它是string类型的无序集合。set是通过hash table实现的，添加，删除和查找,对集合我们可以取并集，交集，差集
        private static void AddSet(RedisClient client)
        {
            if (client == null) throw new ArgumentNullException("client");

            client.AddItemToSet("Set1001", "A");
            client.AddItemToSet("Set1001", "B");
            client.AddItemToSet("Set1001", "C");
            client.AddItemToSet("Set1001", "D");
            var hastset1 = client.GetAllItemsFromSet("Set1001");
            foreach (var item in hastset1)
            {
                Console.WriteLine("Set无序集合Value:{0}", item); //出来的结果是无须的
            }

            client.AddItemToSet("Set1002", "K");
            client.AddItemToSet("Set1002", "C");
            client.AddItemToSet("Set1002", "A");
            client.AddItemToSet("Set1002", "J");
            var hastset2 = client.GetAllItemsFromSet("Set1002");
            foreach (var item in hastset2)
            {
                Console.WriteLine("Set无序集合ValueB:{0}", item); //出来的结果是无须的
            }

            var hashUnion = client.GetUnionFromSets(new string[] { "Set1001", "Set1002" });
            foreach (var item in hashUnion)
            {
                Console.WriteLine("求Set1001和Set1002的并集:{0}", item); //并集
            }

            var hashG = client.GetIntersectFromSets(new string[] { "Set1001", "Set1002" });
            foreach (var item in hashG)
            {
                Console.WriteLine("求Set1001和Set1002的交集:{0}", item);  //交集
            }

            var hashD = client.GetDifferencesFromSet("Set1001", new string[] { "Set1002" });  //[返回存在于第一个集合，但是不存在于其他集合的数据。差集]
            foreach (var item in hashD)
            {
                Console.WriteLine("求Set1001和Set1002的差集:{0}", item);  //差集
            }

        }

        /*
        sorted set 是set的一个升级版本，它在set的基础上增加了一个顺序的属性，这一属性在添加修改.元素的时候可以指定，
        * 每次指定后，zset(表示有序集合)会自动重新按新的值调整顺序。可以理解为有列的表，一列存 value,一列存顺序。操作中key理解为zset的名字.
        */
        private static void AddSetSorted(RedisClient client)
        {
            if (client == null) throw new ArgumentNullException("client");

            client.AddItemToSortedSet("SetSorted1001", "A");
            client.AddItemToSortedSet("SetSorted1001", "B");
            client.AddItemToSortedSet("SetSorted1001", "C");
            var listSetSorted = client.GetAllItemsFromSortedSet("SetSorted1001");
            foreach (var item in listSetSorted)
            {
                Console.WriteLine("SetSorted有序集合{0}", item);
            }

            client.AddItemToSortedSet("SetSorted1002", "A", 400);
            client.AddItemToSortedSet("SetSorted1002", "D", 200);
            client.AddItemToSortedSet("SetSorted1002", "B", 300);

            // 升序获取第一个值:"D"
            var list = client.GetRangeFromSortedSet("SetSorted1002", 0, 0);

            foreach (var item in list)
            {
                Console.WriteLine(item);
            }

            //降序获取第一个值:"A"
            list = client.GetRangeFromSortedSetDesc("SetSorted1002", 0, 0);

            foreach (var item in list)
            {
                Console.WriteLine(item);
            }
        }

        public static void List(RedisClient redis)
        {
            var ser = new ObjectSerializer();
            List<Person> userinfoList = new List<Person> {  
                new Person{Name="露西",Age=1},  
                new Person{Name="玛丽",Age=3},  
            };
            redis.Set<byte[]>("userinfolist_serialize", ser.Serialize(userinfoList));
            List<Person> userList = ser.Deserialize(redis.Get<byte[]>("userinfolist_serialize")) as List<Person>;
            userList.ForEach(i =>
            {
                Console.WriteLine("name=" + i.Name + "   age=" + i.Age);
            });
            Console.WriteLine(userList);
            Console.ReadLine();
            //释放内存  
        }
        [Serializable]
        class Person
        {
            public string Name { get; set; }
            public int Age { get; set; }
        }



    }
}
