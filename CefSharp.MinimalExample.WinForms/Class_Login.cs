using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CefSharp.MinimalExample.WinForms
{
    public static class Class_Login
    {
         static string getFileLogin()
        {
            // string url = "https://drive.google.com/file/d/1CAZM4AIY-C0G-xqKolcFm2riQpUJpkrh/view?usp=sharing";
            string url = "https://github.com/mrkienls/valid/blob/master/fb";
    

            var client = new RestClient(url);
                var request = new RestRequest(url, Method.GET);
            IRestResponse response = client.Execute(request);
            var html = response.Content;


             string content = Regex.Match(html, "description\" content=\"(\\w+)\"").Groups[1].Value;
            return content;
         
        }




        public static bool CheckLogin()
        {
            getFileLogin();
            return true;
        }

        public static void saveToSettings()
        {
        }
    }
}
