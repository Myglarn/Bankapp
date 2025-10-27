
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

        public async Task<Bankaccount> CreatAccount(string name, AccountType accountType, CurrencyType currency, decimal initialBalance)
        {
            await IsInitialized();
            var account = new Bankaccount(name, accountType, currency, initialBalance);
            _accounts.Add(account);
            await SaveAsync();
            return account;
        }

        public async Task<List<Bankaccount>> GetAccounts()
        {
            await IsInitialized();
            return _accounts.Cast<Bankaccount>().ToList();
        }

        public async Task DeleteAccount(IBankaccount account)
        {
            await IsInitialized();

            var accountToRemove = _accounts.FirstOrDefault(a => a.Id == account.Id);
            if (accountToRemove != null)
            {
                _accounts.Remove(accountToRemove);
                await SaveAsync();
            }
        }
        public void Transfer(Guid fromAccountId, Guid toAccountId, decimal amount)
        {
            var fromStorage = _storageService.GetItemAsync<List<Bankaccount>>(StorageKey);
            var fromAccount = _accounts.FirstOrDefault(x => x.Id == fromAccountId);
            var toAccount = _accounts.FirstOrDefault(y => y.Id == toAccountId);
            if (fromAccount == null)
            {
                throw new ArgumentException("From account not found");
            }
            if (toAccount == null)
            {
                throw new ArgumentException("To account not found");
            }
            fromAccount.Transfer(toAccount, amount);
            _storageService.SetItemAsync(StorageKey, _accounts);
        }
        public void Withdraw(Guid fromAccountId, decimal amount)
        {
            var fromStorage = _storageService.GetItemAsync<List<Bankaccount>>(StorageKey);
            var fromAccount = _accounts.FirstOrDefault(x => x.Id == fromAccountId);
            if (fromAccount == null)
            {
                throw new ArgumentException("Account not found");
            }
            fromAccount.Withdraw(amount);
            _storageService.SetItemAsync(StorageKey, _accounts);
        }
        public void Deposit(Guid toAccountId, decimal amount)
        {
            var fromStorage = _storageService.GetItemAsync<List<Bankaccount>>(StorageKey);
            var toAccount = _accounts.FirstOrDefault(x => x.Id == toAccountId);
            if (toAccount == null)
            {
                throw new ArgumentException("Account not found");
            }
            toAccount.Deposit(amount);
            _storageService.SetItemAsync(StorageKey, _accounts);
        }

    }
}