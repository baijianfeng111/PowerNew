using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using PowerNew.Model;

namespace PowerNew.Bll
{
    /// <summary>
    /// 下拉列表类
    /// </summary>
    public static class SelectListCenter
    {
        /// <summary>
        /// 模块
        /// </summary>
        /// <param name="selectedValue"></param>
        /// <returns></returns>
        public static List<SelectListItem> GetBlockSelectList(int? selectedValue = null)
        {
            List<SelectListItem> listResult = new List<SelectListItem>();
            var firstSelectItem = new SelectListItem() { Text = "---未指定---", Value = null };
            listResult.Insert(0, firstSelectItem);


            var productSpecList = BlockManager.GetInstance().SelectList(m => m.isdelete == false && m.isuse == 0);
            if (productSpecList == null) return listResult;

            var tmpList = productSpecList.Select(m => new SelectListItem() { Text = m.blockname.ToString(), Value = m.id.ToString() });
            listResult.AddRange(tmpList);

            //设定为选中状态
            listResult.ForEach(m =>
            {
                if (selectedValue.ToString() == m.Value)
                {
                    m.Selected = true;
                }
            });
            return listResult;
        }

        /// <summary>
        /// 菜单
        /// </summary>
        /// <param name="selectedValue"></param>
        /// <returns></returns>
        public static List<SelectListItem> GetGroupMenuSelectList(int? selectedValue = null)
        {
            List<SelectListItem> listResult = new List<SelectListItem>();
            var firstSelectItem = new SelectListItem() { Text = "---根节点---", Value = 0.ToString() };
            listResult.Insert(0, firstSelectItem);


            var productSpecList = MenuManager.GetInstance().SelectList(m => m.isdelete == false && m.isuse == false && m.isgroup == true);
            if (productSpecList == null) return listResult;

            var tmpList = productSpecList.Select(m => new SelectListItem() { Text = m.menuname.ToString(), Value = m.id.ToString() });
            listResult.AddRange(tmpList);

            //设定为选中状态
            listResult.ForEach(m =>
            {
                if (selectedValue.ToString() == m.Value)
                {
                    m.Selected = true;
                }
            });
            return listResult;
        }

        /// <summary>
        /// 将list>T转换为List>SelectListItem
        /// </summary>
        /// <param name="listintention"></param>
        /// <param name="selectedValue"></param>
        /// <returns></returns>
        public static List<SelectListItem> ToSelectList(List<Model.bjf_articletype> listintention, int? selectedValue = null)
        {
            var listResult = new List<SelectListItem>();
            var firstSelectItem = new SelectListItem() { Text = "---未指定---", Value = "" };
            listResult.Insert(0, firstSelectItem);

            if (listintention == null) return listResult;

            var tmpList = listintention.Select(m => new SelectListItem() { Text = m.title, Value = m.id.ToString() });
            listResult.AddRange(tmpList);

            //设定为选中状态
            listResult.ForEach(m =>
            {
                if (selectedValue.ToString() == m.Value)
                {
                    m.Selected = true;
                }
            });
            return listResult;
        }

        /// <summary>
        ///文章类别下拉列表
        /// </summary>
        /// <param name="selectedValue"></param>
        /// <returns></returns>
        public static List<SelectListItem> GetArticleTypeOneSelectList(int? selectedValue = null)
        {
            List<SelectListItem> listResult = new List<SelectListItem>();
            var firstSelectItem = new SelectListItem() { Text = "---未指定---", Value = null };
            listResult.Insert(0, firstSelectItem);


            var productSpecList = ArticleTypeManager.GetInstance().SelectList(m => m.isdelete == false && m.parentid == 0);
            if (productSpecList == null) return listResult;

            var tmpList = productSpecList.Select(m => new SelectListItem() { Text = m.title.ToString(), Value = m.id.ToString() });
            listResult.AddRange(tmpList);

            //设定为选中状态
            listResult.ForEach(m =>
            {
                if (selectedValue.ToString() == m.Value)
                {
                    m.Selected = true;
                }
            });
            return listResult;
        }

        /// <summary>
        /// 文章系列
        /// </summary>
        /// <param name="parentid"></param>
        /// <param name="selectedValue"></param>
        /// <returns></returns>
        public static List<SelectListItem> GetArticleTypeTwoSelectList(int? parentid = null, int? selectedValue = null)
        {
            List<SelectListItem> listResult = new List<SelectListItem>();
            var firstSelectItem = new SelectListItem() { Text = "---未指定---", Value = null };
            listResult.Insert(0, firstSelectItem);

            var list = new List<bjf_articletype>();
            if (parentid > 0)
            {
                list = ArticleTypeManager.GetInstance().SelectList(m => m.isdelete == false && m.parentid == parentid);
                if (list == null) return listResult;
            }


            var tmpList = list.Select(m => new SelectListItem() { Text = m.title.ToString(), Value = m.id.ToString() });
            listResult.AddRange(tmpList);

            //设定为选中状态
            listResult.ForEach(m =>
            {
                if (selectedValue.ToString() == m.Value)
                {
                    m.Selected = true;
                }
            });
            return listResult;
        }

        /// <summary>
        /// 转化为Html代码
        /// </summary>
        /// <param name="listSelect"></param>
        /// <returns></returns>
        public static string ToHtml(this List<SelectListItem> listSelect)
        {
            if (listSelect == null || listSelect.Count == 0) return "";
            var html = "";
            var optionHtml = @"<option value='{0}' {2}>{1}</option>";
            foreach (var item in listSelect)
            {
                if (item.Selected)
                {
                    html += string.Format(optionHtml, item.Value, item.Text, "selected='selected'");
                }
                else
                {
                    html += string.Format(optionHtml, item.Value, item.Text, "");
                }
            }

            return html;
        }
    }
}
