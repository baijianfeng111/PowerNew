using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Linq.Expressions;
using System.Web.Configuration;
using PowerNew.Model;

namespace PowerNew.Dal
{
    public class NewBaseDal<T> where T : class,new()
    {
        //数据库连接字符串 appsetting和connectionstrings的获取方法

        //private string conn = ConfigurationManager.AppSettings["connstr"];
        private string conn = WebConfigurationManager.ConnectionStrings["powerbjfEntities"].ToString();

        /// <summary>
        /// 查询单个实体
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        public T SelectOne(Expression<Func<T, bool>> where)
        {
            using (var db = new powerbjfEntities(conn))
            {
                return db.Set<T>().Where(where).AsNoTracking().SingleOrDefault();
            }
        }

        /// <summary>
        /// 查询实体集合
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        public List<T> SelectList(Expression<Func<T, bool>> where)
        {
            using (var db = new powerbjfEntities(conn))
            {
                return db.Set<T>().Where(where.Compile()).AsQueryable().ToList();
            }
        }

        /// <summary>
        /// 查询集合中排序后的第一个
        /// </summary>
        /// <param name="where"></param>
        /// <param name="orderby"></param>
        /// <returns></
        /// returns>
        public T SelectFirst(Expression<Func<T, bool>> where, Expression<Func<T, Object>> orderby)
        {
            using (var db = new powerbjfEntities(conn))
            {
                return db.Set<T>().Where(where.Compile()).AsQueryable().OrderByDescending(orderby).FirstOrDefault();
            }
        }


        /// <summary>
        /// lamada分页搜索
        /// </summary>
        /// <param name="query"></param>
        /// <param name="isdesc"></param>
        /// <returns></returns>
        public List<T> SelectPageList(QueryInfo<T> query, bool isdesc = true)
        {
            using (var db = new powerbjfEntities(conn))
            {
                if (isdesc)
                {
                    return db.Set<T>().AsNoTracking()
                        .Where(query.wheresql.Compile())
                        .AsQueryable()
                        .OrderByDescending(query.orderby)
                        .Skip((query.pageindex - 1) * query.pagesize)
                        .Take(query.pagesize)
                        .ToList();
                }
                else
                {
                    return db.Set<T>().AsNoTracking()
                        .Where(query.wheresql.Compile())
                        .AsQueryable()
                        .OrderBy(query.orderby)
                        .Skip((query.pageindex - 1) * query.pagesize)
                        .Take(query.pagesize)
                        .ToList();
                }
            }

        }

        public int GetCountByQuery(QueryInfo<T> query)
        {
            using (var db = new powerbjfEntities(conn))
            {
                return db.Set<T>().AsNoTracking().Where(query.wheresql.Compile()).AsQueryable().Count();
            }
        }
        /// <summary>
        /// 执行添加
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public void Add(T model)
        {
            using (var db = new powerbjfEntities(conn))
            {
                db.Set<T>().Add(model);
                db.SaveChanges();
            }
        }

        /// <summary>
        /// 批量添加
        /// </summary>
        /// <param name="listmodel"></param>
        public void AddRange(List<T> listmodel)
        {
            using (var db = new powerbjfEntities(conn))
            {
                foreach (var model in listmodel)
                {
                    db.Set<T>().Add(model);
                }
                db.SaveChanges();
            }

        }

        /// <summary>
        /// 执行修改
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public void Update(T model)
        {
            using (var db = new powerbjfEntities(conn))
            {
                db.Set<T>().AddOrUpdate(model);
                db.SaveChanges();
            }
        }

        public void UpdateList(List<T> list)
        {
            using (var db = new powerbjfEntities(conn))
            {
                foreach (var model in list)
                {
                    db.Set<T>().AddOrUpdate(model);
                }
                db.SaveChanges();
            }
        }

        public void ExcuteSql(string sql)
        {
            using (var db = new powerbjfEntities(conn))
            {
                db.Database.ExecuteSqlCommand(sql);
                db.SaveChanges();
            }
        }


        /// <summary>
        /// 真正删除
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public void Remove(T model)
        {
            using (var db = new powerbjfEntities(conn))
            {
                db.Set<T>().Remove(model);
                db.SaveChanges();
            }

        }
        public void RemoveList(List<T> list)
        {
            using (var db = new powerbjfEntities(conn))
            {
                foreach (var model in list)
                {
                    db.Set<T>().Remove(model);
                }
                db.SaveChanges();
            }
        }
    }
}
