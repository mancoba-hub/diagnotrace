using Xamarin.Forms;
using DiagnoTrace.Models;
using System.ComponentModel;
using System.Collections.Generic;

namespace DiagnoTrace.Views
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MenuPage : ContentPage
    {
        MainPage RootPage { get => Xamarin.Forms.Application.Current.MainPage as MainPage; }
        public Xamarin.Forms.ListView ListView { get { return listView; } }
        List<Models.MenuItem> menuItems;
        Xamarin.Forms.ListView listView;

        /// <summary>
        /// Initializes a new instance of the <see cref="MenuPage" /> class.
        /// </summary>
        public MenuPage()
        {
            InitializeComponent();

            menuItems = new List<Models.MenuItem>
            {
                new Models.MenuItem { Id = MenuItemType.Home, Title="Home", IconSource = "detail.png", TargetType = typeof(ItemsPage) },
                new Models.MenuItem { Id = MenuItemType.Hotspots, Title="Hotspots", IconSource = "tracking.png", TargetType = typeof(HotspotsPage) },
                new Models.MenuItem { Id = MenuItemType.Questions, Title="Questions", IconSource = "transaction.png", TargetType = typeof(QuestionsPage) },
                new Models.MenuItem { Id = MenuItemType.Settings, Title="Settings", IconSource = "setting.png", TargetType = typeof(SettingsPage) },
                new Models.MenuItem { Id = MenuItemType.About, Title="About", IconSource = "help.png", TargetType = typeof(AboutPage) }
            };

            ListViewMenu = new Xamarin.Forms.ListView
            {
                ItemsSource = menuItems,
                ItemTemplate = new DataTemplate(() =>
                {
                    var grid = new Grid { Padding = new Thickness(5, 10) };
                    grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(30) });
                    grid.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Star });

                    var image = new Image();
                    image.SetBinding(Image.SourceProperty, "IconSource");
                    var label = new Label { VerticalOptions = LayoutOptions.FillAndExpand };
                    label.SetBinding(Label.TextProperty, "Title");

                    grid.Children.Add(image);
                    grid.Children.Add(label, 1, 0);

                    return new ViewCell { View = grid };
                }),
                SeparatorVisibility = SeparatorVisibility.None
            };

            ListViewMenu.SelectedItem = menuItems[0];
            ListViewMenu.ItemSelected += async (sender, e) =>
            {
                if (e.SelectedItem == null)
                    return;

                var id = (int)((Models.MenuItem)e.SelectedItem).Id;
                await RootPage.NavigateFromMenu(id);
            };

            IconImageSource = "hamburger.png";
            Title = "Home";
            Padding = new Thickness(0, 40, 0, 0);
            Content = new StackLayout
            {
                Children = { ListViewMenu }
            };

        }
    }
}

//ListViewMenu.ItemsSource = menuItems;

//ListViewMenu.SelectedItem = menuItems[0];
//ListViewMenu.ItemSelected += async (sender, e) =>
//{
//    if (e.SelectedItem == null)
//        return;

//    var id = (int)((HomeMenuItem)e.SelectedItem).Id;
//    await RootPage.NavigateFromMenu(id);
//};