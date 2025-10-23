namespace Bankapp.Interfaces
{
    public interface IAccountservice
    {
        Task<Bankaccount> CreatAccount(string name, AccountType accountType, CurrencyType currency, decimal initialBalance);
        Task<List<Bankaccount>> GetAccounts();
        Task TransferAsync(Guid fromAccountId, Guid toAccountId, decimal amount);
        Task DepositAsync(Guid id, decimal amount);
        Task WithdrawAsync(Guid id, decimal amount);
        Task DeleteAccount(IBankaccount account);
    }
}
