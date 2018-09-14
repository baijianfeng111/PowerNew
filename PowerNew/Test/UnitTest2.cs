using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using RYM.YBY.Common;

namespace Test
{
    [TestClass]
    public class UnitTest2
    {
        [TestMethod]
        public void TestMethod1()
        {
            //string pwdmd5 = RYMPublic.getpassword("123456");
            //Console.WriteLine(pwdmd5);


            //接口地址
            //var url = "https://wenku.baidu.com/view/928a6f1ce009581b6ad9ebc6.html";

            //var html = GetHttpWebRequest(url);
            //Console.WriteLine(html);

            string href = "/Home/Index";
            var a = href.Split('/')[1];
            Console.WriteLine(a);
        }

        private string GetHttpWebRequest(string url)
        {
            HttpWebResponse result;
            string strHTML = string.Empty;
            try
            {
                Uri uri = new Uri(url);
                WebRequest webReq = WebRequest.Create(uri);
                WebResponse webRes = webReq.GetResponse();

                HttpWebRequest myReq = (HttpWebRequest)webReq;
                myReq.UserAgent = "User-Agent:Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2; .NET CLR 1.0.3705";
                myReq.Accept = "*/*";
                myReq.KeepAlive = true;
                myReq.Headers.Add("Accept-Language", "zh-cn,en-us;q=0.5");
                result = (HttpWebResponse)myReq.GetResponse();
                Stream receviceStream = result.GetResponseStream();
                StreamReader readerOfStream = new StreamReader(receviceStream, System.Text.Encoding.GetEncoding("utf-8"));
                strHTML = readerOfStream.ReadToEnd();
                readerOfStream.Close();
                receviceStream.Close();
                result.Close();
            }
            catch
            {
                Uri uri = new Uri(url);
                WebRequest webReq = WebRequest.Create(uri);
                HttpWebRequest myReq = (HttpWebRequest)webReq;
                myReq.UserAgent = "User-Agent:Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2; .NET CLR 1.0.3705";
                myReq.Accept = "*/*";
                myReq.KeepAlive = true;
                myReq.Headers.Add("Accept-Language", "zh-cn,en-us;q=0.5");
                //result = (HttpWebResponse)myReq.GetResponse(); 
                try
                {
                    result = (HttpWebResponse)myReq.GetResponse();
                }
                catch (WebException ex)
                {
                    result = (HttpWebResponse)ex.Response;
                }
                Stream receviceStream = result.GetResponseStream();
                StreamReader readerOfStream = new StreamReader(receviceStream, System.Text.Encoding.GetEncoding("gb2312"));
                strHTML = readerOfStream.ReadToEnd();
                readerOfStream.Close();
                receviceStream.Close();
                result.Close();
            }
            return strHTML;
        }

        //private static List<string> GetHyperLinks(string htmlCode, string url)
        //{
        //    ArrayList al = new ArrayList();
        //    bool IsGenxin = false;
        //    StringBuilder weburlSB = new StringBuilder();//SQL 
        //    StringBuilder linkSb = new StringBuilder();//展示数据 
        //    List<string> Weburllistzx = new List<string>();//新增 
        //    List<string> Weburllist = new List<string>();//旧的 
        //    string ProductionContent = htmlCode;
        //    Regex reg = new Regex(@"http(s)?://([\w-]+\.)+[\w-]+/?");
        //    string wangzhanyuming = reg.Match(url, 0).Value;
        //    MatchCollection mc = Regex.Matches(ProductionContent.Replace("href=\"/", "href=\"" + wangzhanyuming).Replace("href='/", "href='" + wangzhanyuming).Replace("href=/", "href=" + wangzhanyuming).Replace("href=\"./", "href=\"" + wangzhanyuming), @"<[aA][^>]* href=[^>]*>", RegexOptions.Singleline);
        //    int Index = 1;
        //    foreach (Match m in mc)
        //    {
        //        MatchCollection mc1 = Regex.Matches(m.Value, @"[a-zA-z]+://[^\s]*", RegexOptions.Singleline);
        //        if (mc1.Count > 0)
        //        {
        //            foreach (Match m1 in mc1)
        //            {
        //                string linkurlstr = string.Empty;
        //                linkurlstr = m1.Value.Replace("\"", "").Replace("'", "").Replace(">", "").Replace(";", "");
        //                weburlSB.Append("$-$");
        //                weburlSB.Append(linkurlstr);
        //                weburlSB.Append("$_$");
        //                if (!Weburllist.Contains(linkurlstr) && !Weburllistzx.Contains(linkurlstr))
        //                {
        //                    IsGenxin = true;
        //                    Weburllistzx.Add(linkurlstr);
        //                    linkSb.AppendFormat("{0}<br/>", linkurlstr);
        //                }
        //            }
        //        }
        //        else
        //        {
        //            if (m.Value.IndexOf("javascript") == -1)
        //            {
        //                string amstr = string.Empty;
        //                string wangzhanxiangduilujin = string.Empty;
        //                wangzhanxiangduilujin = url.Substring(0, url.LastIndexOf("/") + 1);
        //                amstr = m.Value.Replace("href=\"", "href=\"" + wangzhanxiangduilujin).Replace("href='", "href='" + wangzhanxiangduilujin);
        //                MatchCollection mc11 = Regex.Matches(amstr, @"[a-zA-z]+://[^\s]*", RegexOptions.Singleline);
        //                foreach (Match m1 in mc11)
        //                {
        //                    string linkurlstr = string.Empty;
        //                    linkurlstr = m1.Value.Replace("\"", "").Replace("'", "").Replace(">", "").Replace(";", "");
        //                    weburlSB.Append("$-$");
        //                    weburlSB.Append(linkurlstr);
        //                    weburlSB.Append("$_$");
        //                    if (!Weburllist.Contains(linkurlstr) && !Weburllistzx.Contains(linkurlstr))
        //                    {
        //                        IsGenxin = true;
        //                        Weburllistzx.Add(linkurlstr);
        //                        linkSb.AppendFormat("{0}<br/>", linkurlstr);
        //                    }
        //                }
        //            }
        //        }
        //        Index++;
        //    }
        //    return Weburllistzx;
        //}


        //二分法
        public static int Method(int[] nums, int low, int high, int target)
        {
            while (low <= high)
            {
                int middle = (low + high) / 2;
                if (target == nums[middle])
                {
                    return nums[middle];
                }
                else if (target > nums[middle])
                {
                    low = middle + 1;
                }
                else if (target < nums[middle])
                {
                    high = middle - 1;
                }
            }
            return -1;
        }
    }
}
