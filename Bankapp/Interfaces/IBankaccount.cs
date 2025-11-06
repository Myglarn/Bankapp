namespace Bankapp.Interfaces
{
    /// <summary>
    /// Interface for the bank account containing methods and properties
    /// </summary>
    public interface IBankaccount
    {
        Guid Id { get; }
        AccountType AccountType { get; }
        string Name { get; }
        CurrencyType Currency { get; }
        decimal Balance { get; }
        DateTime LastUpdated { get; }
        List<Transaction> Transactions { get; }
        void Withdraw(decimal amount, ExpenseCategory category);
        void Deposit(decimal amount);
        void Transfer(Bankaccount to, decimal amount);
    }
}