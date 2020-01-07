using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WpfApp1
{
    public class MoexData
    {
        public string _url { get; set; }
        public CookieContainer _Cookies { get; set; }
        public MoexData(string url)
        {
            _url = url;

            var Cookies = new CookieContainer();
            Cookies.Add(new Uri(url), new Cookie("__utma", "241266590.628378023.1576083282.1578083639.1578137807.4"));
            Cookies.Add(new Uri(url), new Cookie("__utmc", "241266590"));
            Cookies.Add(new Uri(url), new Cookie("__utmz", "241266590.1578137807.4.4.utmcsr=www.moex.com|utmccn=(not set)|utmcmd=(not set)|utmctr=" + Convert.ToBase64String(Encoding.UTF8.GetBytes("Ð½Ð¾ÑÐ½Ð¸")) ));
            Cookies.Add(new Uri(url), new Cookie("MicexTrackID", "176.193.136.174.1578083618773491"));
            Cookies.Add(new Uri(url), new Cookie("MPTrack", "2863d2d5.59b4239847a5b"));
            _Cookies = Cookies;
        }

        public string GetWebData()
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(new Uri(_url));

            request.Method = "GET";
            request.Accept = "*/*";
            request.CookieContainer = _Cookies;
            request.Host = "iss.moex.com";
            request.UseDefaultCredentials = true;
            request.UserAgent = "Mozilla/4.0 (compatible; MSIE 5.01; Windows NT 5.0)";
            request.KeepAlive = true;
            request.Referer = "https://www.moex.com/ru/issue.aspx?board=TQBR&code=GMKN&utm_source=www.moex.com&utm_term=%D0%BD%D0%BE%D1%80%D0%BD%D0%B8";
            
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            Stream resStream = response.GetResponseStream();

            var Sr = new StreamReader(resStream, Encoding.UTF8);
            var result = Sr.ReadToEnd();

            return ConvertStrData(result);
        }

        private string ConvertStrData(string result)
        {
            string data = result.Split(':')[7].Remove(0, 1).Replace(", \"volumes\"", "").Replace("}]", "");
            string FixData = data.Replace(" ", "").Replace("[[", "").Replace("]]", "").Replace("],[", " ");
            return FixData;
        }
    }
}
