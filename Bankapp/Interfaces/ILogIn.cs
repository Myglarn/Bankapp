namespace Bankapp.Interfaces
{
    /// <summary>
    /// Interface with methods for logging in and out
    /// </summary>
    public interface ILogIn
    {
        Task<bool> LoginAsync(string username, string pin);
        Task LogOutAsync();
        Task<bool> IsLogedInAsync();
        Task<Login?> GetLoginAsync();
        event Action? OnLoginStateChanged;
    }
}
