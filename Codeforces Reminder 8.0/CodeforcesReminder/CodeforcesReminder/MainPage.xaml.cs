using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using CodeforcesReminder.Resources;
using Microsoft.Phone.Scheduler;
using CodeforcesReminder.Views;
using System.Windows.Media.Animation;
using Microsoft.Phone.Net.NetworkInformation;

namespace CodeforcesReminder
{
    public partial class MainPage : PhoneApplicationPage
    {
        // Constructor
        public MainPage()
        {
            InitializeComponent();

            // Sample code to localize the ApplicationBar
            //BuildLocalizedApplicationBar();
            Loaded += MainPage_Loaded;
            Load_1.AutoReverse = true;
            Load_2.AutoReverse = true;
            Load_3.AutoReverse = true;
        }

        async void MainPage_Loaded(object sender, RoutedEventArgs e)
        {
            if(NetworkInterface.GetIsNetworkAvailable()){
            List<Contest> l = await MyHelper.GetListContestAsync("http://codeforces.com/api/contest.list?gym=false&d=" + DateTime.Now);
            foreach (Contest c in l)
                listBox.Items.Add(new ContestView(c));
            }
            else
            {
                MessageBox.Show("You need connect to the Internet!");
                Application.Current.Terminate();
            }
            loading.Visibility = System.Windows.Visibility.Collapsed;
            txtLoad.Visibility = System.Windows.Visibility.Collapsed;
        }

        // Sample code for building a localized ApplicationBar
        //private void BuildLocalizedApplicationBar()
        //{
        //    // Set the page's ApplicationBar to a new instance of ApplicationBar.
        //    ApplicationBar = new ApplicationBar();

        //    // Create a new button and set the text value to the localized string from AppResources.
        //    ApplicationBarIconButton appBarButton = new ApplicationBarIconButton(new Uri("/Assets/AppBar/appbar.add.rest.png", UriKind.Relative));
        //    appBarButton.Text = AppResources.AppBarButtonText;
        //    ApplicationBar.Buttons.Add(appBarButton);

        //    // Create a new menu item with the localized string from AppResources.
        //    ApplicationBarMenuItem appBarMenuItem = new ApplicationBarMenuItem(AppResources.AppBarMenuItemText);
        //    ApplicationBar.MenuItems.Add(appBarMenuItem);
        //}
    }
}