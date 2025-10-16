namespace Bankapp.Domain
{
    public class Transaction : IBankaccount
    {
        public Guid Id { get; }

        public AccountType AccountType => throw new NotImplementedException();

        public string Name => throw new NotImplementedException();

        public string Currency => throw new NotImplementedException();

        public decimal Balance => throw new NotImplementedException();

        public DateTime LastUpdated => throw new NotImplementedException();

        public void Deposit(decimal amount)
        {
            throw new NotImplementedException();
        }

        public void Withdraw(decimal amount)
        {
            throw new NotImplementedException();
        }
    }
}
