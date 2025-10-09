namespace Bankapp.Interfaces
{
    public interface IAccountservice
    {
        IBankaccount CreatAccount(string name, string currency, decimal initialBalance);
        List<IBankaccount> GetAccounts();
    }
}
