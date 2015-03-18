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
    }

    public class RootContest
    {
        public string status { get; set; }
        public List<Contest> result { get; set; }
    }
}
