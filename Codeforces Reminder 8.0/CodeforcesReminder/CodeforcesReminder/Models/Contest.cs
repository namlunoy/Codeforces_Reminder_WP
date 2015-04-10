using Microsoft.Phone.Scheduler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeforcesReminder
{
    public class Contest
    {
        public int id { get; set; }
        public string name { get; set; }
        public string type { get; set; }
        public string phase { get; set; }
        public bool frozen { get; set; }
        public int durationSeconds { get; set; }
        public string description { get; set; }
        public int difficulty { get; set; }
        public string kind { get; set; }
        public string country { get; set; }
        public string city { get; set; }
        public string season { get; set; }
        public string preparedBy { get; set; }
        public string icpcRegion { get; set; }
        public int? startTimeSeconds { get; set; }
        public int? relativeTimeSeconds { get; set; }
        public string websiteUrl { get; set; }

        public DateTime StartTime { get; set; }
        public TimeSpan BeforStart { get; set; }
        public void FixTime()
        {
            if (startTimeSeconds != null)
            {
                //Chuyển unix time => time thườn
                StartTime = new DateTime(1970, 1, 1).AddSeconds((double)startTimeSeconds);
                //Chuyểnt time từ utc về timezone hiện tại
                StartTime = StartTime.Add(TimeZoneInfo.Local.BaseUtcOffset);
                BeforStart = StartTime - new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);
            }
        }
        public string NameReminder
        {
            get
            {
                char[] c = name.ToLower().ToCharArray();
                string r_name = "";
                for (int i = 0; i < c.Length; i++)
                    if (c[i] >= 'a' && c[i] <= 'z')
                        r_name += c[i];
                return r_name;
            }
        }
        public bool IsAddedToReminder()
        {
            return ScheduledActionService.Find(NameReminder) != null;
        }

        public void AddToReminder()
        {
            Reminder r = new Reminder(NameReminder);
            r.Title = name;
            r.Content = StartTime.ToString();
            r.BeginTime = StartTime.AddMinutes(-10);
            //   r.BeginTime = DateTime.Now.AddSeconds(10);
            r.ExpirationTime = StartTime.AddMinutes(10);
            r.NavigationUri = new Uri("MainPage.xaml", UriKind.Relative);
            ScheduledActionService.Add(r);
        }
    }

    public class ListContest
    {
        public string status { get; set; }
        public List<Contest> result { get; set; }
    }
}
