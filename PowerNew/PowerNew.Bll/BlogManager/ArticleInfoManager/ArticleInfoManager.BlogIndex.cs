using System.Linq.Expressions;
using LinqKit;
using PowerNew.Common;
using PowerNew.Dal;
using PowerNew.Model;

namespace PowerNew.Bll
{
    public partial class ArticleInfoManager
    {
        public NewPagerResult<bjf_articleinfo> GetBlogPageList(QueryArticleInfo query)
        {
            var queryinfo = new QueryInfo<bjf_articleinfo>()
            {
                wheresql = this.LinqSql(query),
                orderby = m => m.updatetime,
                pageindex = query.PageIndex,
                pagesize = query.PageSize
            };
            var list = this.SelectPageList(queryinfo);
            var count = this.GetCountByQuery(queryinfo);
            //返回到视图
            var res = new NewPagerResult<bjf_articleinfo>
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
