using SQLite;
using System.IO;
using Xamarin.Forms;
using DiagnoTrace.Services;
using DiagnoTrace.Droid.Persistence;

[assembly: Dependency(typeof(SQLiteDb))]
namespace DiagnoTrace.Droid.Persistence
{
    public class SQLiteDb : ISQLiteDb
    {
        /// <summary>
        /// Gets the connection.
        /// </summary>
        /// <returns></returns>
        public SQLiteAsyncConnection GetConnection()
        {
            var documentsPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.ApplicationData);
            var path = Path.Combine(documentsPath, "diagnoTrace.db3");
            return new SQLiteAsyncConnection(path);
        }
    }
}