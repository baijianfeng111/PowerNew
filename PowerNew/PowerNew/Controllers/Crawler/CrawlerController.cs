using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using PowerNew.Common;
using PowerNew.DoWork;

namespace PowerNew.Controllers.Crawler
{
    public class CrawlerController : Controller
    {
        // GET: Crawler
        public ActionResult Index()
        {
            //var url = "https://wenku.baidu.com/view/928a6f1ce009581b6ad9ebc6.html";
            //var html = GetHttpWebRequest(url);
            //ViewBag.Html = html;



            //MyBjfService sa = new MyBjfService();
            //var list = sa.DoWork();
            //var a = new JavaScriptSerializer().Serialize(list);
            //ViewBag.a = a;
            return View();
        }


        [HttpPost]
        public ActionResult Pub()
        {
            try
            {
                localhost.Service1 sa = new localhost.Service1();
                var a = sa.GetData(123, true);
                return Json(new { state = 0, msg = a });
            }
            catch (Exception e)
            {
                return Json(new { state = 0, msg = e.Message });
            }
        }

        [HttpPost]
        public ActionResult OffHook()
        {
            try
            {
                //localhost.Service1 sa = new localhost.Service1();
                //var a = sa.OffHook();
                var key = "M1";
                RedisManager.ChooseDb(2);
                if (RedisManager.GetAllKeys().Contains(key))
                {
                    RedisManager.Remove(key);
                    RedisManager.SetText(key, "1");
                }
                return Json(new { state = 0, msg = "ok" });
            }
            catch (Exception e)
            {
                return Json(new { state = 0, msg = e.Message });
            }
        }

        [HttpPost]
        public ActionResult HangUp()
        {
            try
            {
                //localhost.Service1 sa = new localhost.Service1();
                //var a = sa.HangUp();
                var key = "M1";
                RedisManager.ChooseDb(2);
                if (RedisManager.GetAllKeys().Contains(key))
                {
                    RedisManager.Remove(key);
                    RedisManager.SetText(key, "3");
                }
                return Json(new { state = 0, msg = "ok" });
            }
            catch (Exception e)
            {
                return Json(new { state = 0, msg = e.Message });
            }
        }

        [HttpPost]
        public ActionResult Dial()
        {
            try
            {
                //localhost.Service1 sa = new localhost.Service1();
                //var a = sa.Dial();
                var key = "M1";
                RedisManager.ChooseDb(2);
                if (RedisManager.GetAllKeys().Contains(key))
                {
                    RedisManager.Remove(key);
                    RedisManager.SetText(key, "2");
                }
                return Json(new { state = 0, msg = "ok" });
            }
            catch (Exception e)
            {
                return Json(new { state = 0, msg = e.Message });
            }
        }

        [HttpPost]
        public ActionResult ShowCode()
        {
            try
            {
                var code = new Random().Next(1, 99999);
                var showcode = string.Format("Rompy{0}", code);
                return Json(new { state = 0, msg = showcode });
            }
            catch (Exception e)
            {
                return Json(new { state = 0, msg = e.Message });
            }
        }

        public ActionResult DownZip()
        {
            var path = Server.MapPath("~/asset/Phone.zip");

            //转化二进制
            //FileStream fs = new FileStream(Server.MapPath("~/asset/Phone.zip"), FileMode.Open);
            //byte[] file = new byte[fs.Length];

            //return File(path, "iamge/jpeg");//简单下载
            var filename = Path.GetFileName(path);
            string curBrowser = HttpContext.Request.Browser.Type.ToLower(); //浏览器类型判断
            if (curBrowser.Contains("internetexplorer"))
            {
                filename = HttpUtility.UrlEncode(filename, Encoding.UTF8);
            }
            else
            {
                filename = HttpUtility.UrlDecode(filename, Encoding.UTF8); ;
            }

            return File(path, "application/zip-x-compressed", filename); //带名下载

            //Response.Clear();
            //Response.Buffer = true;
            //Response.AddHeader("Content-Disposition", "attachment; filename=" + filename);
            //Response.ContentType = "application/zip-x-compressed";
            //Response.Charset = "GB2312";
            //Response.ContentEncoding = System.Text.Encoding.GetEncoding("GB2312");
            //Response.BinaryWrite(file);
            //return null;
        }



        public ActionResult Baidu()
        {
            return View();
        }


