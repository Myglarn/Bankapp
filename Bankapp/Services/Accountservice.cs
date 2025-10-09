
namespace Bankapp.Services
{
    public class Accountservice : IAccountservice
    {
        private readonly List<IBankaccount> _accounts = new();
        public IBankaccount CreatAccount(string name, AccountType accountType, string currency, decimal initialBalance)
        {
            var account = new Bankaccount(name, accountType, currency, initialBalance);
            _accounts.Add(account);
            return account;
        }

        public List<IBankaccount> GetAccounts()
        {
            return _accounts;
        }
    }
}
