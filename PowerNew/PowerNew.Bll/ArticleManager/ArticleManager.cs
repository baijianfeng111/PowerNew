using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PowerNew.Dal;
using PowerNew.Model;

namespace PowerNew.Bll
{
    public class ArticleManager : BaseDal<bjf_article>
    {
        private static ArticleManager _instance;
        private ArticleManager() { }

        public static ArticleManager GetInstance()
        {
            return _instance ?? (_instance = new ArticleManager());
        }

        public bjf_article GetModel(int id)
        {
            return this.SelectOne(m => m.isdelete == false && m.id == id);
        }
    }
}
