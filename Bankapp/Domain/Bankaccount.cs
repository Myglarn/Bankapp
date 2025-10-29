using System.Text.Json.Serialization;

namespace Bankapp.Domain
{
    /// <summary>
    /// Bankaccount domain, hanterar transaktioner och sparar egenskaper till bankkontot.
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

        //Constructor
        public Bankaccount(string name, AccountType accountType, CurrencyType currency, decimal initialBalance)
        {
            Name = name;
            Currency = currency;
            Balance = initialBalance;
            AccountType = accountType;
            LastUpdated = DateTime.Now;

        }

        [JsonConstructor]
        public Bankaccount(Guid id, string name, AccountType accountType, CurrencyType currency, decimal balance, List<Transaction>? transactions)
        {
            Id = id;
            Name = name;
            Currency = currency;
            Balance = balance;
            AccountType = accountType;
            LastUpdated = DateTime.Now;
            Transactions = transactions ?? new List<Transaction>();

        }
        /// <summary>
        /// Hantering av insättningar till bankkontots saldo
        /// </summary>
        /// <param name="amount">Specifierad summa</param>
        public void Deposit(decimal amount)
        {
            //felhantering if-sats
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
        /// Hantering av uttag från bankkontots saldo
        /// </summary>
        /// <param name="amount"></param>
        public void Withdraw(decimal amount)
        {

            //felhantering if-sats
            Balance -= amount;
            Transactions.Add(new Transaction
            {
                Amount = amount,
                FromAccount = Id,
                ToAccount = null,
                BalanceAfterTransaction = Balance,
                TransactionType = TransactionType.Withdraw,
                DateTimeNow = DateTime.Now
            });
        }
        /// <summary>
        /// Sköter överföringar mellan olika konton
        /// </summary>
        /// <param name="to">Vilket konto som ska motta överföringen</param>
        /// <param name="amount">Summan som ska skickas</param>
        public void Transfer(Bankaccount to, decimal amount)
        {
            //Felhantering!!!
            //Från vilket konto
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

            //Till vilket konto
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