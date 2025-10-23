using Bankapp.Interfaces;
using System.Text.Json.Serialization;

namespace Bankapp.Domain
{
    public class Bankaccount : IBankaccount
    {
        public Guid Id { get; private set; } = Guid.NewGuid();
       
        public string Name { get; private set; }

        public CurrencyType Currency { get; private set; }

        public decimal Balance { get; private set; }
        public AccountType AccountType { get; private set; }

        public DateTime LastUpdated { get; private set; }
        private readonly List<Transaction> _transactions = new();
        public IReadOnlyList<Transaction> Transactions => _transactions.AsReadOnly();

        public Bankaccount(string name, AccountType accountType, CurrencyType currency, decimal initialBalance)
        {
            Name = name;
            Currency = currency;
            Balance = initialBalance;
            AccountType = accountType;
            LastUpdated = DateTime.Now;
            //Ny transaction direkt
            _transactions.Add(new Transaction(Id, initialBalance, TransactionType.Deposit, Balance));
        }

        [JsonConstructor]
        public Bankaccount(Guid id, string name, AccountType accountType, CurrencyType currency, decimal balance) 
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
            if(amount <= 0)
            {
                throw new ArgumentException("Amount must be greater than zero", nameof(amount));
            }

            Balance += amount;
            LastUpdated = DateTime.Now;
            _transactions.Add(new Transaction(Id, amount, TransactionType.Deposit, Balance, null, $"Deposit of {amount:F2}"));
        }

        public void Withdraw(decimal amount)
        {
            if (amount <= 0)
            {
                throw new ArgumentException("Amount must be greater than zero", nameof(amount));
            }
            if (amount > Balance)
            {
                throw new ArgumentException("Insufficient funds", nameof(amount));
            }
            Balance -= amount;
            LastUpdated = DateTime.Now;
            _transactions.Add(new Transaction(Id, amount, TransactionType.Withdraw, Balance, null, $"Withdrawal of {amount:F2}"));
        }

        public void Transfer(Bankaccount to, decimal amount)
        {
            if (to == null)
            {
                throw new ArgumentNullException(nameof(to));
            }
            if (to.Id == this.Id)
            {
                throw new InvalidOperationException("Cannot transfer to the same acount.");
            }
            if (amount <= 0)
            {
                throw new ArgumentException("Amount must be greater than zero.", nameof(amount));
            }
            if (amount > Balance)
            {
                throw new InvalidOperationException("Insufficient funds.");
            }
            Balance -= amount;
            to.Balance += amount;
            

            _transactions.Add(new Transaction(Id, amount, TransactionType.Transfer, Balance, to.Name, $"Transfer to {to.Name}"));
            to._transactions.Add(new Transaction(to.Id, amount, TransactionType.Transfer, to.Balance, Name, $"Transfer from {Name}"));
        }
        
    }
}
