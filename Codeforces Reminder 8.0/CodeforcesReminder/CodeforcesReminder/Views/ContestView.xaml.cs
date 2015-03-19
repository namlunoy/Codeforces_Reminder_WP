using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using System.Windows.Threading;
using Microsoft.Phone.Scheduler;
using Microsoft.Phone.Tasks;

namespace CodeforcesReminder.Views
{
    public partial class ContestView : UserControl
    {
        private Contest contest;
        DispatcherTimer timer = new DispatcherTimer();
        public ContestView(Contest con)
        {
            contest = con;
            InitializeComponent();
            Loaded += ContestView_Loaded;
            timer.Interval = TimeSpan.FromSeconds(1);
          
        }

        void timer_Tick(object sender, EventArgs e)
        {
            contest.BeforStart -= TimeSpan.FromSeconds(1);
            txtTimeBeforStart.Text = contest.BeforStart.ToString();
        }

        void ContestView_Loaded(object sender, RoutedEventArgs e)
        {
            contest.FixTime();
            txtName.Text = contest.name;
            txtTime.Text = contest.StartTime.ToString();
            txtTimeZone.Text = TimeZoneInfo.Local.DisplayName;
            //Bộ đếm ngược
            int days =(int) contest.BeforStart.TotalDays;
            if(days < 1)
            {
                timer.Tick += timer_Tick;
                timer.Start();
            }
            else
            {
                if (days == 1)
                    txtTimeBeforStart.Text = days + " day before start!";
                else txtTimeBeforStart.Text = days + " days before start!";
            }

            //Bộ add
            if (contest.IsAddedToReminder())
            {
                bt_Reminder.IsHitTestVisible = false;
                bt_Reminder.Content = "Added";
            }
        }

        private void ClickReminder(object sender, RoutedEventArgs e)
        {
            if (contest.IsAddedToReminder())
            {
                MessageBox.Show("Added to Reminder before contest start 10 minutes!");
            }
            else
            {
                contest.AddToReminder();
                MessageBox.Show("Added to Reminder before contest start 10 minutes!");
                bt_Reminder.IsHitTestVisible = false;
                bt_Reminder.Content = "Added";
            }
        }

        private void ClickRegister(object sender, RoutedEventArgs e)
        {
            //http://codeforces.com/contests
            WebBrowserTask browser = new WebBrowserTask();
            browser.Uri = new Uri("http://codeforces.com/contests");
            browser.Show();
        }
    }
}
