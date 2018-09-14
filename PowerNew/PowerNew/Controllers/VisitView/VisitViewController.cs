using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using PowerNew.Bll;
using PowerNew.Filter;

namespace PowerNew.Controllers.VisitView
{
    public class VisitViewController : Controller
    {
        [IsLoginAuthorize]
        public ActionResult Index()
        {
            return View();
        }


        [HttpPost]
        public ActionResult GetParam()
        {
            try
            {
                var x = LoginLogManager.GetInstance().GetXArry();
                var y = LoginLogManager.GetInstance().GetYArry();

                //用于饼状图表
                var name = LoginLogManager.GetInstance().GetYList();
                var info = LoginLogManager.GetInstance().GetListShowInfo();

                return Json(new { state = 0, x = x, y = y, name = name, info = info });
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public void Chart2()
        {
            new Chart(width: 600, height: 400, theme: ChartTheme.Yellow)
                .AddTitle("人员流动情况")

                .AddSeries(name: "Employee",
                           chartType: "line",
                xValue: new[] { "一月份", "二月份", "三月份", "四月份", "五月份", "六月份", "七月份", "八月份", "九月份" },
                yValues: new[] { "2", "6", "4", "5", "3", "4", "9", "2", "5" }

                )

                .AddSeries(name: "Mall"
                    , chartType: "Column"
                    , xValue: new[] { "A", "B", "C", "D", "E", "F", "G", "H", "I" }
                    , yValues: new[] { 4, 5, 8, 1, 6, 8, 4, 3, 6 })

                .Write();
        }
    }
}