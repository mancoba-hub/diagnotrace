using DiagnoTrace.Models;
using DiagnoTrace.Services;
using DiagnoTrace.ViewModels;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DiagnoTrace.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class QuestionsPage : ContentPage
    {
        QuestionsViewModel viewModel;
        public SQLiteAsyncConnection conn;

        /// <summary>
        /// Initializes a new instance of the <see cref="QuestionsPage"/> class.
        /// </summary>
        public QuestionsPage()
        {
            InitializeComponent();

            BindingContext = viewModel = new QuestionsViewModel();

            conn = DependencyService.Get<ISQLiteDb>().GetConnection();
            conn.CreateTableAsync<Question>();
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

            //after call to viewModel

            viewModel.IsBusy = true;
        }

        /// <summary>
        /// Called when [next clicked].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void OnNextClicked(object sender, EventArgs e)
        {
            //var contact = new Contact
            //{
            //    Name = "Jane Doe",
            //    Age = 30,
            //    Occupation = "Developer",
            //    Country = "USA"
            //};

            var secondQuestionPage = new SecondQuestionPage();
            //secondQuestionPage.BindingContext = new { Gender = lblGender.Text, Age = txtAge.Text };
            Navigation.PushAsync(secondQuestionPage);
        }

        private void OnGenderButtonCheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            //RadioButton radioButton = sender as RadioButton;
            //bool isChecked = radioButton.IsChecked;
            //string who = radioButton.Text;
            //if (((RadioButton)sender).Checked)
            //{ 
            //}
        }
    }
}