using Xamarin.Forms;
using DiagnoTrace.Models;
using System.ComponentModel;
using DiagnoTrace.ViewModels;

namespace DiagnoTrace.Views
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class ItemDetailPage : ContentPage
    {
        ItemDetailViewModel viewModel;

        /// <summary>
        /// Initializes a new instance of the <see cref="ItemDetailPage"/> class.
        /// </summary>
        /// <param name="viewModel">The view model.</param>
        public ItemDetailPage(ItemDetailViewModel viewModel)
        {
            InitializeComponent();

            BindingContext = this.viewModel = viewModel;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ItemDetailPage"/> class.
        /// </summary>
        public ItemDetailPage()
        {
            InitializeComponent();

            var item = new Item
            {
                Text = "Item 1",
                Description = "This is an item description."
            };

            viewModel = new ItemDetailViewModel(item);
            BindingContext = viewModel;
        }
    }
}