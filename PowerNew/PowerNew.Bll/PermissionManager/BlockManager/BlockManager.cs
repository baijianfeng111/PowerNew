using System.Linq.Expressions;
using LinqKit;
using PowerNew.Model;
using PowerNew.Common;
using PowerNew.Dal;

namespace PowerNew.Bll
{

    public class QueryBlock : PagerInBase
    {
        public string blockname { get; set; }
    }
    public partial class BlockManager : BaseDal<bjf_block>
    {
        private static BlockManager _instance;
        private BlockManager() { }

        public static BlockManager GetInstance()
        {
            return _instance ?? (_instance = new BlockManager());
        }
        public PagerResult<bjf_block> GetPageList(QueryBlock query)
        {

            var queryinfo = new QueryInfo<bjf_block>()
            {
                wheresql = this.LinqSql(query),
                orderby = m => m.updatetime,
                pageindex = query.PageIndex,
                pagesize = query.PageSize
            };
            var list = this.SelectPageList(queryinfo);
            var count = this.GetCountByQuery(queryinfo);
            //返回到视图
            var res = new PagerResult<bjf_block>
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
        public Expression<System.Func<bjf_block, bool>> LinqSql(QueryBlock query)
        {

            var builder = PredicateBuilder.True<bjf_block>();
            builder = builder.And(m => m.isdelete == false);
            //传参
            if (!string.IsNullOrEmpty(query.blockname))
            {
                builder = builder.And(m => m.blockname == query.blockname);
            }
            return builder;
        }
    }
}
