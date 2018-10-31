using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using WordsMatching;

namespace CefSharp.MinimalExample.WinForms
{
    static public class classGetDataFromFB
    {


        #region properties

        #endregion

        #region methods
        //1. Lay noi dung HTTP request --> exact content & time_id

        // return 1 chuoi:  user_id, content_post, time_id (chi con thieu duong dan anh)
        static public string[] GetContentRequest_WhenPost(IRequest request) 

        {
            string[] data = {"","","" };
        
            var file = request.PostData.Elements[0].Bytes;
         //   string s = Uri.UnescapeDataString(Encoding.UTF8.GetString(file, 0, file.Length));
            string s =Encoding.UTF8.GetString(file, 0, file.Length);
            // user_id
            data[0] = (Regex.Match(s, "user=(\\d+)").Groups[1].Value);
            // content
            data[1] = Regex.Match(s, "text(.+)ranges").Groups[1].Value;
            data[2] = Regex.Match(s, "22id(.+)tags").Groups[1].Value;
            return data;

            
        }


        static string  GetPathImage_WhenPost(string time_id)
        {
            string urlImage = "";

            return urlImage;
        }


        static void SaveImage(string urlImage, string path_to_save)
        {

        }




        // so danh chuoi
       public  static string similmarString(string sourceString, string[] chosingStrings)
        {
            string bestSimilar="";
            double score = 0.0;

            foreach (string s in chosingStrings)
            {
                MatchsMaker match = new MatchsMaker(sourceString, s);
                if (match.Score > score)
                {
                    bestSimilar = s;
                    score = match.Score;
                }
            }

            return bestSimilar;
        }


        #endregion


    }


}
