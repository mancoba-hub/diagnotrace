using DiagnoTrace.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DiagnoTrace.Services
{
    public interface IDataStore<T>
    {
        Task<bool> AddItemAsync(T item);
        Task<bool> UpdateItemAsync(T item);
        Task<bool> DeleteItemAsync(string id);
        Task<T> GetItemAsync(string id);
        Task<IEnumerable<T>> GetItemsAsync(bool forceRefresh = false);
        Task<Setting> GetSettingsAsync(string phoneId);
        Task<bool> SaveSettingsAsync(string phoneId, bool canNotify);
        Task<IEnumerable<Question>> GetQuestionsAsync(string deviceId, bool forceRefresh = false);
    }
}
