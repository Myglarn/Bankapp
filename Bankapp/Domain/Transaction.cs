namespace Bankapp.Domain
{
    public class Transaction
    {
        public Guid Id { get; } = Guid.NewGuid();
        public decimal Amount { get; set; }
        public DateTime DateTimeNow { get; set; } = DateTime.Now;
        public TransactionType TransactionType { get; set; }
        public decimal BalanceAfterTransaction { get; set; }
        public Guid? ToAccount { get; set; }
        public Guid? FromAccount { get; set; }
    }
}
