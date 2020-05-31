using DiagnoTrace.Models;

namespace DiagnoTrace.ViewModels
{
    public class ItemDetailViewModel : BaseViewModel
    {
        public Item Item { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ItemDetailViewModel"/> class.
        /// </summary>
        /// <param name="item">The item.</param>
        public ItemDetailViewModel(Item item = null)
        {
            Title = item?.Text;
            Item = item;
        }
    }
}
