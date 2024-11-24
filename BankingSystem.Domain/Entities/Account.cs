using BankingSystem.Domain.Constants;

namespace BankingSystem.Domain.Entities
{
    public class Account
    {
        public string AccountName { get; private set; }
        public decimal Balance { get; private set; }
        private readonly List<BankTransaction> _transactions = new();

        public IReadOnlyCollection<BankTransaction> Transactions => _transactions.AsReadOnly();

        public Account(string accountName)
        {
            if (string.IsNullOrWhiteSpace(accountName))
                throw new ArgumentException(ErrorMessages.EmptyAccountName);

            AccountName = accountName;
            Balance = 0;
        }

        public void AddTransaction(BankTransaction transaction)
        {
            _transactions.Add(transaction);
            Balance += transaction.Type == TransactionType.D ? transaction.Amount : -transaction.Amount;
        }

        public int GetNextTransactionNumber(DateTime date)
        {
            return _transactions.Count(t => t.Date.Date == date.Date) + 1;
        }
    }
}
