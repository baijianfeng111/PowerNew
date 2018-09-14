using System.Data.Entity;
using System.Runtime.Remoting.Messaging;
using System.Web;
using PowerNew.Model;

namespace PowerNew.DAL
{
    /// <summary>
    /// EF上下文对象创建工厂
    /// </summary>
    public class DbContextFactory
    {
        /// <summary>
        /// EF上下文对象,已存在就直接取,不存在就创建,保证线程内是唯一。
        /// 每次请求对应一个上下文
        /// </summary>
        public static DbContext CreateDbContext()
        {

            DbContext dbContext = (DbContext)CallContext.GetData("powerbjfEntities");
            if (dbContext == null)
            {
                dbContext = new powerbjfEntities();
                dbContext.Configuration.ValidateOnSaveEnabled = false;
                CallContext.SetData("powerbjfEntities", dbContext);
            }
            return dbContext;
        }

        //public static DbContext CreateDbContext()
        //{
        //    DbContext dbContext = CallContext.GetData("powerbjfEntities") as DbContext;
        //    if (dbContext == null)
        //    {
        //        dbContext = new powerbjfEntities();
        //        CallContext.SetData("powerbjfEntities", dbContext);
        //    }
        //    return dbContext;
        //}
    }
}
