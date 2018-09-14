using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PowerNew.Dal;
using PowerNew.Model;

namespace PowerNew.Bll
{
    public class ArticleTypeManager : BaseDal<bjf_articletype>
    {
        private static ArticleTypeManager _instance;
        private ArticleTypeManager() { }

        public static ArticleTypeManager GetInstance()
        {
            return _instance ?? (_instance = new ArticleTypeManager());
        }

        public bjf_articletype GetModel(int id)
        {
            return this.SelectOne(m => m.isdelete == false && m.id == id);
        }

        public List<bjf_articletype> GetListChildren(int parentid)
        {
            return this.SelectList(m => m.isdelete == false && m.parentid == parentid);
        }

        public List<bjf_articletype> GetRootList(int parentid)
        {
            return this.SelectList(m => m.parentid == parentid && m.isdelete == false)
                .OrderBy(m => m.createtime)
                .ToList();
        }
    }
}
