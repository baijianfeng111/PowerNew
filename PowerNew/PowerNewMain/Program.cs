using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using PowerNewMain;

namespace PowerCrawler
{
    class Program
    {

        static void Main(string[] args)
        {
            Console.WriteLine("--------测试开始--------");
            //var d = new CwWords(Cr);
            //d.Invoke();
            Thread t1=new Thread(delegate()
            {
                Thread t = new Thread(new ThreadStart(Cr));
                t.Start();
            } );
            t1.Start();
            Console.WriteLine("--------测试结束--------");
            Console.ReadLine();
            //GetCityAndUri();
            //GetHotels();
            //MoreThread();
            // GetNews();

        }

        public delegate void CwWords();

        public static void Cr()
        {
            for (int i = 0; i < 101; i++)
            {
                Console.WriteLine(i);
            }
        }

        public static void GetCityAndUri()
        {
            var cityUrl = "Http://hotels.ctrip.com/citylist";
            var cityList = new List<City>();
            var cityCrawler = new Crawler.SimpleCrawler();
            cityCrawler.OnStart += (s, e) =>
            {
                Console.WriteLine("爬虫开始抓取地址:" + e.Uri.ToString());
            };
            cityCrawler.OnError += (s, e) =>
            {
                Console.WriteLine("爬虫抓取出现错误:" + e.Message);
            };
            cityCrawler.OnCompleted += (s, e) =>
            {
                //使用正则表达式清洗网页源代码中的数据
                var links = Regex.Matches(e.PageSource,
                    @"<a[^>]+href=""*(?<href>/hotel/[^>\s]+)""\s*[^>]*>(?<text>(?!.*img).*?)</a>", RegexOptions.IgnoreCase);
                foreach (Match match in links)
                {
                    var city = new City()
                    {
                        CityName = match.Groups["text"].Value,
                        Uri = new Uri("http://hotels.ctrip.com" + match.Groups["href"].Value)
                    };
                    if (!cityList.Contains(city)) cityList.Add(city);
                    Console.WriteLine(city.CityName + "|" + city.Uri);

                }
                //Console.WriteLine(e.PageSource);
                Console.WriteLine("********************");
                Console.WriteLine("爬虫抓取任务完成!合计:" + links.Count + "个城市");
                Console.WriteLine("耗时:" + e.Milliseconds + "毫秒");
                Console.WriteLine("线程:" + e.ThreadId);
                Console.WriteLine("地址：" + e.Uri.ToString());
            };
            //cityCrawler.Start(new Uri(cityUrl), new WebProxy("192.168.1.25", 8090)).Wait();
            cityCrawler.Start(new Uri(cityUrl)).Wait();
            Console.ReadKey();
        }


        public static void GetHotels()
        {
            var cityUrl = "http://hotels.ctrip.com/hotel/zhengzhou559";
            var cityList = new List<City>();
            var cityCrawler = new Crawler.SimpleCrawler();
            cityCrawler.OnStart += (s, e) =>
            {
                Console.WriteLine("爬虫开始抓取地址:" + e.Uri.ToString());
            };
            cityCrawler.OnError += (s, e) =>
            {
                Console.WriteLine("爬虫抓取出现错误:" + e.Message);
            };
            cityCrawler.OnCompleted += (s, e) =>
            {
                //使用正则表达式清洗网页源代码中的数据
                var links = Regex.Matches(e.PageSource,
                    @"><a[^>]+href=""*(?<href>/hotel/[^>\s]+)""\s*data-dopost[^>]*><span[^>]+>.*?</span>(?<text>.*?)</a>", RegexOptions.IgnoreCase);
                foreach (Match match in links)
                {
                    var city = new City()
                    {
                        CityName = match.Groups["text"].Value,
                        Uri = new Uri("http://hotels.ctrip.com" + match.Groups["href"].Value)
                    };
                    if (!cityList.Contains(city)) cityList.Add(city);
                    Console.WriteLine(city.CityName + "|" + city.Uri);

                }
                //Console.WriteLine(e.PageSource);
                Console.WriteLine("********************");
                Console.WriteLine("爬虫抓取任务完成!合计:" + links.Count + "个酒店");
                Console.WriteLine("耗时:" + e.Milliseconds + "毫秒");
                Console.WriteLine("线程:" + e.ThreadId);
                Console.WriteLine("地址：" + e.Uri.ToString());
            };
            //cityCrawler.Start(new Uri(cityUrl), new WebProxy("192.168.1.25", 8090)).Wait();
            cityCrawler.Start(new Uri(cityUrl)).Wait();
            Console.ReadKey();
        }

