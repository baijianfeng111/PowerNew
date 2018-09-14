using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Web.Script.Serialization;
using System.Xml;
using System.Xml.Linq;
using Microsoft.Practices.ServiceLocation;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using NServiceKit.Common.Extensions;
using NServiceKit.Text;
using NServiceKit.Text.Json;
using PowerNew.Common;
using SolrNet;
using SolrNet.Attributes;

namespace Test
{
    [TestClass]
    public class MongoTest
    {
        [TestMethod]
        public void Add()
        {
            var createtime = DateTime.Now.Date.ToString();
            Console.WriteLine(createtime);
            //PowerNew.Models.DBcon.AddVedio();
        }

        [TestMethod]
        public void Select()
        {
            PowerNew.Models.DBcon.Select();
        }

        [TestMethod]
        public void SelectOne()
        {
            PowerNew.Models.DBcon.SelectOne();
        }


        [TestMethod]
        public void Update()
        {
            PowerNew.Models.DBcon.Update();
        }


        [TestMethod]
        public void Delete()
        {
            PowerNew.Models.DBcon.Delete();
        }

        [TestMethod]
        public void TestNow()
        {
            var length = 5;
            var par = new char[] { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z', '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z' };
            var random = new Random();
            string str = "";
            for (int i = 0; i < length; i++)
            {
                var index = random.Next(0, par.Length);
                str += par[index];
            }
            Console.WriteLine(str);
        }

        [TestMethod]
        public void TestStringToXml()
        {
            XmlDocument doc = new XmlDocument();
            List<Product> products = new List<Product>(){  
                new Product(){Name="苹果",Price=5.5},  
                new Product(){Name="橘子",Price=2.5},  
                new Product(){Name="干柿子",Price=16.00}  
            };
            ProductList productlist = new ProductList();
            productlist.GetProducts = products;
            var a = new JavaScriptSerializer().Serialize(productlist);
            JavaScriptSerializer oSerializer = new JavaScriptSerializer();


            Dictionary<string, object> Dic = (Dictionary<string, object>)oSerializer.DeserializeObject(a);

            XmlDeclaration xmlDec;
            xmlDec = doc.CreateXmlDeclaration("1.0", "gb2312", "yes");
            doc.InsertBefore(xmlDec, doc.DocumentElement);
            XmlElement nRoot = doc.CreateElement("root");
            doc.AppendChild(nRoot);
            foreach (KeyValuePair<string, object> item in Dic)
            {
                XmlElement element = doc.CreateElement(item.Key);
                KeyValue2Xml(element, item);
                nRoot.AppendChild(element);
            }
            Console.WriteLine(doc.OuterXml);                  //-------------json转xml
            XmlNode xmlnode = doc.SelectSingleNode("root/GetProducts");



            var list = new ProductList();                     //-------------xml转list
            var listpro = new List<Product>();
            XmlNodeList xnl = xmlnode.ChildNodes;
            foreach (XmlNode xn in xnl)
            {
                // 将节点转换为元素，便于得到节点的属性值
                XmlElement xe = (XmlElement)xn;
                // 得到Book节点的所有子节点
                XmlNodeList xnl0 = xe.ChildNodes;
                var model = new Product()
                {
                    Name = xnl0.Item(0).InnerText,
                    Price = Convert.ToDouble(xnl0.Item(1).InnerText)
                };
                listpro.Add(model);
                Console.WriteLine("name:" + model.Name, "Price:" + model.Price);
            }
        }

        private static void KeyValue2Xml(XmlElement node, KeyValuePair<string, object> Source)
        {
            object kValue = Source.Value;
            if (kValue.GetType() == typeof(Dictionary<string, object>))
            {
                foreach (KeyValuePair<string, object> item in kValue as Dictionary<string, object>)
                {
                    XmlElement element = node.OwnerDocument.CreateElement(item.Key);
                    KeyValue2Xml(element, item);
                    node.AppendChild(element);
                }
            }
            else if (kValue.GetType() == typeof(object[]))
            {
                object[] o = kValue as object[];
                for (int i = 0; i < o.Length; i++)
                {
                    XmlElement xitem = node.OwnerDocument.CreateElement("Item");
                    KeyValuePair<string, object> item = new KeyValuePair<string, object>("Item", o[i]);
                    KeyValue2Xml(xitem, item);
                    node.AppendChild(xitem);
                }

            }
            else
            {
                XmlText text = node.OwnerDocument.CreateTextNode(kValue.ToString());
                node.AppendChild(text);
            }
        }



        public class Product
        {
            public string Name { get; set; }
            public double Price { get; set; }
        }

        public class ProductList
        {
            public List<Product> GetProducts { get; set; }
        }
    }
}
