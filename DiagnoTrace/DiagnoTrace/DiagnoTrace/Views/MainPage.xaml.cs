using System;
using Xamarin.Forms;
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
        public List<Models.MenuItem> menuList { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="MainPage"/> class.
        /// </summary>
        public MainPage()
        {
            InitializeComponent();

            MasterBehavior = MasterBehavior.Popover;

            NavigationPage.SetHasBackButton(this, false);

            MenuPages.Add((int)MenuItemType.Home, (NavigationPage)Detail);

            menuList = new List<Models.MenuItem>();

            // Adding menu items to menuList and you can define title ,page and icon
            menuList.Add(new Models.MenuItem() { Title = "Home", IconSource = "home.png", TargetType = typeof(ItemsPage) });
            menuList.Add(new Models.MenuItem() { Title = "Setting", IconSource = "setting.png", TargetType = typeof(SettingsPage) });
            menuList.Add(new Models.MenuItem() { Title = "Hotspots", IconSource = "tracking.png", TargetType = typeof(HotspotsPage) });
            menuList.Add(new Models.MenuItem() { Title = "Questions", IconSource = "transaction.png", TargetType = typeof(QuestionsPage) });
            menuList.Add(new Models.MenuItem() { Title = "About", IconSource = "help.png", TargetType = typeof(AboutPage) });
            
            // Setting our list to be ItemSource for ListView in MainPage.xaml
            navigationDrawerList.ItemsSource = menuList;

            // Initial navigation, this can be used for our home page
            Detail = new NavigationPage((Page)Activator.CreateInstance(typeof(ItemsPage)));
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

        /// <summary>
        /// Called when [menu item selected].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="SelectedItemChangedEventArgs"/> instance containing the event data.</param>
        private void OnMenuItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var item = (Models.MenuItem)e.SelectedItem;
            Type page = item.TargetType;

            Detail = new NavigationPage((Page)Activator.CreateInstance(page));
            IsPresented = false;
        }
    }
}