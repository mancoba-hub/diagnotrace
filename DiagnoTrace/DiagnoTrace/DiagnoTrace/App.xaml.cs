using Xamarin.Forms;
using DiagnoTrace.Views;
using Xamarin.Essentials;
using DiagnoTrace.Services;
using System.Threading.Tasks;

namespace DiagnoTrace
{
    public partial class App : Application
    {
        //TODO: Replace with *.azurewebsites.net url after deploying backend to Azure
        //To debug on Android emulators run the web backend against .NET Core not IIS
        //If using other emulators besides stock Google images you may need to adjust the IP address
        public static string AzureBackendUrl = DeviceInfo.Platform == DevicePlatform.Android ? "http://10.0.2.2:5000" : "http://localhost:5000";
        public static bool UseMockDataStore = true;

        /// <summary>
        /// Initializes a new instance of the <see cref="App"/> class.
        /// </summary>
        public App()
        {
            InitializeComponent();

            if (UseMockDataStore)
                DependencyService.Register<MockDataStore>();
            else
                DependencyService.Register<AzureDataStore>();

            MainPage = new LoginPage();
        }

        /// <summary>
        /// Application developers override this method to perform actions when the application starts.
        /// </summary>
        /// <remarks>
        /// To be added.
        /// </remarks>
        protected override void OnStart()
        {
        }

        /// <summary>
        /// Application developers override this method to perform actions when the application enters the sleeping state.
        /// </summary>
        /// <remarks>
        /// To be added.
        /// </remarks>
        protected override void OnSleep()
        {
        }

        /// <summary>
        /// Application developers override this method to perform actions when the application resumes from a sleeping state.
        /// </summary>
        /// <remarks>
        /// To be added.
        /// </remarks>
        protected override void OnResume()
        {
        }

        /// <summary>
        /// Navigatis the page.
        /// </summary>
        /// <param name="name">The name.</param>
        public static async void NavigatiPage(Page name)
        {
            Application.Current.MainPage = new NavigationPage(new MainPage());
            await name.Navigation.PushAsync(new MainPage());
        }

        /// <summary>
        /// Navigatis the page asynchronous.
        /// </summary>
        /// <param name="name">The name.</param>
        internal static async Task NavigatiPageAsync(Page name)
        {
            Application.Current.MainPage = new NavigationPage(new MainPage());
            await name.Navigation.PushAsync(new MainPage());
        }
    }
}
