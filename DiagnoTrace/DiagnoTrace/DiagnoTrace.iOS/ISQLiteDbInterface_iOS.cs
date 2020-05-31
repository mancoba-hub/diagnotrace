using System;
using SQLite;
using System.IO;
using DiagnoTrace.Services;

namespace DiagnoTrace.iOS
{
    public class ISQLiteDbInterface_iOS : ISQLiteDb
    {
        /// <summary>
        /// Gets the asynchronous connection.
        /// </summary>
        /// <returns></returns>
        public SQLiteAsyncConnection GetAsyncConnection()
        {
            var fileName = "diagnoTrace.db3";
            var documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            var libraryPath = Path.Combine(documentsPath, "..", "Library");
            var path = Path.Combine(libraryPath, fileName);
            //var platform = new Platform.XamarinIOS.SQLitePlatformIOS();
            //var connection = new SQLiteConnection(path);
            return new SQLiteAsyncConnection(path);
        }

        /// <summary>
        /// Gets the connection.
        /// </summary>
        /// <returns></returns>
        public SQLiteConnection GetConnection()
        {
            var fileName = "diagnoTrace.db3";
            var documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            var libraryPath = Path.Combine(documentsPath, "..", "Library");
            var path = Path.Combine(libraryPath, fileName);
            //var platform = new Platform.XamarinIOS.SQLitePlatformIOS();
            //var connection = new SQLiteConnection(path);
            return new SQLiteConnection(path);
        }
    }
}