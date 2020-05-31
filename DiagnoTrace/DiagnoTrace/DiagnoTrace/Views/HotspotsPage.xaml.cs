using SQLite;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using DiagnoTrace.Models;
using DiagnoTrace.Services;
using DiagnoTrace.ViewModels;

namespace DiagnoTrace.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HotspotsPage : ContentPage
    {
        HotspotsViewModel viewModel;
        public SQLiteAsyncConnection conn;

        /// <summary>
        /// Initializes a new instance of the <see cref="HotspotsPage"/> class.
        /// </summary>
        public HotspotsPage()
        {
            InitializeComponent();

            BindingContext = viewModel = new HotspotsViewModel();

            conn = DependencyService.Get<ISQLiteDb>().GetAsyncConnection();
            conn.CreateTableAsync<Hotspot>();
        }

        /// <summary>
        /// When overridden, allows application developers to customize behavior immediately prior to the <see cref="T:Xamarin.Forms.Page" /> becoming visible.
        /// </summary>
        /// <remarks>
        /// To be added.
        /// </remarks>
        protected override void OnAppearing()
        {
            base.OnAppearing();

            //after call to viewModel

            viewModel.IsBusy = true;
        }
    }
}