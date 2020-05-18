using System;
using System.Text;
using System.Net.Http;
using Newtonsoft.Json;
using Xamarin.Essentials;
using DiagnoTrace.Models;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace DiagnoTrace.Services
{
    public class AzureDataStore : IDataStore<Item>
    {
        HttpClient client;
        IEnumerable<Item> items;
        IEnumerable<Question> questions;

        /// <summary>
        /// Initializes a new instance of the <see cref="AzureDataStore"/> class.
        /// </summary>
        public AzureDataStore()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri($"{App.AzureBackendUrl}/");

            items = new List<Item>();
            questions = new List<Question>();
        }

        bool IsConnected => Connectivity.NetworkAccess == NetworkAccess.Internet;

        /// <summary>
        /// Gets the items asynchronous.
        /// </summary>
        /// <param name="forceRefresh">if set to <c>true</c> [force refresh].</param>
        /// <returns></returns>
        public async Task<IEnumerable<Item>> GetItemsAsync(bool forceRefresh = false)
        {
            if (forceRefresh && IsConnected)
            {
                var json = await client.GetStringAsync($"api/item");
                items = await Task.Run(() => JsonConvert.DeserializeObject<IEnumerable<Item>>(json));
            }

            return items;
        }

        /// <summary>
        /// Gets the item asynchronous.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public async Task<Item> GetItemAsync(string id)
        {
            if (id != null && IsConnected)
            {
                var json = await client.GetStringAsync($"api/item/{id}");
                return await Task.Run(() => JsonConvert.DeserializeObject<Item>(json));
            }

            return null;
        }

        /// <summary>
        /// Adds the item asynchronous.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <returns></returns>
        public async Task<bool> AddItemAsync(Item item)
        {
            if (item == null || !IsConnected)
                return false;

            var serializedItem = JsonConvert.SerializeObject(item);

            var response = await client.PostAsync($"api/item", new StringContent(serializedItem, Encoding.UTF8, "application/json"));

            return response.IsSuccessStatusCode;
        }

        /// <summary>
        /// Updates the item asynchronous.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <returns></returns>
        public async Task<bool> UpdateItemAsync(Item item)
        {
            if (item == null || item.Id == null || !IsConnected)
                return false;

            var serializedItem = JsonConvert.SerializeObject(item);
            var buffer = Encoding.UTF8.GetBytes(serializedItem);
            var byteContent = new ByteArrayContent(buffer);

            var response = await client.PutAsync(new Uri($"api/item/{item.Id}"), byteContent);

            return response.IsSuccessStatusCode;
        }

        /// <summary>
        /// Deletes the item asynchronous.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public async Task<bool> DeleteItemAsync(string id)
        {
            if (string.IsNullOrEmpty(id) && !IsConnected)
                return false;

            var response = await client.DeleteAsync($"api/item/{id}");

            return response.IsSuccessStatusCode;
        }

        /// <summary>
        /// Gets the settings asynchronous.
        /// </summary>
        /// <param name="phoneId">The phone identifier.</param>
        /// <returns></returns>
        public async Task<Setting> GetSettingsAsync(string phoneId)
        {
            if (!string.IsNullOrWhiteSpace(phoneId) && IsConnected)
            {
                var json = await client.GetStringAsync($"api/notify/{phoneId}");
                return await Task.Run(() => JsonConvert.DeserializeObject<Setting>(json));
            }

            return new Setting { SettingId = 2, DeviceId = "huaweiY5", CanNotify = true };
        }

        /// <summary>
        /// Saves the settings asynchronous.
        /// </summary>
        /// <param name="phoneId">The phone identifier.</param>
        /// <param name="canNotify">if set to <c>true</c> [can notify].</param>
        /// <returns></returns>
        public async Task<bool> SaveSettingsAsync(string phoneId, bool canNotify)
        {
            if (string.IsNullOrWhiteSpace(phoneId) || !IsConnected)
                return false;

            var serializedItem = JsonConvert.SerializeObject(new { phoneId, canNotify });
            var buffer = Encoding.UTF8.GetBytes(serializedItem);
            var byteContent = new ByteArrayContent(buffer);

            var response = await client.PutAsync(new Uri($"api/notify?phoneId={phoneId}&canNotify={canNotify}"), byteContent);

            return response.IsSuccessStatusCode;
        }

        /// <summary>
        /// Gets the questions asynchronous.
        /// </summary>
        /// <param name="deviceId">The device identifier.</param>
        /// <param name="forceRefresh">if set to <c>true</c> [force refresh].</param>
        /// <returns></returns>
        public async Task<IEnumerable<Question>> GetQuestionsAsync(string deviceId, bool forceRefresh = false)
        {
            if (forceRefresh && IsConnected)
            {
                var json = await client.GetStringAsync($"api/question/{deviceId}");
                questions = await Task.Run(() => JsonConvert.DeserializeObject<IEnumerable<Question>>(json));
            }

            return questions;
        }
    }
}
