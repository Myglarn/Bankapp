namespace Bankapp.Interfaces
{
    public interface IAccountservice
    {
        Task<Bankaccount> CreatAccount(string name, AccountType accountType, string currency, decimal initialBalance);
        Task<List<Bankaccount>> GetAccounts();
    }
}
