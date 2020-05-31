namespace DiagnoTrace.Interfaces
{
    public interface IToastMessage
    {
        /// <summary>
        /// Shows the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        void Show(string message);
    }
}
