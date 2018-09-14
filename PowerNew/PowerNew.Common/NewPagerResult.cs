using System;
using System.Collections.Generic;
using System.Text;

namespace PowerNew.Common
{
    /// <summary>
    /// 分页核心代码
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class NewPagerResult<T>
    {
        public Dictionary<int, string> Dic { get; set; }                          //特殊情况下用到，不属于通用类
        public int Index
        {
            get { return PageIndex == 1 ? 0 : (PageIndex - 1) * PageSize; }       //用于分页之后显示正确序号
        }
        public int Total { get; set; }
        public IEnumerable<T> DataList { get; set; }
        public int PageSize { get; set; }
        public int PageIndex { get; set; }
        public string RequestUrl { get; set; }
        public string PagerHtml(string cssClass = "jpager")
        {
            var html = new StringBuilder();
            html.AppendFormat("<ul class='pagination' >");
            var pageLen = Math.Ceiling((double)Total / PageSize);
            html.AppendFormat("<li><a href='javascript:void(0);' PageIndex='{0}'> 首页 </a></li>", 1);
            html.AppendFormat("<li><a href='javascript:void(0);' PageIndex='{0}'> 上页 </a></li>", PageIndex < 2 ? 1 : PageIndex - 1);

            var si = PageIndex <= 6 ? 1 : PageIndex - 5;
            var ei = si + 9;

            while (si <= pageLen && si <= ei)
                html.AppendFormat(
                    si == PageIndex
                        ? "<li><a style='color:black;border:none;' href='javascript:void(0);' PageIndex='{0}'>{1}</a></li>"
                        : "<li><a href='javascript:void(0);' PageIndex='{0}'>{1}</a></li>", si, si++);

            html.AppendFormat("<li><a href='javascript:void(0);'PageIndex='{0}' > 下页 </a></li>", (int)(PageIndex > pageLen - 1 ? pageLen : PageIndex + 1));

            html.AppendFormat("<li><a href='javascript:void(0);' PageIndex='{0}'> 尾页 </a></li>", Math.Abs(Total) <= 0 ? 1 : (int)pageLen);
            html.Append(@"</ul>");
            return html.ToString();
        }
    }
}
