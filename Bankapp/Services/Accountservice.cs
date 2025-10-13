
namespace Bankapp.Services
{
    public class Accountservice : IAccountservice
    {
        private const string StorageKey = "bankapp.accounts";
        private readonly List<Bankaccount> _accounts = new();
        private readonly IStorageservice _storageService;
        public Accountservice(IStorageservice storageService) => _storageService = storageService;

        private bool isLoaded;
        private async Task IsInitialized()
        {
            if (isLoaded)
            {
                return;
            }
            var fromStorage = await _storageService.GetItemAsync<List<Bankaccount>>(StorageKey);
            _accounts.Clear();
            if (fromStorage is { Count: > 0 })
            {
                _accounts.AddRange(fromStorage);
                isLoaded = true;
            }
        }

        private Task SaveAsync() => _storageService.SetItemAsync(StorageKey, _accounts);

        public async Task<Bankaccount> CreatAccount(string name, AccountType accountType, string currency, decimal initialBalance)
        {
            await IsInitialized();
            var account = new Bankaccount(name, accountType, currency, initialBalance);
            _accounts.Add(account);
            await SaveAsync();
            return account;
        }

        public async Task <List<Bankaccount>> GetAccounts()
        {
            await IsInitialized();
            return _accounts.Cast<Bankaccount>().ToList();
        }
        
    }
}
