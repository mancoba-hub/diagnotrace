using System;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

using Xamarin.Forms;

using DiagnoTrace.Models;
using DiagnoTrace.Views;
using Xamarin.Essentials;

namespace DiagnoTrace.ViewModels
{
    public class ItemsViewModel : BaseViewModel
    {
        public ObservableCollection<Item> Items { get; set; }

        public Command LoadItemsCommand { get; set; }

        public double Longitude { get; set; }

        public double Latitude { get; set; }

        public double? Accuracy { get; set; }

        public double? Altitude { get; set; }

        public string Location { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ItemsViewModel"/> class.
        /// </summary>
        public ItemsViewModel()
        {
            Title = "Home";
            Items = new ObservableCollection<Item>();
            GetCurrentLocation();

            MessagingCenter.Subscribe<NewItemPage, Item>(this, "AddItem", async (obj, item) =>
            {
                var newItem = item as Item;
                Items.Add(newItem);
                await DataStore.AddItemAsync(newItem);
            });
        }

        /// <summary>
        /// Executes the load items command.
        /// </summary>
        async Task ExecuteLoadItemsCommand()
        {
            IsBusy = true;

            try
            {
                Items.Clear();
                var items = await DataStore.GetItemsAsync(true);
                foreach (var item in items)
                {
                    Items.Add(item);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }

        /// <summary>
        /// Gets the current location.
        /// </summary>
        async void GetCurrentLocation()
        {
            IsBusy = true;

            try
            {
                var request = new GeolocationRequest(GeolocationAccuracy.Medium);
                var response = await Geolocation.GetLocationAsync(request);
                if (response != null)
                {
                    Longitude = response.Longitude;
                    Latitude = response.Latitude;
                    Accuracy = response.Accuracy;
                    Altitude = response.Altitude;

                    Location = $"Your current GEO Location : Lat ({Latitude}) - Long ({Longitude})";
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
            
        }
    }
}