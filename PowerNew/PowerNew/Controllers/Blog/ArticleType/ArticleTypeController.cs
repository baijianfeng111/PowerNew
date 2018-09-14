using System;
using System.Collections.Generic;
using System.Web.Mvc;
using PowerNew.Bll;
using PowerNew.Model;

namespace PowerNew.Controllers
{

    public partial class ArticleTypeController : BaseController
    {
        // GET: ArticleType
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult Tree()
        {
            var treeNode = new ProductCategoryTreeNode();
            //最底层的分类
            var rootList = ArticleTypeManager.GetInstance().GetRootList(0);
            foreach (var item in rootList)
            {
                this.LoadMenuTreeNode(item, treeNode);
            }
            return Json(treeNode, JsonRequestBehavior.AllowGet);
        }

        private void LoadMenuTreeNode(bjf_articletype item, ProductCategoryTreeNode treeNode)
        {
            if (item == null)
            {
                return;
            }
            var currentNode = new ProductCategoryTreeNode();
            currentNode.Id = item.id;
            currentNode.key = item.id.ToString();
            currentNode.expanded = true;
            currentNode.folder = false;
            currentNode.title = item.title;
            currentNode.ParentId = item.parentid;
            if (item.parentid == 0)
            {
                currentNode.IsAddButtonForNextLevel = true;
            }
            currentNode.SortCode = item.id;
            treeNode.children.Add(currentNode);
            List<bjf_articletype> groupmentList = ArticleTypeManager.GetInstance().SelectList(m => m.isdelete == false && m.parentid == item.id);
            if (groupmentList.Count > 0)
            {
                foreach (var childItem in groupmentList)
                {
                    this.LoadMenuTreeNode(childItem, currentNode);
                }
            }
        }

    }


    public class ProductCategoryTreeNode : FancyTreeNode
    {
        public ProductCategoryTreeNode()
        {
            SortCode = 0;
            this.expanded = true;
            this.lazy = false;

            this.Level = 0;

        }

        public int Id { get; set; }

        public string Name { get; set; }

        public string CategoryName { get; set; }

        public string Code { get; set; }

        public int SortCode { get; set; }

        public int Level { get; set; }

        public bool IsAddButtonForCategory { get; set; }

        public bool IsAddButtonForSeries { get; set; }
        public bool IsAddButtonForNextLevel { get; set; }

    }

    public class FancyTreeNode
    {
        public FancyTreeNode()
        {
            this.children = new List<FancyTreeNode>();
            this.expanded = false;
            this.lazy = false;
        }

        public long ID { get; set; }
        public string NodeType { get; set; }
        public int ParentId { get; set; }
        public string key { get; set; }
        public string title { get; set; }
        public bool folder { get; set; }
        public bool lazy { get; set; }
        public bool expanded { get; set; }
        public List<FancyTreeNode> children { get; set; }

        public bool hideCheckbox { get; set; }
        public bool unselectable { get; set; }


    }
}