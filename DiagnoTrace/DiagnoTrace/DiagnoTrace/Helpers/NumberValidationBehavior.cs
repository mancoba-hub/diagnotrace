using Xamarin.Forms;

namespace DiagnoTrace.Helpers
{
    public class NumberValidationBehavior : Behavior<Entry>
    {
        /// <summary>
        /// Called when [attached to].
        /// </summary>
        /// <param name="bindable">The bindable.</param>
        protected override void OnAttachedTo(Entry bindable)
        {
            bindable.TextChanged += Bindable_TextChanged;
            base.OnAttachedTo(bindable);
        }

        /// <summary>
        /// Handles the TextChanged event of the Bindable control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="TextChangedEventArgs"/> instance containing the event data.</param>
        private void Bindable_TextChanged(object sender, TextChangedEventArgs e)
        {
            int result;
            bool IsValid = int.TryParse(e.NewTextValue, out result);
            ((Entry)sender).TextColor = IsValid ? Color.Default : Color.Red;
        }

        /// <summary>
        /// Called when [detaching from].
        /// </summary>
        /// <param name="bindable">The bindable.</param>
        protected override void OnDetachingFrom(Entry bindable)
        {
            bindable.TextChanged -= Bindable_TextChanged;
            base.OnDetachingFrom(bindable);
        }
    }
}
