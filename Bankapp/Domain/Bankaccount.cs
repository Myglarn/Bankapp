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
        private readonly List<Transaction> _transactions = new();
        public IReadOnlyList<Transaction> Transactions => _transactions.AsReadOnly();

        public Bankaccount(string name, AccountType accountType, string currency, decimal initialBalance)
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
            Balance += amount;
            _transactions.Add(new Transaction { Amount = amount });
        }

        public void Withdraw(decimal amount)
        {
            Balance -= amount;
            _transactions.Add(new Transaction { 
                Amount = amount           
            });
        }

        public void Transfer(Bankaccount to, decimal amount)
        {
            Balance -= amount;
            DateTime dateTimeSender = DateTime.Now;
            _transactions.Add(new Transaction { 
                Amount = amount,
                TransactionType = TransactionType.TransferOut,
                FromAccount = Id,
                ToAccount = to.Id                
            });

            to.Balance += amount;   
            DateTime dateTimeRec = DateTime.Now;
            to._transactions.Add(new Transaction
            {
                Amount = amount,
                TransactionType = TransactionType.TransferIn,
                FromAccount = Id
            });
        }
        
    }
}