        [HttpPost]
        public ActionResult DownLoadPdf(string httpurl)
        {
            try
            {
                //创建PDF保存的根目录
                string path = System.Web.HttpContext.Current.Server.MapPath(@"/asset");
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }

                //PDF本地地址
                string fileId = DateTime.Now.ToString("yyyyMMddHHmmss") + new Random().Next(10000, 99999);
                string allname = @"/asset/" + fileId + ".pdf";


                //将网页上的信息下载为PDF保存到本地
                this.DownLoad(System.Web.HttpContext.Current, allname, httpurl);
                return Json(new { state = 0, msg = "成功" });
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

        }

        [HttpPost]
        public ActionResult DeleteFile()
        {
            try
            {
                var path = HttpContext.Server.MapPath(@"/asset/2018052909062868347.pdf");
                System.IO.File.Delete(path);
                return Json(new { state = 0, msg = "成功" });
            }
            catch (Exception e)
            {
                LogHelper.log.Error(e.Message);
                return Json(new { state = 1, msg = e.Message });
            }
        }


        #region 通过地址下载pdf

        public void DownLoad(HttpContext context, string pdfurl, string httpurl)
        {

            try
            {

                string url = httpurl;


                string pdf = @"C:\Program Files\wkhtmltopdf\bin\wkhtmltopdf.exe";

                string pdfpath = pdfurl;
                ProcessStartInfo processStartInfo = new ProcessStartInfo();
                processStartInfo.FileName = pdf;
                processStartInfo.WorkingDirectory = Path.GetDirectoryName(pdf);
                processStartInfo.UseShellExecute = false;
                processStartInfo.CreateNoWindow = true;
                processStartInfo.RedirectStandardInput = true;
                processStartInfo.RedirectStandardOutput = true;
                processStartInfo.RedirectStandardError = true;
                processStartInfo.Arguments = GetArguments(url, context.Server.MapPath(pdfpath));

                Process process = new Process();
                process.StartInfo = processStartInfo;
                process.Start();
                process.WaitForExit();

                process.Close();
                process.Dispose();


                //方法2，使用下面代码，让客户下载
                FileStream fs = new FileStream(context.Server.MapPath(pdfpath), FileMode.Open);
                byte[] file = new byte[fs.Length];
                fs.Read(file, 0, file.Length);
                fs.Close();
                System.IO.File.WriteAllBytes(context.Server.MapPath(pdfpath), file);
            }
            catch (Exception e)
            {
                LogHelper.log.Error(e.Message);
            }

        }

        private string GetArguments(string htmlPath, string savePath)
        {
            if (string.IsNullOrEmpty(htmlPath))
            {
                throw new Exception("HTML local path or network address can not be empty.");
            }

            if (string.IsNullOrEmpty(savePath))
            {
                throw new Exception("The path saved by the PDF document can not be empty.");
            }

            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append(" --margin-top 20 ");
            stringBuilder.Append(" --margin-left 5 ");
            stringBuilder.Append(" --disable-smart-shrinking "); //缩放
            stringBuilder.Append(" --page-size A4 ");
            stringBuilder.Append(" --dpi 100 ");
            stringBuilder.Append(" --zoom 0.95 ");//默认1
            stringBuilder.Append(" " + htmlPath + " ");       //本地 HTML 的文件路径或网页 HTML 的URL地址
            stringBuilder.Append(" " + savePath + " ");       //生成的 PDF 文档的保存路径
            return stringBuilder.ToString();
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

        #endregion



        public ActionResult ShowQr()
        {


            var msgurl = string.Format("http://m.rompy.cn/QRCode/Index?shopid={0}&userid={1}&flag={2}", 1, 1, 1);
            var jumpul = HttpUtility.UrlEncode(msgurl);
            var hostUrl = "http://qr.liantu.com/api.php?text={0}";
            //二维码地址
            var qrcodeurl = string.Format(hostUrl, jumpul);
            ViewBag.qecodeurl = qrcodeurl;
            return Index();


            //function doPrint() {
            //    bdhtml = window.document.body.innerHTML;
            //    sprnstr = "<!--startprint-->";
            //    eprnstr = "<!--endprint-->";
            //    prnhtml = bdhtml.substr(bdhtml.indexOf(sprnstr) + 17);
            //    prnhtml = prnhtml.substring(0, prnhtml.indexOf(eprnstr));
            //    window.document.body.innerHTML = prnhtml;
            //    window.print();
            //}
        }


    }
}