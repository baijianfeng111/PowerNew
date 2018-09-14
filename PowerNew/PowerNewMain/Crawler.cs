using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Configuration;
using System.Text;
using System.Threading.Tasks;

namespace PowerNewMain
{
    public static class Crawler
    {
        public class SimpleCrawler
        {
            public event EventHandler<OnStartEventArgs> OnStart; //爬虫启动事件
            public event EventHandler<OnCompletedEventArgs> OnCompleted; //爬虫完成事件
            public event EventHandler<Exception> OnError;  //爬虫出错事件
            public CookieContainer CookiesContainer { get; set; }  //定义cookie容器
            public SimpleCrawler() { }

            public async Task<string> Start(Uri uri, WebProxy proxy = null)
            {
                return await Task.Run(() =>
                {
                    var pageSource = string.Empty;
                    try
                    {
                        if (this.OnStart != null) this.OnStart(this, new OnStartEventArgs(uri));
                        var watch = new Stopwatch();
                        var request = (HttpWebRequest)WebRequest.Create(uri);
                        request.Accept = "*/*";
                        request.ContentType = "application/x-www-from-urlencoded";
                        request.AllowAutoRedirect = false;
                        request.UserAgent = "Mozilla/5.0 (Windows NT 6.1; Win64; x64; rv:57.0) Gecko/20100101 Firefox/57.0";
                        request.Timeout = 115000;
                        request.Method = "GET";
                        if (proxy != null) request.Proxy = proxy;
                        request.CookieContainer = this.CookiesContainer;
                        request.ServicePoint.ConnectionLimit = int.MaxValue;
                        var response = (HttpWebResponse)request.GetResponse();
                        foreach (Cookie cookie in response.Cookies) this.CookiesContainer.Add(cookie);
                        var stream = response.GetResponseStream();
                        var reader = new StreamReader(stream, Encoding.UTF8);
                        pageSource = reader.ReadToEnd();
                        watch.Stop();
                        var threadId = System.Threading.Thread.CurrentThread.ManagedThreadId;
                        var milliseconds = watch.ElapsedMilliseconds;
                        reader.Close();
                        stream.Close();
                        request.Abort();
                        response.Close();
                        if (this.OnCompleted != null)
                            this.OnCompleted(this, new OnCompletedEventArgs(uri, threadId, milliseconds, pageSource));
                    }
                    catch (Exception e)
                    {
                        if (this.OnError != null) this.OnError(this, e);
                    }
                    return pageSource;
                });
            }
        }


        /// <summary>
        /// 爬虫启动事件
        /// </summary>
        public class OnStartEventArgs
        {
            public Uri Uri { get; set; } //爬虫url地址

            public OnStartEventArgs(Uri uri)
            {
                this.Uri = uri;
            }
        }

        /// <summary>
        /// 爬虫完成事件
        /// </summary>
        public class OnCompletedEventArgs
        {
            public Uri Uri { get; set; }  //爬虫url地址
            public int ThreadId { get; private set; }  //任务线程id
            public string PageSource { get; private set; }  //页面源代码
            public long Milliseconds { get; private set; }  //爬虫请求执行事件

            public OnCompletedEventArgs(Uri uri, int threadId, long milliseconds, string pageSource)
            {
                this.Uri = uri;
                this.ThreadId = threadId;
                this.PageSource = pageSource;
                this.Milliseconds = milliseconds;
            }
        }


    }
}
