using System.Linq.Expressions;
using LinqKit;
using PowerNew.Common;
using PowerNew.Dal;
using PowerNew.Model;

namespace PowerNew.Bll
{
    public class QueryAccount : PagerInBase
    {
        public string Name { get; set; }
        public string DbServer { get; set; }
    }

    public partial class AccountManager : BaseDal<bjf_account>
    {
        private static AccountManager _Instance;
        private AccountManager() { }

        public static AccountManager GetInstance()
        {
            return _Instance ?? (_Instance = new AccountManager());
        }

        public PagerResult<bjf_account> GetPageList(QueryAccount query)
        {

            var queryinfo = new QueryInfo<bjf_account>()
            {
                wheresql = this.LinqSql(query),
                orderby = m => m.updatetime,
                pageindex = query.PageIndex,
                pagesize = query.PageSize
            };
            var list = this.SelectPageList(queryinfo);
            var count = this.GetCountByQuery(queryinfo);
            //返回到视图
            var res = new PagerResult<bjf_account>
            {
                DataList = list,
                Total = count,
                PageSize = query.PageSize,
                PageIndex = query.PageIndex,
                RequestUrl = query.RequetUrl
            };
            return res;
        }

        //拼接sql条件查询-表达式树
        public Expression<System.Func<bjf_account, bool>> LinqSql(QueryAccount query)
        {

            var builder = PredicateBuilder.True<bjf_account>();
            builder = builder.And(m => m.isdelete == false);
            //传参
            if (!string.IsNullOrEmpty(query.Name))
            {
                builder = builder.And(m => m.name == query.Name);
            }
            if (!string.IsNullOrEmpty(query.DbServer))
            {
                builder = builder.And(m => m.dbserver == query.DbServer);
            }
            return builder;
        }
    }
}
