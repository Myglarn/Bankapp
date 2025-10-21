namespace Bankapp.Interfaces
{
    /// <summary>
    /// Interface containging the BankAccount methods
    /// </summary>
    public interface IBankaccount
    {
        Guid Id { get; }
        AccountType AccountType { get; }
        string Name { get; }
        string Currency {  get; }
        decimal Balance {  get; }
        DateTime LastUpdated { get; }

        void Withdraw(decimal amount);
        void Deposit(decimal amount);
        void Transfer(Bankaccount to, decimal amount);
    }
}
