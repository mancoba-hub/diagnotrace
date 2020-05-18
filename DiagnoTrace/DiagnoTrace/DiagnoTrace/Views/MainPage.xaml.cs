using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using DiagnoTrace.Models;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace DiagnoTrace.Views
{
    [DesignTimeVisible(false)]
    public partial class MainPage : MasterDetailPage
    {
        Dictionary<int, NavigationPage> MenuPages = new Dictionary<int, NavigationPage>();

        /// <summary>
        /// Initializes a new instance of the <see cref="MainPage"/> class.
        /// </summary>
        public MainPage()
        {
            InitializeComponent();

            MasterBehavior = MasterBehavior.Popover;

            MenuPages.Add((int)MenuItemType.Home, (NavigationPage)Detail);
        }

        /// <summary>
        /// Navigates from menu.
        /// </summary>
        /// <param name="id">The identifier.</param>
        public async Task NavigateFromMenu(int id)
        {
            if (!MenuPages.ContainsKey(id))
            {
                switch (id)
                {
                    case (int)MenuItemType.Home:
                        //display stats
                        MenuPages.Add(id, new NavigationPage(new ItemsPage()));
                        break;
                    case (int)MenuItemType.Hotspots:
                        //display known hotspots within certain radius
                        MenuPages.Add(id, new NavigationPage(new HotspotsPage()));
                        break;
                    case (int)MenuItemType.Questions:
                        //question to answer to add to stats
                        MenuPages.Add(id, new NavigationPage(new QuestionsPage()));
                        break;
                    case (int)MenuItemType.Settings:
                        //set whether you want to receive push notifications
                        MenuPages.Add(id, new NavigationPage(new SettingsPage()));
                        break;
                    case (int)MenuItemType.About:
                        
                        MenuPages.Add(id, new NavigationPage(new AboutPage()));
                        break;
                }
            }

            var newPage = MenuPages[id];

            if (newPage != null && Detail != newPage)
            {
                Detail = newPage;

                if (Device.RuntimePlatform == Device.Android)
                    await Task.Delay(100);

                IsPresented = false;
            }
        }
    }
}