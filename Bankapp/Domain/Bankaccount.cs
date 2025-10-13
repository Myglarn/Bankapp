using Bankapp.Interfaces;
using System.Text.Json.Serialization;

namespace Bankapp.Domain
{
    public class Bankaccount : IBankaccount
    {
        public Guid Id { get; private set; } = Guid.NewGuid();
       
        public string Name { get; private set; }

        public string Currency { get; private set; }

        public decimal Balance { get; private set; }
        public AccountType AccountType { get; private set; }

        public DateTime LastUpdated { get; private set; }

        public Bankaccount(string name, AccountType accountType, string currency, decimal initialBalance )
        {
            Name = name;
            Currency = currency;
            Balance = initialBalance;
            AccountType = accountType;
            LastUpdated = DateTime.Now;
        }

        [JsonConstructor]
        public Bankaccount(Guid id, string name, AccountType accountType, string currency, decimal balance) 
        {
            Id = id;
            Name = name;
            Currency = currency;
            Balance = balance;
            AccountType = accountType;
            LastUpdated = DateTime.Now;

        }
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
