using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using DiagnoTrace.Helpers;
using System.Threading.Tasks;

namespace DiagnoTrace.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        UserDB userData = new UserDB();
        string pass = string.Empty;

        /// <summary>
        /// Initializes a new instance of the <see cref="LoginPage"/> class.
        /// </summary>
        public LoginPage()
        {
            InitializeComponent();

            NavigationPage.SetHasBackButton(this, false);
            userNameEntry.ReturnCommand = new Command(() => passwordEntry.Focus());
            firstPassword.ReturnCommand = new Command(() => secondPassword.Focus());
            var forgetpassword_tap = new TapGestureRecognizer();
            forgetpassword_tap.Tapped += ForgotPasswordClicked;
            forgetLabel.GestureRecognizers.Add(forgetpassword_tap);
        }

        /// <summary>
        /// Forgots the password clicked.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private async void ForgotPasswordClicked(object sender, EventArgs e)
        {
             popupLoadingView.IsVisible = true;
            //var textresult = userData.updateUserValidation(useridValidationEntry.Text);
            //if (textresult)
            //{

            //}
            await Task.Run(() => true);
        }

        /// <summary>
        /// Users the identifier check event.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private async void UserIdCheckEvent(object sender, EventArgs e)
        {
            if ((string.IsNullOrWhiteSpace(useridValidationEntry.Text)) || (string.IsNullOrWhiteSpace(useridValidationEntry.Text)))
            {
                await DisplayAlert("Alert", "Please enter email address", "OK");
            }
            else
            {
                pass = useridValidationEntry.Text;
                var textresult = userData.UpdateUserValidation(useridValidationEntry.Text);
                if (textresult)
                {
                    popupLoadingView.IsVisible = false;
                    passwordView.IsVisible = true;
                }
                else
                {
                    await DisplayAlert("User email address does not exist", "Please enter email address", "OK");
                }
            }
        }

        /// <summary>
        /// Passwords the clicked event.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private async void PasswordClickedEvent(object sender, EventArgs e)
        {
            if (!string.Equals(firstPassword.Text, secondPassword.Text))
            {
                warningLabel.Text = "Confirm Password";
                warningLabel.TextColor = Color.IndianRed;
                warningLabel.IsVisible = true;
            }
            else if ((string.IsNullOrWhiteSpace(firstPassword.Text)) || (string.IsNullOrWhiteSpace(secondPassword.Text)))
            {
                await DisplayAlert("Alert", " Enter Password", "OK");
            }
            else
            {
                try
                {
                    var return1 = userData.UpdateUser(pass, firstPassword.Text);
                    passwordView.IsVisible = false;
                    if (return1)
                    {
                        await DisplayAlert("Password Updated", "User Data updated", "OK");
                    }
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }

        /// <summary>
        /// Logins the validation button clicked.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private async void LoginValidationButtonClicked(object sender, EventArgs e)
        {
            //popupLoadingView.IsVisible = true;
            //activityIndicator.IsVisible = true;
            //activityIndicator.IsRunning = true;

            if (userNameEntry.Text != null && passwordEntry.Text != null)
            {
                var validData = userData.LoginValidate(userNameEntry.Text, passwordEntry.Text);
                if (validData)
                {
                    popupLoadingView.IsVisible = false;
                    //activityIndicator.IsVisible = false;
                    //activityIndicator.IsRunning = false;
                    //var name = this.GetType().Name;
                    await App.NavigatiPageAsync(frmLoginPage);
                }
                else
                {
                    popupLoadingView.IsVisible = false;
                    //activityIndicator.IsVisible = false;
                    //activityIndicator.IsRunning = false;
                    await DisplayAlert("Login Failed", "Username or Password Incorrect", "OK");
                }

            }
            else
            {
                popupLoadingView.IsVisible = false;
                await DisplayAlert("He He", "Enter User Name and Password Please", "OK");
            }
        }
    }
}