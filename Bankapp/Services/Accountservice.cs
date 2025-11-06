
namespace Bankapp.Services
{
    /// <summary>
    /// Handles logic for bank accounts, including creating accounts,
    /// retrieving stored accounts, and performing deposits, withdraws, and transfers.
    /// Communicates with a storage service to persist account data and is used by the UI layer
    /// to manage account-related operations.
    /// </summary>
    public class Accountservice : IAccountservice
    {
        private const string StorageKey = "bankapp.accounts";
        private readonly List<Bankaccount> _accounts = new();
        private readonly IStorageservice _storageService;
        public Accountservice(IStorageservice storageService) => _storageService = storageService;
        private bool isLoaded;

        /// <summary>
        /// Initializes the accounts from local storage
        /// </summary>        
        private async Task IsInitialized()
        {
            if (isLoaded)
            {
                return;
            }
            var fromStorage = await _storageService.GetItemAsync<List<Bankaccount>>(StorageKey);
            Console.WriteLine("Account Service: Accounts initialized");
            _accounts.Clear();
            if (fromStorage is { Count: > 0 })
            {
                _accounts.AddRange(fromStorage);
                isLoaded = true;
            }
        }
        
        // Saves accounts to storage               
        private Task SaveAsync()
        {
            Console.WriteLine("Account Service: Account list saved to storage");
            return _storageService.SetItemAsync(StorageKey, _accounts);
        } 

        /// <summary>
        /// Method for account creation
        /// </summary>        
        public async Task<Bankaccount> CreatAccount(string name, AccountType accountType, CurrencyType currency, decimal initialBalance)
        {
            await IsInitialized();
            var account = new Bankaccount(name, accountType, currency, initialBalance);
            if (name == null)
            {
                Console.WriteLine("Argument exception in Account Service: Account name needed");
                throw new ArgumentException("You need a name for your account");
            }
            _accounts.Add(account);
            Console.WriteLine("Account Service: Account created");
            await SaveAsync();
            return account;
        }

        /// <summary>
        /// Method for printing account list
        /// </summary>        
        public async Task<List<Bankaccount>> GetAccounts()
        {
            await IsInitialized();
            Console.WriteLine("Account Service: Accounts list retrieved from storage");
            return _accounts.Cast<Bankaccount>().ToList();
        }

        /// <summary>
        /// Method for permanently removing accounts
        /// </summary>
        public async Task DeleteAccount(IBankaccount account)
        {
            await IsInitialized();

            var accountToRemove = _accounts.FirstOrDefault(a => a.Id == account.Id);
            if (accountToRemove != null)
            {
                _accounts.Remove(accountToRemove);
                Console.WriteLine("Account Service: Account removed");
                await SaveAsync();
            }
        }

        /// <summary>
        /// Method for transfers between accounts
        /// </summary>        
        public void Transfer(Guid fromAccountId, Guid toAccountId, decimal amount)
        {
            var fromStorage = _storageService.GetItemAsync<List<Bankaccount>>(StorageKey);
            var fromAccount = _accounts.FirstOrDefault(x => x.Id == fromAccountId);
            var toAccount = _accounts.FirstOrDefault(y => y.Id == toAccountId);
            if (fromAccount == null)
            {
                Console.WriteLine("Argument exception in Account Service: From account not found");
                throw new ArgumentException("From account not found");
            }
            if (toAccount == null)
            {
                Console.WriteLine("Argument exception in Account Service: To account not found");
                throw new ArgumentException("To account not found");
            }
            if (fromAccount == toAccount)
            {
                Console.WriteLine("Argument exception in Account Service: Cannot transfer funds to the same account");
                throw new ArgumentException("Cannot transfer funds to the same account.");
            }
            fromAccount.Transfer(toAccount, amount);
            Console.WriteLine("Account Service: Transfer succeeded");
            _storageService.SetItemAsync(StorageKey, _accounts);
        }

        /// <summary>
        /// Method for withdrawing funds 
        /// </summary>        
        public void Withdraw(Guid fromAccountId, decimal amount, ExpenseCategory category)
        {
            var fromStorage = _storageService.GetItemAsync<List<Bankaccount>>(StorageKey);
            var fromAccount = _accounts.FirstOrDefault(x => x.Id == fromAccountId);
            if (fromAccount == null)
            {
                Console.WriteLine("Argument exception in Account Service: fromAccount field empty");
                throw new ArgumentException("You must select an account");
            }
            fromAccount.Withdraw(amount, category);
            Console.WriteLine("Account Service: Withdraw succeeded");
            _storageService.SetItemAsync(StorageKey, _accounts);
        }

        /// <summary>
        /// Mehtod for deposeting funds
        /// </summary>        
        public void Deposit(Guid toAccountId, decimal amount)
        {
            var fromStorage = _storageService.GetItemAsync<List<Bankaccount>>(StorageKey);
            var toAccount = _accounts.FirstOrDefault(x => x.Id == toAccountId);
            if (toAccount == null)
            {
                Console.WriteLine("Argument exception in Account Service: toAccount field empty");
                throw new ArgumentException("You must select an account");
            }
            toAccount.Deposit(amount);
            Console.WriteLine("Account Service: Deposit succeeded");
            _storageService.SetItemAsync(StorageKey, _accounts);
        }
    }
}