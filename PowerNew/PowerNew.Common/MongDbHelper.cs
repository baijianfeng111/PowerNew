using System.Collections.Generic;
using MongoDB;

namespace PowerNew.Models
{
    //public class UserModel
    //{
    //    //链接字符串(此处三个字段值根据需要可为读配置文件)
    //    public string connectionString = "Server=127.0.0.1:27017";
    //    //数据库名
    //    public string databaseName = "bjf";
    //    //集合名 
    //    public string collectionName = "userCollection";

    //    private Mongo mongo;
    //    private MongoDatabase mongoDatabase;
    //    private MongoCollection<Document> mongoCollection;

    //    public UserModel()
    //    {
    //        mongo = new Mongo(connectionString);
    //        mongoDatabase = mongo.GetDatabase(databaseName) as MongoDatabase;
    //        mongoCollection = mongoDatabase.GetCollection<Document>(collectionName) as MongoCollection<Document>;
    //        mongo.Connect();
    //    }


    //    public void Add(Document doc)
    //    {
    //        mongoCollection.Insert(doc);
    //    }


    //    public void Delete(string UserId)
    //    {
    //        mongoCollection.Remove(new Document { { "UserId", UserId } });
    //    }

    //    public void Update(Document doc)
    //    {
    //        mongoCollection.FindAndModify(doc, new Document { { "UserId", doc["UserId"].ToString() } });
    //    }
    //    public IEnumerable<Document> FindAll()
    //    {
    //        return mongoCollection.FindAll().Documents;
    //    }

    //}
}