using System;
using System.Linq;
using Xamarin.Forms;
using Acr.UserDialogs;
using DiagnoTrace.Views;
using Xamarin.Essentials;
using DiagnoTrace.Models;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

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

        private string _confirmedCases;
        private string _deadCases;
        private string _recoveredCases;
        private string _lastUpdated;

        /// <summary>
        /// Gets the appearing command.
        /// </summary>
        /// <value>
        /// The appearing command.
        /// </value>
        public Command AppearingCommand => new Command(ExecuteFetchStatistics);

        /// <summary>
        /// Gets or sets the confirmed cases.
        /// </summary>
        /// <value>
        /// The confirmed cases.
        /// </value>
        public string ConfirmedCases
        {
            get => _confirmedCases;
            set
            {
                _confirmedCases = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets the dead cases.
        /// </summary>
        /// <value>
        /// The dead cases.
        /// </value>
        public string DeadCases
        {
            get => _deadCases;
            set
            {
                _deadCases = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets the recovered cases.
        /// </summary>
        /// <value>
        /// The recovered cases.
        /// </value>
        public string RecoveredCases
        {
            get => _recoveredCases;
            set
            {
                _recoveredCases = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets the last updated.
        /// </summary>
        /// <value>
        /// The last updated.
        /// </value>
        public string LastUpdated
        {
            get => _lastUpdated;
            set
            {
                _lastUpdated = value;
                OnPropertyChanged();
            }
        }

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

        /// <summary>
        /// Executes the fetch statistics.
        /// </summary>
        async void ExecuteFetchStatistics()
        {
            //UserDialogs.Instance.Alert("Starting...", "Check", "Ok");

            var connected = _permissionService.CheckNetworkConnectivity();
            if (connected != NetworkAccess.Internet)
            {
                UserDialogs.Instance.Alert("No internet connection available.", "Oops", "Ok");
                return;
            }

            try
            {
                using (UserDialogs.Instance.Loading("Fetching statistics..."))
                {
                    // call service
                    //var url = AppSettings.CoronaTrackerEndpoint;

                    var response = await DataStore.GetItemsAsync(true);

                    if (response.Any())
                    {
                        //var content = response.Content.ReadAsStringAsync().Result;

                        //var json = JsonConvert.DeserializeObject<dynamic>(content);

                        var VirusCases = new { Latest = new { Confirmed = 21343, Deaths = 407, Recovered = 10104 } };

                        // Map values to label
                        ConfirmedCases = VirusCases.Latest.Confirmed.ToString("N0");
                        DeadCases = VirusCases.Latest.Deaths.ToString("N0");
                        RecoveredCases = VirusCases.Latest.Recovered.ToString("N0");

                        // Add date
                        var currentDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                        LastUpdated = $"Last Updated: {currentDate}";
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                await UserDialogs.Instance.AlertAsync("Something went wrong, please try again later.", "Error", "Ok");
            }
        }
    }
}