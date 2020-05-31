using SQLite;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using DiagnoTrace.Models;
using System.Diagnostics;
using DiagnoTrace.Services;
using DiagnoTrace.ViewModels;
using DiagnoTrace.Interfaces;
using System.Threading.Tasks;

namespace DiagnoTrace.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SettingsPage : ContentPage
    {
        SettingsViewModel viewModel;
        public SQLiteAsyncConnection conn;

        public bool IsToggled { get; set; }
        public string Information { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="SettingsPage"/> class.
        /// </summary>
        public SettingsPage()
        {
            InitializeComponent();
            
            BindingContext = viewModel = new SettingsViewModel();

            Information = "You will NOT get notifications";

            conn = DependencyService.Get<ISQLiteDb>().GetAsyncConnection();
            conn.CreateTableAsync<Setting>();

            IsToggled = viewModel.Setting.CanNotify;
            conn.InsertAsync(viewModel.Setting);
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

            viewModel.IsBusy = true;
        }

        /// <summary>
        /// Saves the settings clicked.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        async void SaveSettingsClicked(object sender, EventArgs e)
        {
            try
            {
                viewModel.Setting.CanNotify = IsToggled;
                var response = await conn.UpdateAsync(viewModel.Setting);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                DependencyService.Get<IToastMessage>().Show("Settings updated successfully!");
            }
        }

        /// <summary>
        /// Notifies the toggled.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="ToggledEventArgs"/> instance containing the event data.</param>
        private void NotifyToggled(object sender, ToggledEventArgs e)
        {
            try
            {
                if (e.Value == true)
                    Information = "You will get notifications as you enter hotspot zones";
                else
                    Information = "You will NOT get notifications";

                viewModel.Setting.CanNotify = e.Value;
                var response = Task.Run(() => conn.UpdateAsync(viewModel.Setting));
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                DependencyService.Get<IToastMessage>().Show("Settings updated successfully!");
            }
        }
    }
}