using BankingSystem.Domain.Entities;
using System.Text;

namespace BankingSystem.Domain.Interfaces
{
    public interface ITransactionProcessor
    {
        decimal CalculateOpeningBalance(Account account, DateTime month);
        void AppendTransactionDetails(Account account, DateTime month, StringBuilder statementBuilder, ref decimal runningBalance);
    }
}