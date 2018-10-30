using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CefSharp.MinimalExample.WinForms
{
    class MyRequestHandler: IRequestHandler
    {
        static string user_id = "";
        // get html face

        private static async void get_cookies()
        {
            CefSharp.TaskCookieVisitor _cookieVisitor = new CefSharp.TaskCookieVisitor();

            var result = await Cef.GetGlobalCookieManager().VisitAllCookiesAsync();





            string domain = "";
            string name = "";
            string value = "";
            for (int i = 0; i < result.Count; i++)
            {
                domain = result[i].Domain;

                if (domain.Contains("facebook"))
                {
                    name = result[i].Name;
                    value = result[i].Value;
                    //       MessageBox.Show(domain + ": name=" + name + " value=" + value);
                    //  File.AppendAllText(@"D:\cookies.txt", domain + ": name=" + name + " value=" + value + Environment.NewLine);


                    TextWriter tsw = new StreamWriter(@"D:\cookies.txt", true);

                    //Writing text to the file.
                    tsw.WriteLine(domain + ": name=" + name + " value=" + value + Environment.NewLine);


                    //Close the file.
                    tsw.Close();

                }
            }


        }
        static string get_post(string user_id)
        {
            string html = "";
            get_cookies();

            return html;
        }

        public void OnResourceLoadComplete(IWebBrowser browserControl, IBrowser browser, IFrame frame, IRequest request, IResponse response, UrlRequestStatus status, long receivedContentLength)
        {
            // You can also check the request URL here
            // if (request.Method == "GET" && request.Url.Contains("scontent"))
            if (request.Method == "GET" && request.Url.Contains("facebook"))
            {
                MessageBox.Show("day roi");
            }
            //    https://stackoverflow.com/questions/42536262/how-to-use-image-from-embedded-resource-with-cefsharp
        }

        #region KHONGDUNG
        public bool GetAuthCredentials(IWebBrowser browserControl, IBrowser browser, IFrame frame, bool isProxy, string host, int port, string realm, string scheme, IAuthCallback callback)
        {
            return false;
        }

        public bool OnBeforeBrowse(IWebBrowser browserControl, IBrowser browser, IFrame frame, IRequest request, bool isRedirect)
        {
            if (request.Method == "POST")
            {
                //MessageBox.Show("ok");
            }
            // You can check the Request object for the URL Here
            return false;
        }

        public CefReturnValue OnBeforeResourceLoad(IWebBrowser browserControl, IBrowser browser, IFrame frame, IRequest request, IRequestCallback callback)
        {

            if (request.Method == "POST" && request.Url.Contains("https://www.facebook.com/webgraphql/mutation"))
            {

                //https://stackoverflow.com/questions/16079087/making-a-http-request-using-cefsharp-and-all-its-stored-cookies?rq=1
                // Get last post


                //var a = request;
                var file = request.PostData.Elements[0].Bytes;

                //MemoryStream memstr = new MemoryStream(file1);
                //Image img = Image.FromStream(memstr);
                //img.Save("D:\\ok.jpg");
                string s = Encoding.UTF8.GetString(file, 0, file.Length);
                ////  post_xenzu("face",file);
                ////   MessageBox.Show("ok");
                // MessageBox.Show("ok");

                user_id = Regex.Match(s, "user=(\\d+)").Groups[1].Value;
                string content = Regex.Match(s, "text(.+)ranges").Groups[1].Value;

                //doi 50s roi goi ham nay de lay duong dan anh

                // setTimer de lay coockie; nhung gio co cach hay hon: bat goi in quay lai: 
                //                      1. lay dc noi dung    string content = Regex.Match(s, "text(.+)ranges").Groups[1].Value tu bien post variables=%7B%22client_mutation_id%22%3A%228c8e6a86-71bc-4a9c-9885-0b8ab9485d0e%22%2C%22actor_id%22%3A%22100012603367367%22%2C%22input%22%3A%7B%22actor_id%22%3A%22100012603367367%22%2C%22client_mutation_id%22%3A%22ec447f92-841b-44ff-98ab-58b4fb483f24%22%2C%22source%22%3A%22WWW%22%2C%22audience%22%3A%7B%22web_privacyx%22%3A%22286958161406148%22%7D%2C%22message%22%3A%7B%22text%22%3A%2233%22%2C%22ranges%22%3A[]%7D%2C%22logging%22%3A%7B%22composer_session_id%22%3A%22dfa1139d-2d0e-4679-8dc5-a3251287958b%22%2C%22ref%22%3A%22timeline%22%7D%2C%22with_tags_ids%22%3A[]%2C%22multilingual_translations%22%3A[]%2C%22camera_post_context%22%3A%7B%22deduplication_id%22%3A%22dfa1139d-2d0e-4679-8dc5-a3251287958b%22%2C%22source%22%3A%22composer%22%7D%2C%22composer_source_surface%22%3A%22timeline%22%2C%22composer_entry_point%22%3A%22timeline%22%2C%22composer_entry_time%22%3A1225%2C%22composer_session_events_log%22%3A%7B%22composition_duration%22%3A362%2C%22number_of_keystrokes%22%3A2%7D%2C%22direct_share_status%22%3A%22NOT_SHARED%22%2C%22sponsor_relationship%22%3A%22WITH%22%2C%22web_graphml_migration_params%22%3A%7B%22is_also_posting_video_to_feed%22%3Afalse%2C%22target_type%22%3A%22feed%22%2C%22xhpc_composerid%22%3A%22rc.js_79s%22%2C%22xhpc_context%22%3A%22profile%22%2C%22xhpc_publish_type%22%3A%22FEED_INSERT%22%2C%22xhpc_timeline%22%3Atrue%2C%22waterfall_id%22%3A%22dfa1139d-2d0e-4679-8dc5-a3251287958b%22%7D%2C%22extensible_sprouts_ranker_request%22%3A%7B%22RequestID%22%3A%22ZvBXCwABAAAAJGYyZjBlZWYxLWE4MjQtMDkyNS1lOGM3LWEwOTdlY2U5N2E4NAoAAgAAAABb07bLCwADAAAABE5PVEUGAAQAOAsABQAAABhVTkRJUkVDVEVEX0ZFRURfQ09NUE9TRVIA%22%7D%2C%22place_attachment_setting%22%3A%22HIDE_ATTACHMENT%22%2C%22attachments%22%3A[%7B%22photo%22%3A%7B%22id%22%3A%22555212951575479%22%2C%22tags%22%3A[]%7D%7D]%7D%7D&__user=100012603367367&__a=1&__dyn=7AgNe-4amaUmgDxyHqAyqomzFE9XGiWF3ozzkC-C267Uqzob4q6oF1qbwTwyCwMyWDyoRoK6UnGi4FpFXyEjF3Ea8iGt1m26aUS2SaCx3U945Kuifz8nxm1tyoiyElAx61cxq2fHx2ih0WG7Elxa1CDBzE4C68y2i6rGbx11yufzQaGUB4yCGwgFoaXyUG58oxadUb9EaK5aGf-Egy8do9EOEyh7yVEhyo8fixK8BUjUy6Fo-cGECmUCfzUiVEtyEozeq8yUnDGFUOi6opG8h4axaeKi8wGxm4UGqbKdwByVUOmidxC8xuUdU-rz8mgK8w&__req=e2&__be=1&__pc=PHASED%3ADEFAULT&__rev=4468652&fb_dtsg=AQFaVZnAZyQz%3AAQHB3w83eCFw&jazoest=265817097869011065901218112258658172665111956511016770119&__spin_r=4468652&__spin_b=trunk&__spin_t=1540601547
                //                      2. lay duoc   post --> lay post_fbid o response --> lay duoc thoi gian 555214528241988
                //                         lay thoi gian cac goi GET co domain=https://scontent.fhan3-1.fna.fbcdn.net/v/t1.0-9/44932996_555212954908812_3152838679135780864_n.jpg
                //                         co domain chua scontent vaf thoi gian gan dung 55521


                //using (var fs = new FileStream("D:\\2", FileMode.Create, FileAccess.Write))
                //{
                //    fs.Write(file, 0, file.Length);

                //}


            }




            //if (request.Method == "GET" && request.Url.Contains("t1.0-0"))
            //{
            //    var file1 = request.PostData.Elements[0].Bytes;
            //}


            // You can also check the URL here
            callback.Dispose();
            return CefReturnValue.Continue;
        }


        public bool OnCertificateError(IWebBrowser browserControl, IBrowser browser, CefErrorCode errorCode, string requestUrl, ISslInfo sslInfo, IRequestCallback callback)
        {
            callback.Dispose();
            return false;
        }

        public bool OnOpenUrlFromTab(IWebBrowser browserControl, IBrowser browser, IFrame frame, string targetUrl, WindowOpenDisposition targetDisposition, bool userGesture)
        {
            return false;
        }

        public void OnPluginCrashed(IWebBrowser browserControl, IBrowser browser, string pluginPath)
        {
        }

        public bool OnProtocolExecution(IWebBrowser browserControl, IBrowser browser, string url)
        {
            return false;
        }

        public bool OnQuotaRequest(IWebBrowser browserControl, IBrowser browser, string originUrl, long newSize, IRequestCallback callback)
        {
            callback.Dispose();
            return false;
        }

        public void OnRenderProcessTerminated(IWebBrowser browserControl, IBrowser browser, CefTerminationStatus status)
        {
        }

        public void OnRenderViewReady(IWebBrowser browserControl, IBrowser browser)
        {
        }



        public void OnResourceRedirect(IWebBrowser browserControl, IBrowser browser, IFrame frame, IRequest request, ref string newUrl)
        {
        }

        public bool OnResourceResponse(IWebBrowser browserControl, IBrowser browser, IFrame frame, IRequest request, IResponse response)
        {
            if (request.Method == "GET" && request.Url.Contains("scontent"))
            {

                //ResourceHandler rh = new ResourceHandler();
                //var res = rh.GetResponse(response, 1024, null);
                //            var res =  rh.GetResponse(response, out long responseLength, out string redirectUrl)

            }


            return false;
        }


        public IResponseFilter GetResourceResponseFilter(IWebBrowser browserControl, IBrowser browser, IFrame frame, IRequest request, IResponse response)
        {
            return null;
        }


        public void OnResourceRedirect(IWebBrowser browserControl, IBrowser browser, IFrame frame, IRequest request, IResponse response, ref string newUrl)
        {
        }

        public bool OnSelectClientCertificate(IWebBrowser browserControl, IBrowser browser, bool isProxy, string host, int port, System.Security.Cryptography.X509Certificates.X509Certificate2Collection certificates, ISelectClientCertificateCallback callback)
        {
            callback.Dispose();
            return false;
        }

        public bool OnBeforeBrowse(IWebBrowser chromiumWebBrowser, IBrowser browser, IFrame frame, IRequest request, bool userGesture, bool isRedirect)
        {

            if (request.Method == "POST")
            {
                //MessageBox.Show("ok");
            }
            return false;
        }

        public bool CanGetCookies(IWebBrowser chromiumWebBrowser, IBrowser browser, IFrame frame, IRequest request)
        {
            if (request.Method == "POST")
            {
                // MessageBox.Show("ok");
            }
            return true;
        }

        public bool CanSetCookie(IWebBrowser chromiumWebBrowser, IBrowser browser, IFrame frame, IRequest request, Cookie cookie)
        {
            if (request.Method == "POST")
            {
                //  MessageBox.Show("ok");
            }
            return true;
        }
        #endregion
    }
}
