using Android.App;
using Android.Widget;
using DiagnoTrace.Droid;
using DiagnoTrace.Interfaces;

[assembly: Xamarin.Forms.Dependency(typeof(ToastMessage))]
namespace DiagnoTrace.Droid
{
    /// <summary>
    /// Displays a toast message
    /// </summary>
    public class ToastMessage : IToastMessage
    {
        /// <summary>
        /// Shows the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        public void Show(string message)
        {
            Toast.MakeText(Application.Context, message, ToastLength.Long).Show();
        }
    }
}