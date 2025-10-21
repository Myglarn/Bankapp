namespace Bankapp.Interfaces
{
    public interface IAccountservice
    {
        Task<Bankaccount> CreatAccount(string name, AccountType accountType, string currency, decimal initialBalance);
        Task<List<Bankaccount>> GetAccounts();
        void Transfer(Guid fromAccountId, Guid toAccountId, decimal amount);
        Task DeleteAccount(IBankaccount account);
    }
}
