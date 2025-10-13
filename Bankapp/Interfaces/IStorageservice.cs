namespace Bankapp.Interfaces
{
    public interface IStorageservice
    {
        //spara
        Task SetItemAsync<T>(string key, T value);
        //Hämta
        Task<T> GetItemAsync<T>(string key);
    }
}
