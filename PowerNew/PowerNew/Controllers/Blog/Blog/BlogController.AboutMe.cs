using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using PowerNew.Bll;

namespace PowerNew.Controllers
{
    public partial class BlogController 
    {
      
        #region 关于我
        public ActionResult AboutMe()
        {
            return View();
        }
        #endregion
    }
}