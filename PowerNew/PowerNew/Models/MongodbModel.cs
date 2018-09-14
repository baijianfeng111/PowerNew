using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MongoDB;
using System.Web.Script.Serialization;

namespace PowerNew.Models
{

    public class DBcon
    {
        public const string _connectionString = "Server=127.0.0.1:27017";

        public const string _database = "runoob";


        public static void AddVedio()
        {
            using (var mg = new Mongo(DBcon._connectionString))
            {
                mg.Connect();
                var db = mg.GetDatabase(DBcon._database);
                var list = db.GetCollection<NewModel>();
                var model = new NewModel()
                {
                    Age = 2,
                    Name = "柏建峰2"
                };
                list.Insert(model);
            }
        }

        public static void Select()
        {
            using (var mg = new Mongo(DBcon._connectionString))
            {
                mg.Connect();
                var db = mg.GetDatabase(DBcon._database);
                var list = db.GetCollection<NewModel>();
                var newlist = list.FindAll().Documents;

                if (newlist != null)
                {
                    var query = from u in newlist
                                select new
                                {
                                    cell = new NewModel()
                                    {
                                        Age = u.Age,
                                        Name = u.Name
                                    }
                                };
                    var newmodel = query.ToList()[0].cell;
                    Console.WriteLine(new JavaScriptSerializer().Serialize(newmodel));
                }
                else { Console.WriteLine("没有任何消息"); }
            }
        }

        public static void SelectOne()
        {
            using (var mg = new Mongo(DBcon._connectionString))
            {
                mg.Connect();
                var db = mg.GetDatabase(DBcon._database);
                var list = db.GetCollection<NewModel>();
                var newlist = list.FindOne(new Document() { { "Name", "柏建峰2" } });
                Console.WriteLine(newlist.Age + "," + newlist.Name);

            }
        }

        public static void Update()
        {
            using (var mg = new Mongo(DBcon._connectionString))
            {
                var model = new NewModel()
                {
                    Age = 1,
                    Name = "修改过"
                };
                mg.Connect();
                var db = mg.GetDatabase(DBcon._database);
                var list = db.GetCollection<NewModel>();
                list.Update(model, new Document() { { "Age", model.Age } });
            }
        }

        public static void Delete()
        {
            using (var mg = new Mongo(DBcon._connectionString))
            {
                mg.Connect();
                var db = mg.GetDatabase(DBcon._database);
                var list = db.GetCollection<NewModel>();
                list.Remove(new Document() { { "Name", "柏建峰2" } });
            }
        }
    }

    public class NewModel
    {
        public int Age { get; set; }
        public string Name { get; set; }
    }
}
