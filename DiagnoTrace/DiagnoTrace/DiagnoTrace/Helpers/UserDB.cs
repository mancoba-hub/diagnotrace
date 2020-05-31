using SQLite;
using System.Linq;
using Xamarin.Forms;
using DiagnoTrace.Models;
using DiagnoTrace.Services;
using System.Collections.Generic;

namespace DiagnoTrace.Helpers
{
    public class UserDB
    {
        private SQLiteAsyncConnection _SQLiteAsyncConnection;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserDB"/> class.
        /// </summary>
        public UserDB()
        {
            var instance = DependencyService.Get<ISQLiteDb>();
            if (instance != null)
            {
                _SQLiteAsyncConnection = instance.GetAsyncConnection();
                _SQLiteAsyncConnection.CreateTableAsync<User>();
            }            
        }

        /// <summary>
        /// Gets the users.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<User> GetUsers()
        {
            var userListResponse = _SQLiteAsyncConnection.Table<User>().ToListAsync();
            return (from u in userListResponse.Result select u).ToList();
        }

        /// <summary>
        /// Gets the specific user.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public User GetSpecificUser(int id)
        {
            return _SQLiteAsyncConnection.Table<User>().FirstOrDefaultAsync(t => t.Id == id).Result;
        }

        /// <summary>
        /// Deletes the user.
        /// </summary>
        /// <param name="id">The identifier.</param>
        public void DeleteUser(int id)
        {
            _SQLiteAsyncConnection.DeleteAsync<User>(id);
        }

        /// <summary>
        /// Adds the user.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns></returns>
        public string AddUser(User user)
        {            
            var data = _SQLiteAsyncConnection.Table<User>();
            var d1 = data.Where(x => x.Name == user.Name && x.UserName == user.UserName).FirstOrDefaultAsync();
            if (d1 == null)
            {
                _SQLiteAsyncConnection.InsertAsync(user);
                return "Sucessfully Added";
            }
            else
                return "Already id Exist";
        }

        /// <summary>
        /// Updates the user validation.
        /// </summary>
        /// <param name="userid">The userid.</param>
        /// <returns></returns>
        public bool UpdateUserValidation(string userid)
        {
            var data = _SQLiteAsyncConnection.Table<User>();
            var d1 = (from values in data where values.UserName == userid select values).FirstOrDefaultAsync();
            if (d1 != null)
                return true;
            else
                return false;
        }

        /// <summary>
        /// Updates the user.
        /// </summary>
        /// <param name="username">The username.</param>
        /// <param name="pwd">The password.</param>
        /// <returns></returns>
        public bool UpdateUser(string username, string pwd)
        {
            var data = _SQLiteAsyncConnection.Table<User>();
            var d1 = (from values in data where values.UserName == username select values).FirstOrDefaultAsync().Result;
            if (d1 != null)
            {
                d1.Password = pwd;
                _SQLiteAsyncConnection.UpdateAsync(d1);
                return true;
            }
            else
                return false;
        }

        /// <summary>
        /// Logins the validate.
        /// </summary>
        /// <param name="userName1">The user name1.</param>
        /// <param name="pwd1">The PWD1.</param>
        /// <returns></returns>
        public bool LoginValidate(string userName1, string pwd1)
        {
            var data = _SQLiteAsyncConnection.Table<User>();
            var d1 = data.Where(x => x.UserName == userName1 && x.Password == pwd1).FirstOrDefaultAsync();
            if (d1 != null)
            {
                return true;
            }
            else
                return false;
        }
    }
}
