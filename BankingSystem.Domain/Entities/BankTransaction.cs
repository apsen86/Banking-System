using BankingSystem.Domain.Constants;

namespace BankingSystem.Domain.Entities
{
    public class BankTransaction
    {
        public string Id { get; private set; }
        public decimal Amount { get; private set; }
        public TransactionType Type { get; private set; }
        public DateTime Date { get; private set; }

        private BankTransaction(string id, decimal amount, TransactionType type, DateTime date)
        {
            Id = id;
            Amount = amount;
            Type = type;
            Date = date;
        }

        public static BankTransaction Create(string type, decimal amount, DateTime date, int transactionNumber)
        {
            if (string.IsNullOrWhiteSpace(type) ||
                !(string.Equals(type, "D", StringComparison.OrdinalIgnoreCase) || string.Equals(type, "W", StringComparison.OrdinalIgnoreCase)))
                throw new ArgumentException(ErrorMessages.InvalidTransactionType);

            if (amount <= 0 || Math.Round(amount, 2) != amount)
                throw new ArgumentException(ErrorMessages.InvalidTransactionAmount);

            var transactionType = string.Equals(type, "D", StringComparison.OrdinalIgnoreCase) ? TransactionType.D : TransactionType.W;
            var id = $"{date:yyyyMMdd}-{transactionNumber:D2}";

            return new BankTransaction(id, amount, transactionType, date);
        }
    }
}
