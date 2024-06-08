namespace ScamBet.Entities
{
    public class Transaction
    {
        public int Id { get; set; }
        public int AccountId { get; set; }
        public double Amount { get; set; }
        public DateTime Date { get; set; }
        public TransactionType Type { get; set; }

        public Account Account { get; set; }
    }

    public enum TransactionType
    {
        Deposit,
        Withdrawal
    }
}

