
namespace Bankapp.Services
{
    /// <summary>
    /// Service responsible for handling user login and logout operations.
    /// Stores login state in local storage service.
    /// </summary>
    public class LoginService : ILogIn
    {
        private readonly IStorageservice _storageservice;
        private string UserKey = "CurrentUser";
        private string LoggedInKey = "isLoggedIn";
        private readonly Login _defaultUser = new("User", "1234");
        
        /// <summary>
        /// Event triggered whenever the login state changes.
        /// Components subscribing to this are able to update automatically.
        /// </summary>
        public event Action? OnLoginStateChanged;

        /// <summary>
        /// Creates a new instance of the LoginService with an injected storage service.
        /// </summary>
        public LoginService(IStorageservice storageservice)
        {
            _storageservice = storageservice;
        }

        /// <summary>
        /// Retrieves the currently stored logged-in user, if one exists.
        /// </summary>
        public async Task<Login?> GetLoginAsync()
        {
            return await _storageservice.GetItemAsync<Login>(UserKey);
        }

        /// <summary>
        /// Checks whether a user is currently logged in.
        /// </summary>
        public async Task<bool> IsLogedInAsync()
        {
            return await _storageservice.GetItemAsync<bool>(LoggedInKey);
        }

        /// <summary>
        /// Attempts to log a user in using a username and PIN.
        /// If the credentials match the default user, the user is stored and logged in.
        /// </summary>
        public async Task<bool> LoginAsync(string username, string pin)
        {
            if (username == _defaultUser.Username && pin == _defaultUser.Pin)
            {
                await _storageservice.SetItemAsync(UserKey, _defaultUser);
                await _storageservice.SetItemAsync(LoggedInKey, true);
                OnLoginStateChanged?.Invoke();
                return true;
            }
            return false;            
        }

        /// <summary>
        /// Logs out the current user and clears stored login information.
        /// </summary>
        public async Task LogOutAsync()
        {
            await _storageservice.SetItemAsync<Login?>(UserKey, null);
            await _storageservice.SetItemAsync(LoggedInKey, false);
            OnLoginStateChanged?.Invoke();
        }
    }
}
