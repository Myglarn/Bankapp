using System.Diagnostics;
using System.Text.Json.Serialization;

namespace Bankapp.Domain
{

    public class Transaction
    {
        public Guid Id { get; } = Guid.NewGuid();
        public decimal Amount { get; set; }
        public DateTime DateTimeNow { get; set; }
        public TransactionType TransactionType { get; set; }
        public decimal BalanceAfterTransaction { get; set; }
        public Guid ToAccount { get; set; }
        public Guid FromAccount { get; set; }
        public string? RelatedAccountName { get; private set; }
        public string? Description { get; private set; }

        public Transaction(Guid accountId, decimal amount, TransactionType type, decimal balanceAfter, string? relatedAccountName, string? description)
        {
            //if sats för amount <=0
            Id = Guid.NewGuid();
            FromAccount = accountId;
            Amount = amount;
            TransactionType = type;
            BalanceAfterTransaction = balanceAfter;
            RelatedAccountName = relatedAccountName;
            Description = description;
            DateTimeNow = DateTime.Now;
        }
        
        [JsonConstructor]      
        
        public Transaction(Guid accountId, decimal amount, TransactionType type, decimal balanceAfter)
        {
            Id = Guid.NewGuid();
            FromAccount = accountId;
            Amount = amount;
            TransactionType = type;
            BalanceAfterTransaction = balanceAfter;
            RelatedAccountName = null;
            DateTimeNow = DateTime.Now;
            Description = $"{type} of {amount:C}";
        }
    }
}

    
      