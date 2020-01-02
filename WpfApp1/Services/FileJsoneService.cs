using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Runtime.Serialization.Json;
using System.Text;
using WpfApp1.Models;

namespace WpfApp1.Services
{
    public interface IFileService
    {
        organizationSummaryWithNsiList Open(string filename);
        void Save(string filename, List<ResultData> phonesList);
        organizationSummaryWithNsiList GetPostWebData(string url, CookieContainer Cook);
        CompanyInfo GetWebData(string url, CookieContainer Cook);
    }

    public class FileJsoneService : IFileService
    {
        public organizationSummaryWithNsiList Open(string filename)
        {
            var data = new organizationSummaryWithNsiList();
            var jsonFormatter =
                new DataContractJsonSerializer(typeof(organizationSummaryWithNsiList));
            
            using (FileStream fs = new FileStream(filename, FileMode.Open, FileAccess.Read))
            {
                data = jsonFormatter.ReadObject(fs) as organizationSummaryWithNsiList;
            }

            return data;
        }

        public void Save(string filename, List<ResultData> jsonObj)
        {
            DataContractJsonSerializer jsonFormatter =
                new DataContractJsonSerializer(typeof(List<ResultData>));
            using (FileStream fs = new FileStream(filename, FileMode.Create))
            {
                jsonFormatter.WriteObject(fs, jsonObj);
            }
        }


        public organizationSummaryWithNsiList GetPostWebData(string url, CookieContainer Cook)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(new Uri(url));
            request.Method = "POST";
            request.ContentType = "application/json;charset=utf-8";
            request.Accept = "application/json; charset=utf-8";

            request.CookieContainer = Cook;
            request.Host = "dom.gosuslugi.ru";
            request.UseDefaultCredentials = true;

            request.UserAgent = "Mozilla/4.0 (compatible; MSIE 5.01; Windows NT 5.0)";
            
            request.KeepAlive = true;
            request.Referer = "https://dom.gosuslugi.ru/";
            request.AllowAutoRedirect = false;

            string json = "{\"organizationRoleList\":[{\"code\":\"19\",\"guid\":\"708b663b - 8cb8 - 4114 - 84bc - e6b909c7f714\"},{\"code\":\"20\",\"guid\":\"b9468598 - 0535 - 4d76 - a462 - 32082dce86b7\"},{\"code\":\"21\",\"guid\":\"d7625e16 - 19fd - 4dea - a3e3 - 3999a195e1c9\"},{\"code\":\"22\",\"guid\":\"5c408b08 - 0a95 - 4ea2 - 8d78 - 55af6e6a5ac5\"},{\"code\":\"1\",\"guid\":\"52f5e0a4 - 7f4c - 4fb1 - 9423 - 160e9178ddda\"}],\"factualAddress\":{\"area\":null,\"city\":null,\"region\":null,\"settlement\":null,\"street\":null,\"planningStructureElement\":null,\"house\":null,\"houseNumber\":null,\"buildingNumber\":null,\"structNumber\":null},\"organizationStatuses\":{\"coll\":[\"REGISTERED\"],\"operand\":\"OR\"},\"okopfList\":[],\"roleStatuses\":[\"APPROVED\"],\"onlyHeadOrganization\":true}";
            using (var streamWriter = new StreamWriter(request.GetRequestStream()))
            {

                streamWriter.Write(json);
                streamWriter.Flush();
            }

            request.ContentLength = json.Length;

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            Stream resStream = response.GetResponseStream();


            var data = new organizationSummaryWithNsiList();
            var jsonFormatter =
                new DataContractJsonSerializer(typeof(organizationSummaryWithNsiList));

            
            data = jsonFormatter.ReadObject(resStream) as organizationSummaryWithNsiList;
            

            return data;
        }


        public CompanyInfo GetWebData(string url, CookieContainer Cook)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(new Uri(url));
            request.Method = "GET";
            request.ContentType = "application/json;charset=utf-8";
            request.Accept = "application/json; charset=utf-8";

            request.CookieContainer = Cook;
            request.Host = "dom.gosuslugi.ru";
            request.UseDefaultCredentials = true;

            request.UserAgent = "Mozilla/4.0 (compatible; MSIE 5.01; Windows NT 5.0)";

            request.KeepAlive = true;
            request.Referer = "https://dom.gosuslugi.ru/";
            request.AllowAutoRedirect = false;

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            Stream resStream = response.GetResponseStream();

            var data = new CompanyInfo();
            var jsonFormatter =
                new DataContractJsonSerializer(typeof(CompanyInfo));

            data = jsonFormatter.ReadObject(resStream) as CompanyInfo;

            return data;
        }
    }
}
