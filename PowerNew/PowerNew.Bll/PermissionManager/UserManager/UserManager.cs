using LinqKit;
using PowerNew.Common;
using PowerNew.Dal;
using PowerNew.Model;

namespace PowerNew.Bll
{
    public class QueryUser : PagerInBase
    {
        public string loginname { get; set; }
    }
    public partial class UserManager : BaseDal<bjf_user>
    {
        private static UserManager _instance;
        private UserManager() { }

        public static UserManager GetInstance()
        {
            return _instance ?? (_instance = new UserManager());
        }

        public PagerResult<bjf_user> GetPageList(QueryUser query)
        {
            //拼接sql条件查询-表达式树
            var builder = PredicateBuilder.True<bjf_user>();
            builder = builder.And(m => m.isdelete == false);
            //传参
            if (!string.IsNullOrEmpty(query.loginname))
            {
                builder = builder.And(m => m.loginname == query.loginname);
            }
            var queryinfo = new QueryInfo<bjf_user>()
            {
                wheresql = builder,
                orderby = m => m.updatetime,
                pageindex = query.PageIndex,
                pagesize = query.PageSize
            };
            var list = this.SelectPageList(queryinfo);
            var count = this.GetCountByQuery(queryinfo);
            //返回到视图
            var res = new PagerResult<bjf_user>
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
