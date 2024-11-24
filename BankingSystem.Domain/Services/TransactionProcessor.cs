using BankingSystem.Domain.Constants;
using BankingSystem.Domain.Entities;
using BankingSystem.Domain.Interfaces;
using System.Text;

namespace BankingSystem.Domain.Services
{
    public class TransactionProcessor : ITransactionProcessor
    {
        public decimal CalculateOpeningBalance(Account account, DateTime month)
        {
            return account.Transactions
                .Where(t => t.Date < new DateTime(month.Year, month.Month, 1, 0, 0, 0, DateTimeKind.Utc))
                .Sum(t => t.Type == TransactionType.D ? t.Amount : -t.Amount);
        }

        public void AppendTransactionDetails(Account account, DateTime month, StringBuilder statementBuilder, ref decimal runningBalance)
        {
            var transactions = account.Transactions
                .Where(t => t.Date.Month == month.Month && t.Date.Year == month.Year)
                .OrderBy(t => t.Date);

            foreach (var transaction in transactions)
            {
                runningBalance += transaction.Type == TransactionType.D ? transaction.Amount : -transaction.Amount;
                statementBuilder.AppendLine(string.Format(
                    StatementConstants.TransactionRowStatmentFormat,
                    transaction.Date.ToString("yyyyMMdd"),
                    transaction.Id,
                    transaction.Type,
                    transaction.Amount,
                    runningBalance
                ));
            }
        }
    }
}
