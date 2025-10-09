namespace Bankapp.Interfaces
{
    public interface IAccountservice
    {
        IBankaccount CreatAccount(string name, AccountType accountType, string currency, decimal initialBalance);
        List<IBankaccount> GetAccounts();
    }
}
