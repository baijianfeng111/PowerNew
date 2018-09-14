using PowerNew.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LinqKit;
using PowerNew.Common;
using PowerNew.Dal;

namespace PowerNew.Bll
{
    public partial class LoginLogManager : BaseDal<bjf_loginlog>
    {
        private static LoginLogManager _instance;
        private LoginLogManager() { }

        public static LoginLogManager GetInstance()
        {
            return _instance ?? (_instance = new LoginLogManager());
        }

        /// <summary>
        /// 获取X轴显示内容(近5个月的登录数据)
        /// </summary>
        /// <returns></returns>
        public int[] GetXArry()
        {
            int[] arr = new int[5];
            int month = DateTime.Now.Month;

            for (int i = 0; i < 5; i++)
            {
                //判断是不是头一年的
                var a = month - i;
                if (a == month)               //本月
                {
                    arr[i] = month;
                }
                else if (a > 0)
                {
                    arr[i] = a;
                }
                else
                {
                    arr[i] = 12 + a;
                }
            }
            return arr;
        }

        /// <summary>
        /// 获取Y轴显示内容
        /// </summary>
        /// <returns></returns>
        public int[] GetYArry()
        {

            var list = this.GetXArry().ToList(); //月份
            int[] arr = new int[list.Count];
            foreach (var item in list)
            {
                var index = list.IndexOf(item);
                if (index + 1 < list.Count)                            //索引小于集合索引
                {
                    if (list[index] > list[index + 1])                 //前面的月大于后面的  则是本年          
                    {
                        arr[index] = this.GetLoginCount(item, 0);
                    }
                    else
                    {
                        arr[index] = this.GetLoginCount(item, 1);       //前面的月小于后面的  则是跨年     
                    }
                }
                else
                {
                    if (list[index] > list[index - 1])
                    {
                        arr[index] = this.GetLoginCount(item, 0);
                    }
                    else
                    {
                        arr[index] = this.GetLoginCount(item, 1);
                    }
                }


            }
            return arr;
        }

        public string[] GetYList()
        {
            var list = this.GetXArry().ToList();
            string[] namelist = new string[list.Count];
            if (list.Count > 0)
            {
                foreach (var item in list)
                {
                    var index = list.IndexOf(item);
                    namelist[index] = item + "月";
                }
            }
            return namelist;
        }

        public List<ShowInfo> GetListShowInfo()
        {
            var listx = this.GetXArry().ToList();
            var listy = this.GetYArry();
            var listshow = new List<ShowInfo>();
            if (listx.Count > 0)
            {
                foreach (var item in listx)
                {
                    var index = listx.IndexOf(item);
                    var model = new ShowInfo()
                    {
                        name = item + "月",
                        value = listy.ToList().Count > 0 ? listy[index] : 0
                    };
                    listshow.Add(model);
                }
            }
            return listshow;
        }

        public int GetLoginCount(int month, int flag)
        {
            var list = new List<bjf_loginlog>();
            if (flag == 0)
            {
                list = this.SelectList(m => m.logintime.Month == month);
            }
            else
            {
                list = this.SelectList(m => m.logintime.Month == month && m.logintime.Year == DateTime.Now.Year - 1);
            }

            return list.Count;
        }


        public PagerResult<bjf_loginlog> GetPageList(QueryRole query)
        {
            //拼接sql条件查询-表达式树
            var builder = PredicateBuilder.True<bjf_loginlog>();
            var queryinfo = new QueryInfo<bjf_loginlog>()
            {
                wheresql = builder,
                orderby = m => m.updatetime,
                pageindex = query.PageIndex,
                pagesize = query.PageSize
            };
            var list = this.SelectPageList(queryinfo);
            var count = this.GetCountByQuery(queryinfo);
            //返回到视图
            var res = new PagerResult<bjf_loginlog>
            {
                DataList = list,
                Total = count,
                PageSize = query.PageSize,
                PageIndex = query.PageIndex,
                RequestUrl = query.RequetUrl
            };
            return res;
        }
        public class ShowInfo
        {
            public string name { get; set; }
            public int value { get; set; }
        }


        public TableInfo GetShowTable(QueryRole query)
        {
            var builder = PredicateBuilder.True<bjf_loginlog>();
            var queryinfo = new QueryInfo<bjf_loginlog>()
            {
                wheresql = builder,
                orderby = m => m.updatetime,
                pageindex = query.PageIndex,
                pagesize = query.PageSize
            };
            var list = this.SelectPageList(queryinfo);
            var count = this.GetCountByQuery(queryinfo);
            var model = new TableInfo()
            {
                rows = list,
                total = count
            };
            return model;
        }
        public class TableInfo
        {
            public List<bjf_loginlog> rows { get; set; }
            public int total { get; set; }
        }
    }
}
