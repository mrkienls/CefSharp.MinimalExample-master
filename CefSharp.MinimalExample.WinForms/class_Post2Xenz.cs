using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CefSharp.MinimalExample.WinForms
{
    public static class class_Post2Xenz
    {

        public static void post_xenzu(string content, string path_file,string user,string pass)
        {
            var client = new RestClient("https://www.xenzuu.com/index.php");
            var request = new RestRequest(Method.POST);
            CookieContainer _cookieJar = new CookieContainer();
            client.CookieContainer = _cookieJar;
            request.AddHeader("content-type", "application/x-www-form-urlencoded");
            request.AddParameter("mp", "home");
            request.AddParameter("ac", "login");
            request.AddParameter("user", user);
            request.AddParameter("pwd", pass);
            request.AddParameter("autologin", "1");
            IRestResponse response = client.Execute(request);
            string html = response.Content;

            // post
            if (html.ToLower().Contains("logout"))
            {
                string dpostid = Regex.Match(html, "dpostid value='(\\d+)'").Groups[1].Value;
                request.AddParameter("ac", "post_status");
                request.AddParameter("mp", "home");
                request.AddParameter("id_community", "0");
                request.AddParameter("dpostid", dpostid);
                request.AddParameter("txt", Uri.UnescapeDataString(content));
                if (path_file != "")
                {
                    request.AddHeader("content-type", "multipart/form-data");
                    request.AddFile("thefile", path_file, "image.jpg");
                }

                    
                // execute the request
                IRestResponse response1 = client.Execute(request);
            }
            else
            {
            }

            // logout
            var request1 = new RestRequest(Method.GET);
            request1.AddHeader("Referer", "https://www.xenzuu.com/index.php?mp=home");
            request1.AddParameter("mp", "home");
            request1.AddParameter("ac", "logout");
            IRestResponse response2 = client.Execute(request1);
        }

    }
}
