using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Linq.Expressions;
using PowerNew.DAL;
using PowerNew.Model;

namespace PowerNew.Dal
{
    public class BaseDal<T> where T : class,new()
    {
        //实例化EF上下文容器对象
        private DbContext db = DbContextFactory.CreateDbContext();

        /// <summary>
        /// 用于执行增删改的泛型实体集
        /// </summary>
        DbSet<T> _dbset;


        public BaseDal()
        {
            this._dbset = db.Set<T>();
        }

        /// <summary>
        /// 查询单个实体
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        public T SelectOne(Expression<Func<T, bool>> where)
        {
            return _dbset.Where(where).AsNoTracking().SingleOrDefault();

        }

        /// <summary>
        /// 查询实体集合
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        public List<T> SelectList(Expression<Func<T, bool>> where)
        {
            return _dbset.Where(where.Compile()).AsQueryable().ToList();
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

            return _dbset.Where(where.Compile()).AsQueryable().OrderByDescending(orderby).FirstOrDefault();
        }


        /// <summary>
        /// lamada分页搜索
        /// </summary>
        /// <param name="query"></param>
        /// <param name="isdesc"></param>
        /// <returns></returns>
        public List<T> SelectPageList(QueryInfo<T> query, bool isdesc = true)
        {
            if (isdesc)
            {
                return _dbset.AsNoTracking()
                    .Where(query.wheresql.Compile())
                    .AsQueryable()
                    .OrderByDescending(query.orderby)
                    .Skip((query.pageindex - 1) * query.pagesize)
                    .Take(query.pagesize)
                    .ToList();
            }
            else
            {
                return _dbset.AsNoTracking()
                    .Where(query.wheresql.Compile())
                    .AsQueryable()
                    .OrderBy(query.orderby)
                    .Skip((query.pageindex - 1) * query.pagesize)
                    .Take(query.pagesize)
                    .ToList();
            }
        }

        public int GetCountByQuery(QueryInfo<T> query)
        {
            return _dbset.AsNoTracking().Where(query.wheresql.Compile()).AsQueryable().Count();
        }
        /// <summary>
        /// 执行添加
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public void Add(T model)
        {
            _dbset.Add(model);
        }

        /// <summary>
        /// 批量添加
        /// </summary>
        /// <param name="listmodel"></param>
        public void AddRange(List<T> listmodel)
        {

            foreach (var model in listmodel)
            {
                _dbset.Add(model);
            }
        }

        /// <summary>
        /// 执行修改
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public void Update(T model)
        {

            _dbset.AddOrUpdate(model);
            //_dbset.Attach(model);
            //db.Entry<T>(model).State = System.Data.Entity.EntityState.Modified;
        }

        public void UpdateNew(T model)
        {

            _dbset.AddOrUpdate(model);
        }

        public void ExcuteSql(string sql)
        {
            using (var newdb = new powerbjfEntities())
            {
                newdb.Database.ExecuteSqlCommand(sql);
            }
        }


        /// <summary>
        /// 真正删除
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public void Remove(T model)
        {

            _dbset.Remove(model);

        }
        public void RemoveList(List<T> list)
        {

            foreach (var model in list)
            {
                _dbset.Remove(model);
            }
        }
        public bool Save()
        {
            return db.SaveChanges() > 0 ? true : false;
        }
    }

    /// <summary>
    /// 分页帮助类（搜索条件，排序依据，第几页，每页大小）
    /// </summary>
    /// <typeparam name="T1"></typeparam>
    public class QueryInfo<T1>
    {
        public Expression<Func<T1, bool>> wheresql;
        public Expression<Func<T1, Object>> orderby;
        public int pageindex { get; set; }
        public int pagesize { get; set; }
    }
}
