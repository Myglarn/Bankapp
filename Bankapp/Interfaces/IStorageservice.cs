namespace Bankapp.Interfaces
{
    /// <summary>
    /// Interface for storage services
    /// </summary>
    public interface IStorageservice
    {        
        Task SetItemAsync<T>(string key, T value);        
        Task<T> GetItemAsync<T>(string key);
    }
}