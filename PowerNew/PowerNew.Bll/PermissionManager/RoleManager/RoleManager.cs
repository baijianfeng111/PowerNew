using LinqKit;
using PowerNew.Common;
using PowerNew.Dal;
using PowerNew.Model;

namespace PowerNew.Bll
{
    public class QueryRole : PagerInBase
    {
        public string rolename { get; set; }

    }
    public partial class RoleManager : BaseDal<bjf_role>
    {
        private static RoleManager _instance;
        private RoleManager() { }

        public static RoleManager GetInstance()
        {
            return _instance ?? (_instance = new RoleManager());
        }
        public PagerResult<bjf_role> GetPageList(QueryRole query)
        {
            //拼接sql条件查询-表达式树
            var builder = PredicateBuilder.True<bjf_role>();
            builder = builder.And(m => m.isdelete == false);
            //传参
            if (!string.IsNullOrEmpty(query.rolename))
            {
                builder = builder.And(m => m.rolename == query.rolename);
            }
            var queryinfo = new QueryInfo<bjf_role>()
            {
                wheresql = builder,
                orderby = m => m.updatetime,
                pageindex = query.PageIndex,
                pagesize = query.PageSize
            };
            var list = this.SelectPageList(queryinfo);
            var count = this.GetCountByQuery(queryinfo);
            //返回到视图
            var res = new PagerResult<bjf_role>
            {
                DataList = list,
                Total = count,
                PageSize = query.PageSize,
                PageIndex = query.PageIndex,
                RequestUrl = query.RequetUrl
            };
            return res;
        }
    }
}
