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
        /// Gets the asynchronous connection.
        /// </summary>
        /// <returns></returns>
        public SQLiteAsyncConnection GetAsyncConnection()
        {
            var documentsPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            var path = Path.Combine(documentsPath, "diagnoTrace.db3");
            //var platform = new SQLite.Net.Platform.XamarinAndroid.SQLitePlatformAndroid();            
            return new SQLiteAsyncConnection(path);
        }

        /// <summary>
        /// Gets the connection.
        /// </summary>
        /// <returns></returns>
        public SQLiteConnection GetConnection()
        {
            var documentsPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            var path = Path.Combine(documentsPath, "diagnoTrace.db3");
            return new SQLiteConnection(path);
        }
    }
}