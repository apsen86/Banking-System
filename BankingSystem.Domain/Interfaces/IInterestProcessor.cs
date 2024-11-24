using BankingSystem.Domain.Entities;
using System.Text;

namespace BankingSystem.Domain.Interfaces
{
    public interface IInterestProcessor
    {
        List<(DateTime Start, DateTime End, decimal Balance, InterestRule Rule)> CalculateInterestPeriods(
            IEnumerable<BankTransaction> transactions, IEnumerable<InterestRule> rules, DateTime month);

        void AppendInterestDetails(StringBuilder statementBuilder, IEnumerable<(DateTime Start, DateTime End, decimal Balance, InterestRule Rule)> interestPeriods);
    }
}
