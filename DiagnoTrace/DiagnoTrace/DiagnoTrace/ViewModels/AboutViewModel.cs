using Xamarin.Forms;
using Xamarin.Essentials;
using System.Windows.Input;

namespace DiagnoTrace.ViewModels
{
    public class AboutViewModel : BaseViewModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AboutViewModel"/> class.
        /// </summary>
        public AboutViewModel()
        {
            Title = "About";
            OpenWebCommand = new Command(async () => await Browser.OpenAsync("https://xamarin.com"));
        }

        public ICommand OpenWebCommand { get; }
    }
}