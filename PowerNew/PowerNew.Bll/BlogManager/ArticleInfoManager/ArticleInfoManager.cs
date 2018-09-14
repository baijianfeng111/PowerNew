using System.Linq.Expressions;
using LinqKit;
using PowerNew.Common;
using PowerNew.Dal;
using PowerNew.Model;

namespace PowerNew.Bll
{

    public class QueryArticleInfo : PagerInBase
    {
        public string title { get; set; }
        public int typeid { get; set; }
    }

    public class ArticleInfoItem : bjf_articleinfo
    {
        public string UserName { get; set; }
    }
    public partial class ArticleInfoManager : BaseDal<bjf_articleinfo>
    {
        private static ArticleInfoManager _instance;
        private ArticleInfoManager() { }

        public static ArticleInfoManager GetInstance()
        {
            return _instance ?? (_instance = new ArticleInfoManager());
        }

        public bjf_articleinfo GetModel(int id)
        {
            return this.SelectOne(m => m.isdelete == false && m.id == id);
        }

        public PagerResult<bjf_articleinfo> GetPageList(QueryArticleInfo query)
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
            var res = new PagerResult<bjf_articleinfo>
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
        public Expression<System.Func<bjf_articleinfo, bool>> LinqSql(QueryArticleInfo query)
        {

            var builder = PredicateBuilder.True<bjf_articleinfo>();
            builder = builder.And(m => m.isdelete == false);
            //传参
            if (!string.IsNullOrEmpty(query.title))
            {
                builder = builder.And(m => m.title == query.title);
            }

            if (query.typeid > 0)
            {
                builder = builder.And(m => m.typeid == query.typeid);
            }
            return builder;
        }
    }
}
