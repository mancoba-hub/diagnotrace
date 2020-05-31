using System;
using Xamarin.Forms;
using System.Diagnostics;
using DiagnoTrace.Models;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace DiagnoTrace.ViewModels
{
    public class QuestionsViewModel : BaseViewModel
    {
        public ObservableCollection<Question> Questions { get; set; }
        public Command LoadQuestionsCommand { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="QuestionsViewModel"/> class.
        /// </summary>
        public QuestionsViewModel()
        {
            Title = "Questions";
            Questions = new ObservableCollection<Question>();
            LoadQuestionsCommand = new Command(async () => await ExecuteLoadItemsCommand());
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
                Questions.Clear();
                var questionList = await DataStore.GetQuestionsAsync(string.Empty);
                foreach (var question in questionList)
                {
                    Questions.Add(question);
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
