using System;
using Xamarin.Forms;
using System.Diagnostics;
using DiagnoTrace.Models;
using System.Threading.Tasks;

namespace DiagnoTrace.ViewModels
{
    public class SettingsViewModel : BaseViewModel
    {
        public Setting Setting { get; set; }
        public bool CanNotify { get; set; }
        
        public Command LoadItemCommand { get; set; }

        public Command SaveSettingsCommand { get; set; }

        string deviceId = string.Empty;

        /// <summary>
        /// Gets the device identifier.
        /// </summary>
        /// <value>
        /// The device identifier.
        /// </value>
        public string DeviceId
        {
            get
            {
                if (!string.IsNullOrWhiteSpace(deviceId))
                    return deviceId;

                //deviceId = Android.OS.Build.Serial;
                //if (string.IsNullOrWhiteSpace(deviceId) || deviceId == Build.Unknown || deviceId == "0")
                //{
                //    try
                //    {
                //        var context = Android.App.Application.Context;
                //        deviceId = Secure.GetString(context.ContentResolver, Secure.AndroidId);
                //    }
                //    catch (Exception ex)
                //    {
                //        Android.Util.Log.Warn("DeviceInfo", "Unable to get id: " + ex.ToString());
                //    }
                //}

                return deviceId;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SettingsViewModel"/> class.
        /// </summary>
        public SettingsViewModel()
        {
            Title = "Settings";
            Setting = new Setting { SettingId = 1, DeviceId = "HuaweiY5", CanNotify = false };
            LoadItemCommand = new Command(async () => await ExecuteLoadItemCommand());
            SaveSettingsCommand = new Command(async () => await ExecuteSaveCommand());
        }

        /// <summary>
        /// Executes the load items command.
        /// </summary>
        async Task ExecuteLoadItemCommand()
        {
            IsBusy = true;
            try
            {
                Setting = await DataStore.GetSettingsAsync(DeviceId);                
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
        /// Executes the save command.
        /// </summary>
        async Task ExecuteSaveCommand()
        {
            IsBusy = true;
            try
            {
                CanNotify = await DataStore.SaveSettingsAsync(DeviceId, CanNotify);
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
