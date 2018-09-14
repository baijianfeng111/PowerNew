using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Web;
using System.Web.Mvc;
using PowerNew.Common;

namespace PowerNew.Controllers
{
    public class FileController : Controller
    {
        private string StorageRoot
        {
            get
            {
                var rootPath = Server.MapPath("~/Upload/Files");
                var savePath = GetSavePath(rootPath);
                return savePath;
            }
        }

        [HttpPost]
        public ActionResult UploadFiles()
        {
            var r = new List<ViewDataUploadFilesResult>();

            foreach (string file in Request.Files)
            {

                var statuses = new List<ViewDataUploadFilesResult>();
                var headers = Request.Headers;

                if (string.IsNullOrEmpty(headers["X-File-Name"]))
                {
                    UploadWholeFile(Request, statuses);
                }
                else
                {
                    UploadPartialFile(headers["X-File-Name"], Request, statuses);
                }

                JsonResult result = Json(statuses);
                result.ContentType = "text/plain";

                return result;
            }

            return Json(r);
        }

        private void UploadPartialFile(string fileName, HttpRequestBase request, List<ViewDataUploadFilesResult> statuses)
        {
            if (request.Files.Count != 1) throw new HttpRequestValidationException("Attempt to upload chunked file containing more than one fragment per request");
            var file = request.Files[0];
            var inputStream = file.InputStream;

            var saveName = this.GetSaveName(fileName);
            var fullName = Path.Combine(StorageRoot, saveName);

            using (var fs = new FileStream(fullName, FileMode.Append, FileAccess.Write))
            {
                var buffer = new byte[1024];

                var l = inputStream.Read(buffer, 0, 1024);
                while (l > 0)
                {
                    fs.Write(buffer, 0, l);
                    l = inputStream.Read(buffer, 0, 1024);
                }
                fs.Flush();
                fs.Close();
            }
            statuses.Add(new ViewDataUploadFilesResult()
            {
                openid = OpenHelper.CreateOpenId(),
                name = fileName,
                save_name = saveName,
                size = file.ContentLength,
                type = file.ContentType,
                url = GetHttpFileUrl(saveName),
                delete_url = Url.Action("Delete?id=" + saveName),
                delete_type = "GET",
            });
        }

        private void UploadWholeFile(HttpRequestBase request, List<ViewDataUploadFilesResult> statuses)
        {
            for (int i = 0; i < request.Files.Count; i++)
            {
                var file = request.Files[i];
                var saveName = this.GetSaveName(file.FileName);

                var fullPath = Path.Combine(StorageRoot, saveName);

                file.SaveAs(fullPath);

                statuses.Add(new ViewDataUploadFilesResult()
                {
                    openid = OpenHelper.CreateOpenId(),
                    name = file.FileName,
                    save_name = saveName,
                    size = file.ContentLength,
                    type = file.ContentType,
                    url = GetHttpFileUrl(saveName),
                    delete_url = Url.Action("Delete?id=" + saveName),
                    delete_type = "GET",
                });
            }
        }

        private string GetSavePath(string rootPath)
        {
            string path = Path.Combine(rootPath, DateTime.Now.Year.ToString(), DateTime.Now.ToString("MMdd"));
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            return path;
        }


        /// <summary>
        /// 文件夹命名与文件有关
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        private string GetSaveName(string fileName)
        {
            var savaName = DateTime.Now.ToString("yyyyMMdd") + new Random().Next(0, 999).ToString("D3") + "userid" + Convert.ToInt32(SessionHelper.GetSession("userid")) + Path.GetExtension(fileName);
            return savaName;
        }

        private string GetHttpFileUrl(string filename)
        {

            var url = "/Upload/Files/";
            var tmp = GetFilePath(filename).Replace("\\", "/");
            return url + tmp;

        }

        private string GetFilePath(string fileName)
        {
            var year = fileName.Substring(0, 4);
            var monthday = fileName.Substring(4, 4);
            return Path.Combine(year, monthday, fileName);
        }



        public ActionResult Download(string filePath, string downloadName)
        {
            if (System.IO.File.Exists(filePath))
            {
                byte[] bytes = System.IO.File.ReadAllBytes(filePath);

                //解决不同浏览器乱码问题
                HttpBrowserCapabilitiesBase bc = Request.Browser;

                if (!bc.IsBrowser("FIREFOX"))
                {
                    downloadName = HttpUtility.UrlEncode(downloadName);
                }

                Response.Charset = "UTF-8";
                Response.ContentEncoding = Encoding.GetEncoding("GB2312");
                Response.ContentType = "application/vnd.ms-excel";
                this.HttpContext.Response.AddHeader("Content-Disposition", string.Format("attachment; filename={0}.xlsx", downloadName));
                this.HttpContext.Response.BinaryWrite(bytes);
            }
            return new EmptyResult();
        }

        public class ViewDataUploadFilesResult
        {
            public string openid { get; set; }
            public string name { get; set; }
            public string save_name { get; set; }
            public int size { get; set; }
            public string type { get; set; }
            public string url { get; set; }
            public string delete_url { get; set; }
            public string thumbnail_url { get; set; }
            public string delete_type { get; set; }
        }
    }
}
