namespace Bankapp.Domain
{
    /// <summary>
    /// Specifies the type of transaction.
    /// </summary>    
    public enum TransactionType
    {
        Deposit,
        Withdraw,
        TransferIn,
        TransferOut,
        Transfer
    }
}