using BankingSystem.Domain.Constants;
using BankingSystem.Domain.Entities;
using BankingSystem.Domain.Interfaces;
using System.Text;

namespace BankingSystem.Domain.Services
{
    public class StatementGenerator : IStatementGenerator
    {
        private readonly ITransactionProcessor _transactionProcessor;
        private readonly IInterestProcessor _interestProcessor;

        public StatementGenerator(ITransactionProcessor transactionProcessor, IInterestProcessor interestProcessor)
        {
            _transactionProcessor = transactionProcessor ?? throw new ArgumentNullException(nameof(transactionProcessor));
            _interestProcessor = interestProcessor ?? throw new ArgumentNullException(nameof(interestProcessor));
        }

        public string Generate(Account account, DateTime month, IEnumerable<InterestRule> rules)
        {
            if (account == null) throw new ArgumentNullException(nameof(account));
            if (rules == null) throw new ArgumentNullException(nameof(rules));

            var statementBuilder = InitializeStatement(account);

            var openingBalance = _transactionProcessor.CalculateOpeningBalance(account, month);
            _transactionProcessor.AppendTransactionDetails(account, month, statementBuilder, ref openingBalance);

            var interestPeriods = _interestProcessor.CalculateInterestPeriods(account.Transactions, rules, month);
            _interestProcessor.AppendInterestDetails(statementBuilder, interestPeriods);

            FinalizeStatement(statementBuilder);

            return statementBuilder.ToString();
        }

        private static StringBuilder InitializeStatement(Account account)
        {
            var sb = new StringBuilder();
            sb.AppendLine($"Account: {account.AccountName}");
            sb.AppendLine(StatementConstants.Header);
            sb.AppendLine(StatementConstants.Separator);
            return sb;
        }

        private static void FinalizeStatement(StringBuilder statementBuilder)
        {
            statementBuilder.AppendLine(StatementConstants.Separator);
        }
    }
}
