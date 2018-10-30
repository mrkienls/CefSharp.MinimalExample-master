using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CefSharp.MinimalExample.WinForms
{
    class classGetDataFromFB
    {

        public classGetDataFromFB() {
          //  MessageBox.Show("ok");
        }

        #region properties

        #endregion

        #region methods
        //1. Lay noi dung HTTP request --> exact content & time_id
        result_post GetContentRequest_WhenPost()
        {
            result_post result = new result_post();
            result.content = "";
            result.time_id = "";
            
            return result;
        }


        string  GetPathImage_WhenPost(string time_id)
        {
            string urlImage = "";

            return urlImage;
        }


        void SaveImage(string urlImage, string path_to_save)
        {

        }
        #endregion
    }

    class result_post
    {
        public string content { get; set; }
        public string time_id { get; set; }
    }
}
