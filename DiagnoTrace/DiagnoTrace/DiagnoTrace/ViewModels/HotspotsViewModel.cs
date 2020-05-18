using System;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

using Xamarin.Forms;

using DiagnoTrace.Models;

namespace DiagnoTrace.ViewModels
{
    public class HotspotsViewModel : BaseViewModel
    {
        public ObservableCollection<Item> Items { get; set; }
        public Command LoadItemsCommand { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="HotspotsViewModel"/> class.
        /// </summary>
        public HotspotsViewModel()
        {
            Title = "Hotspots";
            Items = new ObservableCollection<Item>();
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());
        }

        /// <summary>
        /// Executes the load items command.
        /// </summary>
        /// <returns></returns>
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
    }
}
