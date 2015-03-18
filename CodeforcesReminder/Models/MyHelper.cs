using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace CodeforcesReminder
{
    class MyHelper
    {
        private static string pattern = @"\d\d:\d\d:\d\d";
        private static DateTime now;
        //Một lớp dùng để lấy Html về
        public static async Task<string> GetStringAsync(string url)
        {
            Stream stream = await new HttpClient().GetStreamAsync(url);
               DataContractJsonSerializer jsonSerializer = new DataContractJsonSerializer(typeof(RootContest));
               RootContest objResponse = jsonSerializer.ReadObject(stream) as RootContest;
              if(objResponse!=null && objResponse.result.Count > 0)
              {
                  foreach (Contest item in objResponse.result)
                  {
                      Debug.WriteLine(item.name);
                  }
              }
              else
              {
                  Debug.WriteLine("Khong duoc!");
              }
            //now = DateTime.Now;
            //Debug.WriteLine(html);
            return "";
        }
      

        public static List<Contest> F(string html)
        {
         
                return null;
        }
        public static List<DateTime> GetTimeFromString(string input)
        {
            List<DateTime> list = new List<DateTime>();
            int n = Regex.Matches(input, "class='countdown'").Count;
            MatchCollection results = Regex.Matches(input, pattern);
            //HtmlElementCollection
            // XDocument doc = XDocument.Parse() 
         
            foreach (Match item in results)
            {
                //Khoảng thời gian
                TimeSpan sp = TimeSpan.Parse(item.Value);

                Debug.WriteLine(sp);
                if (list.Count < n)
                {
                    DateTime t = now.Add(sp);
                    list.Add(t);
                    Debug.WriteLine("=> " + t);
                }
            }

     

            return list;
        }
    }
}
