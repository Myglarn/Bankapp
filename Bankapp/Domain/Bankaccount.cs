using System.Text.Json.Serialization;
namespace Bankapp.Domain
{
    /// <summary>    
    /// Bank account class, contains methods for transactions and interest rate 
    /// and contructors for localstorage   
    /// </summary>
    public class Bankaccount : IBankaccount
    {
        public Guid Id { get; private set; } = Guid.NewGuid();
        public string Name { get; private set; }
        public CurrencyType Currency { get; private set; }
        public decimal Balance { get; private set; }
        public AccountType AccountType { get; private set; }
        public DateTime LastUpdated { get; private set; }
        public List<Transaction> Transactions { get; private set; } = new();
        public const decimal InterestRate = 0.02m;

        // Constructor
        public Bankaccount(string name, AccountType accountType, CurrencyType currency, decimal initialBalance)
        {
            Name = name;
            Currency = currency;
            Balance = initialBalance;
            AccountType = accountType;
            LastUpdated = DateTime.Now;
        }

        [JsonConstructor]
        public Bankaccount(Guid id, string name, AccountType accountType, CurrencyType currency, DateTime lastUpdated, decimal balance, List<Transaction>? transactions)
        {
            Id = id;
            Name = name;
            Currency = currency;
            Balance = balance;
            AccountType = accountType;
            LastUpdated = lastUpdated;
            Transactions = transactions ?? new List<Transaction>();
        }

        /// <summary>
        /// Handles deposits to accounts
        /// </summary>        
        public void Deposit(decimal amount)
        {
            if (amount <= 0)
            {
                Console.WriteLine("Argument Exception in Bankaccount: Amount field needs to be greater than 0");
                throw new ArgumentException("Amount must be greater than 0");
            }
            
            Balance += amount;
            Transactions.Add(new Transaction
            {
                Amount = amount,
                ToAccount = Id,
                FromAccount = null,
                BalanceAfterTransaction = Balance,
                TransactionType = TransactionType.Deposit,
                DateTimeNow = DateTime.Now
            });
        }

        /// <summary>
        /// Handles withdraws from accounts
        /// </summary>        
        public void Withdraw(decimal amount, ExpenseCategory category)
        {
            if (amount <= 0)
            {
                Console.WriteLine("Argument Exception in Bankaccount: Amount needs to be greater than 0");
                throw new ArgumentException("Amount must be greater than 0");
            }            
            else if (amount > Balance)
            {
                Console.WriteLine("Argument Exception in Bankaccount: Insufficient funds in the selected account");
                throw new ArgumentException("Insufficient funds");
            }
            Balance -= amount;
            Transactions.Add(new Transaction
            {
                Amount = amount,
                FromAccount = Id,
                ToAccount = null,
                BalanceAfterTransaction = Balance,
                TransactionType = TransactionType.Withdraw,
                DateTimeNow = DateTime.Now,
                Category = category
            }); 
        }

        /// <summary>
        /// Manages transfers between accounts
        /// </summary>
        /// <param name="to">Recieving account</param>
        /// <param name="amount">amount to send</param>
        public void Transfer(Bankaccount to, decimal amount)
        {
            // Error messages
            if (amount <= 0)
            {
                Console.WriteLine("Argument Exception in Bankaccount: Amount needs to be greater than 0");
                throw new ArgumentException("Amount must be greater than 0");
            }
            else if (amount > Balance)
            {
                Console.WriteLine("Argument Exception in Bankaccount: Insufficient funds in the selected account");
                throw new ArgumentException("Insufficient funds");
            }            

            // From account
            Balance -= amount;
            DateTime dateTimeSender = DateTime.Now;
            Transactions.Add(new Transaction
            {
                Amount = amount,
                TransactionType = TransactionType.TransferOut,
                FromAccount = Id,
                ToAccount = to.Id,
                DateTimeNow = DateTime.Now,
                BalanceAfterTransaction = Balance
            });

            // To account
            to.Balance += amount;
            DateTime dateTimeRec = DateTime.Now;
            to.Transactions.Add(new Transaction
            {
                Amount = amount,
                TransactionType = TransactionType.TransferIn,
                FromAccount = Id,
                ToAccount = to.Id,
                DateTimeNow = DateTime.Now,
                BalanceAfterTransaction = to.Balance
            });
        }

        /// <summary>
        /// Handles interest rate
        /// </summary>
        /// <returns>interest rate if the account is of type "Savings"</returns>
        public decimal ApplyInterest()
        {
            if (AccountType != AccountType.Savings)
            {
                return 0m;
            }
            return Balance * InterestRate;
        }
    }
}