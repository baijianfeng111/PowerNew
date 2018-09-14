using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LinqKit;
using PowerNew.Common;
using PowerNew.Dal;
using PowerNew.Model;

namespace PowerNew.Bll
{
    public class QueryMenu : PagerInBase
    {
        
    }
    public partial class MenuManager : BaseDal<bjf_menu>
    {
        private static MenuManager _instance;
        private MenuManager() { }

        public static MenuManager GetInstance()
        {
            return _instance ?? (_instance = new MenuManager());
        }

        public PagerResult<bjf_menu> GetPageList(PagerInBase query)
        {
            //拼接sql条件查询-表达式树
            var builder = PredicateBuilder.True<bjf_menu>();
            builder = builder.And(m => m.isdelete == false);
            //传参
            var queryinfo = new QueryInfo<bjf_menu>()
            {
                wheresql = builder,
                orderby = m => m.updatetime,
                pageindex = query.PageIndex,
                pagesize = query.PageSize
            };
            var list = this.SelectPageList(queryinfo);
            var count = this.GetCountByQuery(queryinfo);
            //返回到视图
            var res = new PagerResult<bjf_menu>
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
