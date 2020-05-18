using DiagnoTrace.Models;
using DiagnoTrace.Services;
using DiagnoTrace.ViewModels;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

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

            conn = DependencyService.Get<ISQLiteDb>().GetConnection();
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