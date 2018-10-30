using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CefSharp.MinimalExample.WinForms
{
    class ThamKhao
    {

      // Copyright © 2010-2015 The CefSharp Authors. All rights reserved.
//
// Use of this source code is governed by a BSD-style license that can be found in the LICENSE file.

using System;
using System.Collections.Generic;
using System.Windows.Forms;
using CefSharp.MinimalExample.WinForms.Controls;
using CefSharp.WinForms;
using System.Linq;
using System.IO;
using RestSharp;
using CefSharp;
using System.Net;
using System.Text.RegularExpressions;
using System.Text;
using System.Drawing;
using RestSharp;
using System.Collections;
using System.Threading;
using System.Timers;
using System.Collections.Specialized;


// cefsharp with tabs
//https://www.youtube.com/watch?v=itpEj1yB_Tg


// listen event
//https://stackoverflow.com/questions/28067549/how-to-trap-listen-javascript-function-or-events-in-cefsharp
//How do you handle a Javascript event in C#?
//http://jslim.net/blog/2018/01/30/CefSharp-listen-to-JavaScript-event/
namespace CefSharp.MinimalExample.WinForms
    {


        public partial class BrowserForm : Form
        {
            private readonly ChromiumWebBrowser browser;
            private string contentPost = "";

            public BrowserForm()
            {
                var tempBuffer = File.ReadAllBytes("D:\\kien.jpg");
                // post_xenzu("kinqua", tempBuffer);




                // check key
                string key = Properties.Settings.Default.key;
                string key_sever = Kien.get_pass();
                // neu dung pass thi chay binh thuong
                if (key == key_sever)
                {
                    InitializeComponent();
                    Text = "CefSharp";
                    WindowState = FormWindowState.Maximized;

                    browser = new ChromiumWebBrowser("https://www.facebook.com")
                    //   browser = new ChromiumWebBrowser("https://www.xenzuu.com/")
                    {
                        Dock = DockStyle.Fill,
                        RequestHandler = new MyRequestHandler()
                    };





                    //    toolStripContainer.ContentPanel.Controls.Add(browser);
                    browser.Parent = tabControl1.SelectedTab;
                    browser.IsBrowserInitializedChanged += OnIsBrowserInitializedChanged;
                    browser.LoadingStateChanged += OnLoadingStateChanged;
                    browser.ConsoleMessage += OnBrowserConsoleMessage;
                    browser.StatusMessage += OnBrowserStatusMessage;
                    browser.TitleChanged += OnBrowserTitleChanged;
                    browser.AddressChanged += OnBrowserAddressChanged;



                    // listen event from web
                    CefSharpSettings.LegacyJavascriptBindingEnabled = true;
                    var obj = new BoundObject(browser);
                    browser.RegisterJsObject("bound", obj);
                    browser.FrameLoadEnd += obj.OnFrameLoadEnd;


                    var bitness = Environment.Is64BitProcess ? "x64" : "x86";
                    var version = String.Format("Chromium: {0}, CEF: {1}, CefSharp: {2}, Environment: {3}", Cef.ChromiumVersion, Cef.CefVersion, Cef.CefSharpVersion, bitness);
                    DisplayOutput(version);
                }
                else
                {
                    // neu sai key thi hien form nhap key
                    this.Hide();
                    frmSetting formSetting = new frmSetting();
                    formSetting.Show();
                    formSetting.TopMost = true;
                }
            }



            // dang nhap xenzu
            public void post_xenzu(string content, byte[] file)
            {
                var client = new RestClient("https://www.xenzuu.com/index.php");
                var request = new RestRequest(Method.POST);
                CookieContainer _cookieJar = new CookieContainer();
                client.CookieContainer = _cookieJar;

                request.AddHeader("content-type", "application/x-www-form-urlencoded");
                request.AddParameter("mp", "home");
                request.AddParameter("ac", "login");
                request.AddParameter("user", "nguyenthuylinhls");
                request.AddParameter("pwd", "cstd1234");
                request.AddParameter("autologin", "1");

                IRestResponse response = client.Execute(request);


                string html = response.Content;

                // post
                if (html.ToLower().Contains("logout"))
                {


                    string dpostid = Regex.Match(html, "dpostid value='(\\d+)'").Groups[1].Value;
                    //MessageBox.Show(dpostid);
                    //httpRequest.Referer = url;
                    //httpRequest.AddHeader("Accept - Encoding", "gzip, deflate, br");
                    //httpRequest.AddFile("thefile", @"C:\Users\data\Desktop\PhotoshopPortable\test\1.jpg");
                    ////   html = httpRequest.Post("https://www.xenzuu.com/index.php?mp=home", "max_file_size=1000000000&ac=post_status&mp=home&id_community=0&dpostid="+ dpostid  + "&txt=hôm nay là ngày 10 tháng 10&thefile=&visibility=0", "application/x-www-form-urlencoded").ToString();
                    //html = httpRequest.Post("https://www.xenzuu.com/index.php?mp=home", "max_file_size=1000000000&ac=post_status&mp=home&id_community=0&dpostid=" + dpostid + "&txt=ok&visibility=0", "application/x-www-form-urlencoded").ToString();

                    request.AddParameter("max_file_size", "1000000000");
                    request.AddParameter("ac", "post_status");
                    request.AddParameter("mp", "home");
                    request.AddParameter("id_community", "0");
                    request.AddParameter("dpostid", dpostid);
                    request.AddParameter("txt", content);
                    request.AddHeader("content-type", "multipart/form-data");
                    //read file content as byte array

                    request.AddFile("thefile", file, "kien.jpg");

                    // execute the request
                    IRestResponse response1 = client.Execute(request);


                }
                else
                {
                    //   MessageBox.Show("false");
                }

                // logout

                //  var client1 = new RestClient("https://www.xenzuu.com/index.php?mp=home&ac=logout");
                var request1 = new RestRequest(Method.GET);
                ////  client.CookieContainer = _cookieJar;
                //var request1 = new RestRequest("https://www.xenzuu.com/index.php?mp=home&ac=logout");
                //request1.Method = Method.GET;
                request1.AddHeader("Referer", "https://www.xenzuu.com/index.php?mp=home");

                request1.AddParameter("mp", "home");
                request1.AddParameter("ac", "logout");
                //request1.AddHeader("Host", "www.xenzuu.com");
                //request1.AddHeader("User-Agent", "Mozilla/5.0 (Windows NT 6.2; rv:62.0) Gecko/20100101 Firefox/62.0");
                //request1.AddHeader("Accept", "text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8");
                //request1.AddHeader("Accept-Language", "en-US,en;q=0.5");
                //request1.AddHeader("Accept-Encoding", "gzip, deflate, br");

                IRestResponse response2 = client.Execute(request1);





            }




            private void OnIsBrowserInitializedChanged(object sender, IsBrowserInitializedChangedEventArgs e)
            {
                if (e.IsBrowserInitialized)
                {
                    var b = ((ChromiumWebBrowser)sender);

                    this.InvokeOnUiThreadIfRequired(() => b.Focus());
                }
            }

            private void OnBrowserConsoleMessage(object sender, ConsoleMessageEventArgs args)
            {
                DisplayOutput(string.Format("Line: {0}, Source: {1}, Message: {2}", args.Line, args.Source, args.Message));
            }

            private void OnBrowserStatusMessage(object sender, StatusMessageEventArgs args)
            {
                this.InvokeOnUiThreadIfRequired(() => statusLabel.Text = args.Value);
            }

            private async void OnLoadingStateChanged(object sender, LoadingStateChangedEventArgs args)
            {



                // get post from FB
                if (args.IsLoading == false)
                {
                    // var html = browser.EvaluateScriptAsync("document.getElementById('js_16').innerHTML;").ToString();
                    //     MessageBox.Show(html.ToString());


                    // string script = string.Format("document.getElementsByClassName('userContentWrapper')[0].children[0].children[1].innerHTML;");

                    // Lay noi dung post dau tien cuar facebook
                    if (browser.Address.Contains("facebook.com"))

                    {
                        // browser.DialogHandler = new TempFileDialogHandler("no");

                        //string script = string.Format("document.getElementsByClassName('userContent')[0].innerHTML;");

                        //browser.EvaluateScriptAsync(script).ContinueWith(x =>
                        //{
                        //    var response = x.Result;

                        //    if (response.Success && response.Result != null)
                        //    {
                        //    // lay dc noi dung post
                        //    contentPost = response.Result.ToString();
                        //        MessageBox.Show("1 " + contentPost);
                        //        LoadUrl("https://xenzuu.com/");
                        //    // mo web xenzuu
                        //     }
                        //    MessageBox.Show("2" + contentPost);
                        //});
                        //MessageBox.Show("3" + contentPost);

                        //string script = "(function() {return document.getElementsByClassName('userContent')[0].outerHTML;})();";
                        //string script = string.Format("document.getElementsByClassName('userContent')[0].outerHTML;");

                        //var task = browser.EvaluateScriptAsync(script);

                        //await task.ContinueWith(t =>
                        //{
                        //    if (!t.IsFaulted)
                        //    {
                        //        var response = t.Result;

                        //        if (response.Success && response.Result != null)
                        //        {
                        //            contentPost = response.Result.ToString();

                        //            LoadUrl("https://xenzuu.com/");
                        //        }
                        //    }
                        //});

                    }





                    if (browser.Address == "https://www.xenzuu.com/" || browser.Address == "https://xenzuu.com/")

                    {
                        browser.DialogHandler = new TempFileDialogHandler("D:\\kien.jpg");

                        //    // dang nhap xenzu

                        //    browser.EvaluateScriptAsync("document.getElementsByName('user').value='nguyentrungkienctn';");
                        //    browser.EvaluateScriptAsync("document.getElementsByName('pwd').value='cstd1234';");
                        //    browser.EvaluateScriptAsync("document.getElementsByClassName('login_button')[0].click();");
                        //}

                        //// sau khi login xong, dang bai
                        //if (browser.Address == "https://www.xenzuu.com/index.php?mp=home" || browser.Address == "https://xenzuu.com/index.php?mp=home")
                        //{
                        //    MessageBox.Show(contentPost);
                        //    browser.EvaluateScriptAsync("document.getElementById('was_machst_du').innerHTML='" + contentPost + "';");
                        //    browser.EvaluateScriptAsync("document.getElementsByClassName('blue_button')[0].click();");
                        //   // MessageBox.Show("kk" + contentPost);
                    }

                }





                SetCanGoBack(args.CanGoBack);
                SetCanGoForward(args.CanGoForward);

                this.InvokeOnUiThreadIfRequired(() => SetIsLoading(!args.CanReload));
            }

            private void OnBrowserTitleChanged(object sender, TitleChangedEventArgs args)
            {
                this.InvokeOnUiThreadIfRequired(() => Text = args.Title);
            }

            private void OnBrowserAddressChanged(object sender, AddressChangedEventArgs args)
            {
                this.InvokeOnUiThreadIfRequired(() => urlTextBox.Text = args.Address);
            }

            private void SetCanGoBack(bool canGoBack)
            {
                this.InvokeOnUiThreadIfRequired(() => backButton.Enabled = canGoBack);
            }

            private void SetCanGoForward(bool canGoForward)
            {
                this.InvokeOnUiThreadIfRequired(() => forwardButton.Enabled = canGoForward);
            }

            private void SetIsLoading(bool isLoading)
            {
                goButton.Text = isLoading ?
                    "Stop" :
                    "Go";
                goButton.Image = isLoading ?
                    Properties.Resources.nav_plain_red :
                    Properties.Resources.nav_plain_green;

                HandleToolStripLayout();
            }

            public void DisplayOutput(string output)
            {
                this.InvokeOnUiThreadIfRequired(() => outputLabel.Text = output);
            }

            private void HandleToolStripLayout(object sender, LayoutEventArgs e)
            {
                HandleToolStripLayout();
            }

            private void HandleToolStripLayout()
            {
                var width = toolStrip1.Width;
                foreach (ToolStripItem item in toolStrip1.Items)
                {
                    if (item != urlTextBox)
                    {
                        width -= item.Width - item.Margin.Horizontal;
                    }
                }
                urlTextBox.Width = Math.Max(0, width - urlTextBox.Margin.Horizontal - 18);
            }

            private void ExitMenuItemClick(object sender, EventArgs e)
            {

                browser.Dispose();
                Cef.Shutdown();
                Close();
            }

            private void GoButtonClick(object sender, EventArgs e)
            {
                LoadUrl(urlTextBox.Text);
            }

            private void BackButtonClick(object sender, EventArgs e)
            {
                ChromiumWebBrowser brow = tabControl1.SelectedTab.Controls[0] as ChromiumWebBrowser;
                brow.Back();
            }

            private void ForwardButtonClick(object sender, EventArgs e)
            {
                ChromiumWebBrowser brow = tabControl1.SelectedTab.Controls[0] as ChromiumWebBrowser;
                brow.Forward();
            }

            private void UrlTextBoxKeyUp(object sender, KeyEventArgs e)
            {
                if (e.KeyCode != Keys.Enter)
                {
                    return;
                }

                LoadUrl(urlTextBox.Text);
            }

            private void LoadUrl(string url)
            {
                ChromiumWebBrowser brow = tabControl1.SelectedTab.Controls[0] as ChromiumWebBrowser;
                if (Uri.IsWellFormedUriString(url, UriKind.RelativeOrAbsolute))
                {
                    brow.Load(url);
                    //  MessageBox.Show(url);
                }
            }

            private void ShowDevToolsMenuItemClick(object sender, EventArgs e)
            {

            }




            private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
            {

            }

            private void settingToolStripMenuItem_Click(object sender, EventArgs e)
            {
                browser.Show();
            }

            private void settingToolStripMenuItem_Click_1(object sender, EventArgs e)
            {

            }

            private void BrowserForm_Load(object sender, EventArgs e)
            {

            }

            private void settingToolStripMenuItem_Click_2(object sender, EventArgs e)
            {
                //frmSetting
                this.Hide();
                frmSetting formSetting = new frmSetting();
                formSetting.Show();
                formSetting.TopMost = true;
            }

            private void BrowserForm_Load_1(object sender, EventArgs e)
            {
            }

            private void newTabToolStripMenuItem_Click(object sender, EventArgs e)
            {
                TabPage tab = new TabPage();
                tab.Text = "New tab";
                tabControl1.Controls.Add(tab);
                tabControl1.SelectTab(tabControl1.TabCount - 1);
                ChromiumWebBrowser browser1 = new ChromiumWebBrowser("https://google.com/");
                browser1.Parent = tabControl1.SelectedTab;

                browser1.Parent = tabControl1.SelectedTab;
                browser1.IsBrowserInitializedChanged += OnIsBrowserInitializedChanged;
                browser1.LoadingStateChanged += OnLoadingStateChanged;
                browser1.ConsoleMessage += OnBrowserConsoleMessage;
                browser1.StatusMessage += OnBrowserStatusMessage;
                browser1.TitleChanged += OnBrowserTitleChanged;
                browser1.AddressChanged += OnBrowserAddressChanged;



                var bitness = Environment.Is64BitProcess ? "x64" : "x86";
                var version = String.Format("Chromium: {0}, CEF: {1}, CefSharp: {2}, Environment: {3}", Cef.ChromiumVersion, Cef.CefVersion, Cef.CefSharpVersion, bitness);
                DisplayOutput(version);
            }
        }

        //public class TempFileDialogHandler : IDialogHandler
        //{
        //    public bool OnFileDialog(IWebBrowser browserControl, IBrowser browser, CefFileDialogMode mode, CefFileDialogFlags flags, string title, string defaultFilePath, List<string> acceptFilters, int selectedAcceptFilter, IFileDialogCallback callback)
        //    {
        //        //callback.Continue(selectedAcceptFilter, new List<string> { Path.GetRandomFileName() });
        //        callback.Continue(selectedAcceptFilter, new List<string> { "D:\\kien.jpg" });

        //        return true;
        //    }
        //}


        public class TempFileDialogHandler : IDialogHandler
        {
            string[] _filePath;

            public TempFileDialogHandler(params string[] filePath)
            {
                _filePath = filePath;
            }


            public bool OnFileDialog(IWebBrowser browserControl, IBrowser browser, CefFileDialogMode mode, CefFileDialogFlags flags, string title, string defaultFilePath, List<string> acceptFilters, int selectedAcceptFilter, IFileDialogCallback callback)
            {
                MessageBox.Show(_filePath[0].ToString());
                if (_filePath[0].ToString().Contains("jpg"))
                {
                    callback.Continue(0, _filePath.ToList());
                }
                else
                {
                    callback.Continue(selectedAcceptFilter, new List<string> { Path.GetRandomFileName() });
                }

                return true;
            }

            //public bool OnFileDialog(IWebBrowser browserControl, IBrowser browser, CefFileDialogMode mode, CefFileDialogFlags flags, string title, string defaultFilePath, List<string> acceptFilters, int selectedAcceptFilter, IFileDialogCallback callback)
            //{
            //    callback.Continue(selectedAcceptFilter, new List<string> { Path.GetRandomFileName() });

            //    return true;
            //}
        }

        public class BoundObject
        {
            private ChromiumWebBrowser browser;

            public BoundObject(ChromiumWebBrowser br) { browser = br; }

            public void OnFrameLoadEnd(object sender, FrameLoadEndEventArgs e)
            {
                if (e.Frame.IsMain)
                {
                    browser.ExecuteScriptAsync(@"
             document.body.onclick = function()
             {
                   bound.onSelected(window.getSelection().toString());
             }
            ");
                }




                //if (e.Frame.IsMain)
                //{
                //    browser.ExecuteScriptAsync(@"
                // document.body.onmouseup = function()
                // {
                //   bound.onSelected(window.getSelection().toString());
                // }
                //");
                //}
            }
            public void OnSelected(string selected)
            {
                // MessageBox.Show("The user selected some text [" + selected + "]");
            }
        }




        public class MyRequestHandler : IRequestHandler
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

            public void post_xenzu(string content, byte[] file)
            {
                var client = new RestClient("https://www.xenzuu.com/index.php");
                var request = new RestRequest(Method.POST);
                CookieContainer _cookieJar = new CookieContainer();
                client.CookieContainer = _cookieJar;

                request.AddHeader("content-type", "application/x-www-form-urlencoded");
                request.AddParameter("mp", "home");
                request.AddParameter("ac", "login");
                request.AddParameter("user", "nguyenthuylinhls");
                request.AddParameter("pwd", "cstd1234");
                request.AddParameter("autologin", "1");

                IRestResponse response = client.Execute(request);


                string html = response.Content;

                // post
                if (html.ToLower().Contains("logout"))
                {


                    string dpostid = Regex.Match(html, "dpostid value='(\\d+)'").Groups[1].Value;
                    //MessageBox.Show(dpostid);
                    //httpRequest.Referer = url;
                    //httpRequest.AddHeader("Accept - Encoding", "gzip, deflate, br");
                    //httpRequest.AddFile("thefile", @"C:\Users\data\Desktop\PhotoshopPortable\test\1.jpg");
                    ////   html = httpRequest.Post("https://www.xenzuu.com/index.php?mp=home", "max_file_size=1000000000&ac=post_status&mp=home&id_community=0&dpostid="+ dpostid  + "&txt=hôm nay là ngày 10 tháng 10&thefile=&visibility=0", "application/x-www-form-urlencoded").ToString();
                    //html = httpRequest.Post("https://www.xenzuu.com/index.php?mp=home", "max_file_size=1000000000&ac=post_status&mp=home&id_community=0&dpostid=" + dpostid + "&txt=ok&visibility=0", "application/x-www-form-urlencoded").ToString();

                    request.AddParameter("max_file_size", "1000000000");
                    request.AddParameter("ac", "post_status");
                    request.AddParameter("mp", "home");
                    request.AddParameter("id_community", "0");
                    request.AddParameter("dpostid", dpostid);
                    request.AddParameter("txt", content);
                    request.AddHeader("content-type", "multipart/form-data");
                    //read file content as byte array

                    request.AddFile("thefile", file, "kien.jpg");

                    // execute the request
                    IRestResponse response1 = client.Execute(request);


                }
                else
                {
                    //   MessageBox.Show("false");
                }

                // logout

                //  var client1 = new RestClient("https://www.xenzuu.com/index.php?mp=home&ac=logout");
                var request1 = new RestRequest(Method.GET);
                ////  client.CookieContainer = _cookieJar;
                //var request1 = new RestRequest("https://www.xenzuu.com/index.php?mp=home&ac=logout");
                //request1.Method = Method.GET;
                request1.AddHeader("Referer", "https://www.xenzuu.com/index.php?mp=home");

                request1.AddParameter("mp", "home");
                request1.AddParameter("ac", "logout");
                //request1.AddHeader("Host", "www.xenzuu.com");
                //request1.AddHeader("User-Agent", "Mozilla/5.0 (Windows NT 6.2; rv:62.0) Gecko/20100101 Firefox/62.0");
                //request1.AddHeader("Accept", "text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8");
                //request1.AddHeader("Accept-Language", "en-US,en;q=0.5");
                //request1.AddHeader("Accept-Encoding", "gzip, deflate, br");

                IRestResponse response2 = client.Execute(request1);





            }

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
                    SetTimer();

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

            private static System.Timers.Timer aTimer;
            private static void SetTimer()
            {
                // Create a timer with a two second interval.
                aTimer = new System.Timers.Timer(10000);
                // Hook up the Elapsed event for the timer. 
                aTimer.Elapsed += OnTimedEvent;
                aTimer.AutoReset = true;
                aTimer.Enabled = true;
            }

            private static void OnTimedEvent(Object source, ElapsedEventArgs e)
            {
                string html = get_post(user_id);
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

            public void OnResourceLoadComplete(IWebBrowser browserControl, IBrowser browser, IFrame frame, IRequest request, IResponse response, UrlRequestStatus status, long receivedContentLength)
            {
                // You can also check the request URL here
                if (request.Method == "GET" && request.Url.Contains("scontent"))
                {

                }
                //    https://stackoverflow.com/questions/42536262/how-to-use-image-from-embedded-resource-with-cefsharp
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
        }



        //https://github.com/cefsharp/CefSharp/pull/1519/commits/8d00a3dd2632558acc83e60c869303d9862f4091

        public class ResponseFilter : IResponseFilter
        {
            bool IResponseFilter.InitFilter()
            {
                return true;
            }
            //FilterStatus IResponseFilter.Filter(Stream dataIn, long dataInSize, out long dataInRead, Stream dataOut, long dataOutSize, out long dataOutWritten)
            FilterStatus IResponseFilter.Filter(Stream dataIn, out long dataInRead, Stream dataOut, out long dataOutWritten)
            {
                dataInRead = dataInSize;
                dataOutWritten = Math.Min(dataInRead, dataOutSize);
                dataIn.CopyTo(dataOut);
                return FilterStatus.Done;
            }

            public FilterStatus Filter(Stream dataIn, out long dataInRead, Stream dataOut, out long dataOutWritten)
            {
                throw new NotImplementedException();
            }

            public void Dispose()
            {
                throw new NotImplementedException();
            }
        }







    }


}
}
