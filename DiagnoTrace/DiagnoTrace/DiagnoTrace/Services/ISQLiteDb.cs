using SQLite;

namespace DiagnoTrace.Services
{
    public interface ISQLiteDb
    {
        /// <summary>
        /// Gets the connection.
        /// </summary>
        /// <returns></returns>
        SQLiteConnection GetConnection();

        /// <summary>
        /// Gets the asynchronous connection.
        /// </summary>
        /// <returns></returns>
        SQLiteAsyncConnection GetAsyncConnection();
    }
}
