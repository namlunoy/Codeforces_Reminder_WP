using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml.Linq;
using Windows.Devices.Geolocation;

namespace CodeforcesReminder
{
    delegate void DownLoadXong(ListContest s);
    class MyHelper
    {
        private static string pattern = @"\d\d:\d\d:\d\d";
        private static DateTime now;
        public static event DownLoadXong downxong;
        //Một lớp dùng để lấy Html về
        public static async Task<List<Contest>> GetListContestAsync(string url)
        {
            WebRequest request = WebRequest.Create(url);
            using (WebResponse response = await request.GetResponseAsync())
            {
                using (Stream stream = response.GetResponseStream())
                {
                    DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(ListContest));
                    ListContest l = serializer.ReadObject(stream) as ListContest;
                    List<Contest> list = l.result.Where(c => c.relativeTimeSeconds < 0).ToList();
                    list.Sort(new XepTheoThoiGian());
                    return list;
                }
            }
        }

        public class XepTheoThoiGian : IComparer<Contest>
        {
            public int Compare(Contest x, Contest y)
            {
                int a = (int)x.startTimeSeconds;
                int b = (int)y.startTimeSeconds;
                return a.CompareTo(b);
            }
        }


        public static async Task<Geoposition> GetLocation()
        {
            Geolocator geolocator = new Geolocator();
            geolocator.DesiredAccuracyInMeters = 50;

            try
            {
                Geoposition geoposition = await geolocator.GetGeopositionAsync(
                     maximumAge: TimeSpan.FromMinutes(5),
                     timeout: TimeSpan.FromSeconds(10)
                    );
                return geoposition;
                //With this 2 lines of code, the app is able to write on a Text Label the Latitude and the Longitude, given by {{Icode|geoposition}}
            }
            //If an error is catch 2 are the main causes: the first is that you forgot to include ID_CAP_LOCATION in your app manifest. 
            //The second is that the user doesn't turned on the Location Services
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