        public static void MoreThread()
        {
            //var cityUrl = "http://hotels.ctrip.com/hotel/zhengzhou559";
            var hotelList = new List<Hotel>()
            {
                new Hotel(){HotelName = "河南天地粤海酒店",Uri=new Uri("http://hotels.ctrip.com/hotel/501757.html?isFull=F")},
                new Hotel(){HotelName = "郑州中都饭店",Uri=new Uri("http://hotels.ctrip.com/hotel/1318801.html?isFull=F")}
            };
            var cityList = new List<City>();
            var cityCrawler = new Crawler.SimpleCrawler();
            cityCrawler.OnStart += (s, e) =>
            {
                Console.WriteLine("爬虫开始抓取地址:" + e.Uri.ToString());
            };
            cityCrawler.OnError += (s, e) =>
            {
                Console.WriteLine("爬虫抓取出现错误:" + e.Message);
            };
            cityCrawler.OnCompleted += (s, e) =>
            {
                Console.WriteLine("********************");
                Console.WriteLine("爬虫抓取任务完成!");
                Console.WriteLine("耗时:" + e.Milliseconds + "毫秒");
                Console.WriteLine("线程:" + e.ThreadId);
                Console.WriteLine("地址：" + e.Uri.ToString());
            };
            Parallel.For(0, 2, (i) =>
            {
                var hotel = hotelList[i];
                cityCrawler.Start(hotel.Uri).Wait();
            });
            Console.ReadKey();
        }

        public static void GetNews()
        {
            var cityUrl = "http://www.rompy.cn/Center/Index";
            var cityList = new List<Rompy>();
            var cityCrawler = new Crawler.SimpleCrawler();
            cityCrawler.OnStart += (s, e) =>
            {
                Console.WriteLine("爬虫开始抓取地址:" + e.Uri.ToString());
            };
            cityCrawler.OnError += (s, e) =>
            {
                Console.WriteLine("爬虫抓取出现错误:" + e.Message);
            };
            cityCrawler.OnCompleted += (s, e) =>
            {
                //使用正则表达式清洗网页源代码中的数据
                var links = Regex.Matches(e.PageSource, @"<a[^>]*?>(?<Text>[^<]*)</a>", RegexOptions.IgnoreCase);
                foreach (Match match in links)
                {
                    var city = new Rompy()
                    {
                        CityName = match.Groups[1].Value,
                        ShopName = ""
                    };
                    if (!cityList.Contains(city)) cityList.Add(city);
                    Console.WriteLine(city.CityName + "|" + city.ShopName);

                }
                //Console.WriteLine(e.PageSource);
                Console.WriteLine("********************");
                Console.WriteLine("爬虫抓取任务完成!合计:" + links.Count + "个城市");
                Console.WriteLine("耗时:" + e.Milliseconds + "毫秒");
                Console.WriteLine("线程:" + e.ThreadId);
                Console.WriteLine("地址：" + e.Uri.ToString());
            };
            //cityCrawler.Start(new Uri(cityUrl), new WebProxy("192.168.1.25", 8090)).Wait();
            cityCrawler.Start(new Uri(cityUrl)).Wait();
            Console.ReadKey();
        }
    }

    public class City
    {
        public string CityName { get; set; }
        public Uri Uri { get; set; }
    }

    public class Rompy
    {
        public string CityName { get; set; }
        public string ShopName { get; set; }
    }

    public class Hotel
    {
        public string HotelName { get; set; }
        public Uri Uri { get; set; }
    }
}
