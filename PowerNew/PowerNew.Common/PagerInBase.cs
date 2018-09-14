using System;
using System.Collections.Generic;
using System.Text;
using System.Web;

namespace PowerNew.Common
{
    /// <summary>
    /// 分页基础类
    /// </summary>
    public class PagerInBase
    {
        public int limit { get; set; }

        public int offset { get; set; }
        /// <summary>
        /// 当前页
        /// </summary>
        public int PageIndex { get; set; }

        /// <summary>
        /// 每页显示的个数
        /// </summary>
        public int PageSize { get; set; }

        /// <summary>
        /// 获取请求URL
        /// </summary>
        public string RequetUrl
        {
            get { return System.Web.HttpContext.Current.Request.Url.OriginalString; }
        }
        /// <summary>
        /// 构造函数给当前页和页数初始化
        /// </summary>
        public PagerInBase()
        {
            if (PageIndex == 0) PageIndex = 1;
            if (PageSize == 0) PageSize = 10;
        }
    }

}