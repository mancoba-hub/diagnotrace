using System;
using Xamarin.Forms;
using DiagnoTrace.Models;
using System.ComponentModel;
using DiagnoTrace.ViewModels;

namespace DiagnoTrace.Views
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class ItemsPage : ContentPage
    {
        ItemsViewModel viewModel;

        /// <summary>
        /// Initializes a new instance of the <see cref="ItemsPage"/> class.
        /// </summary>
        public ItemsPage()
        {
            InitializeComponent();

            BindingContext = viewModel = new ItemsViewModel();
            //NavigationPage.SetHasBackButton(this, false);
        }

        /// <summary>
        /// Called when [item selected].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="args">The <see cref="EventArgs"/> instance containing the event data.</param>
        async void OnItemSelected(object sender, EventArgs args)
        {
            var layout = (BindableObject)sender;
            var item = (Item)layout.BindingContext;
            await Navigation.PushAsync(new ItemDetailPage(new ItemDetailViewModel(item)));
        }

        /// <summary>
        /// Handles the Clicked event of the AddItem control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        async void AddItem_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new NavigationPage(new NewItemPage()));
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

            //lblLongitude.Text = viewModel.Longitude.ToString();
            //lblLatitude.Text = viewModel.Latitude.ToString();
            //if (viewModel.Items.Count == 0)
            //    viewModel.IsBusy = true;
        }
    }
}