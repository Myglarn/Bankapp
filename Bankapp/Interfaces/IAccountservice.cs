namespace Bankapp.Interfaces
{
    public interface IAccountservice
    {
        Task<Bankaccount> CreatAccount(string name, AccountType accountType, CurrencyType currency, decimal initialBalance);
        Task<List<Bankaccount>> GetAccounts();
        void Transfer(Guid fromAccountId, Guid toAccountId, decimal amount);
        void Deposit(Guid toAccountId, decimal amount);
        void Withdraw(Guid fromAccountId, decimal amount);
        Task DeleteAccount(IBankaccount account);
    }
}
