using System;
using System.Linq;
using DiagnoTrace.Models;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace DiagnoTrace.Services
{
    public class MockDataStore : IDataStore<Item>
    {
        readonly List<Item> items;
        readonly List<Question> questions;

        /// <summary>
        /// Initializes a new instance of the <see cref="MockDataStore"/> class.
        /// </summary>
        public MockDataStore()
        {
            items = new List<Item>()
            {
                new Item { Id = Guid.NewGuid().ToString(), Text = "First item", Description="This is an item description." },
                new Item { Id = Guid.NewGuid().ToString(), Text = "Second item", Description="This is an item description." },
                new Item { Id = Guid.NewGuid().ToString(), Text = "Third item", Description="This is an item description." },
                new Item { Id = Guid.NewGuid().ToString(), Text = "Fourth item", Description="This is an item description." },
                new Item { Id = Guid.NewGuid().ToString(), Text = "Fifth item", Description="This is an item description." },
                new Item { Id = Guid.NewGuid().ToString(), Text = "Sixth item", Description="This is an item description." }
            };

            //TO DO : create enum for gender
            questions = new List<Question>
            {
                new Question { QuestionId = 1, QuestionText = "Please indicate your gender", QuestionAnswer = "Male", DeviceId = "dafds"}
            };
        }

        /// <summary>
        /// Adds the item asynchronous.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <returns></returns>
        public async Task<bool> AddItemAsync(Item item)
        {
            items.Add(item);

            return await Task.FromResult(true);
        }

        /// <summary>
        /// Updates the item asynchronous.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <returns></returns>
        public async Task<bool> UpdateItemAsync(Item item)
        {
            var oldItem = items.Where((Item arg) => arg.Id == item.Id).FirstOrDefault();
            items.Remove(oldItem);
            items.Add(item);

            return await Task.FromResult(true);
        }

        /// <summary>
        /// Deletes the item asynchronous.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public async Task<bool> DeleteItemAsync(string id)
        {
            var oldItem = items.Where((Item arg) => arg.Id == id).FirstOrDefault();
            items.Remove(oldItem);

            return await Task.FromResult(true);
        }

        /// <summary>
        /// Gets the item asynchronous.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public async Task<Item> GetItemAsync(string id)
        {
            return await Task.FromResult(items.FirstOrDefault(s => s.Id == id));
        }

        /// <summary>
        /// Gets the items asynchronous.
        /// </summary>
        /// <param name="forceRefresh">if set to <c>true</c> [force refresh].</param>
        /// <returns></returns>
        public async Task<IEnumerable<Item>> GetItemsAsync(bool forceRefresh = false)
        {
            return await Task.FromResult(items);
        }

        /// <summary>
        /// Gets the settings asynchronous.
        /// </summary>
        /// <param name="phoneId">The phone identifier.</param>
        /// <returns></returns>
        public async Task<Setting> GetSettingsAsync(string phoneId)
        {
            return await Task.FromResult(new Setting { SettingId = 1, DeviceId = "huaweiY5", CanNotify = true });
        }

        /// <summary>
        /// Saves the settings asynchronous.
        /// </summary>
        /// <param name="phoneId">The phone identifier.</param>
        /// <param name="canNotify">if set to <c>true</c> [can notify].</param>
        /// <returns></returns>
        public async Task<bool> SaveSettingsAsync(string phoneId, bool canNotify)
        {
            return await Task.FromResult(true);
        }

        /// <summary>
        /// Gets the questions asynchronous.
        /// </summary>
        /// <param name="deviceId">The device identifier.</param>
        /// <param name="forceRefresh">if set to <c>true</c> [force refresh].</param>
        /// <returns></returns>
        public async Task<IEnumerable<Question>> GetQuestionsAsync(string deviceId, bool forceRefresh = false)
        {
            return await Task.FromResult(questions);
        }
    }
}