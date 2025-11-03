namespace Bankapp.Interfaces
{
    /// <summary>
    /// Interface for Accountservice containging methods
    /// </summary>
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