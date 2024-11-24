using BankingSystem.Application.Constants;
using BankingSystem.Application.Interfaces;
using BankingSystem.Application.Utilities;
using BankingSystem.Domain.Interfaces;
using BankingSystem.Infrastructure.Repositories;
using System.Text;

namespace BankingSystem.Application.Services
{
    public class StatementService : IStatementService
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IInterestRuleRepository _interestRuleRepository;
        private readonly ITransactionProcessor _transactionProcessor;
        private readonly IInterestProcessor _interestProcessor;

        public StatementService(
            IAccountRepository accountRepository,
            IInterestRuleRepository interestRuleRepository,
            ITransactionProcessor transactionProcessor,
            IInterestProcessor interestProcessor)
        {
            _accountRepository = accountRepository ?? throw new ArgumentNullException(nameof(accountRepository));
            _interestRuleRepository = interestRuleRepository ?? throw new ArgumentNullException(nameof(interestRuleRepository));
            _transactionProcessor = transactionProcessor ?? throw new ArgumentNullException(nameof(transactionProcessor));
            _interestProcessor = interestProcessor ?? throw new ArgumentNullException(nameof(interestProcessor));
        }

        public string GetStatement(string input)
        {
            if (!StatementValidator.IsValidStatementInput(input.Split(' '), out var accountName, out var month))
                throw new ArgumentException(ErrorMessages.InvalidInputFormat);

            var account = _accountRepository.GetByName(accountName)
                ?? throw new KeyNotFoundException(ErrorMessages.AccountNotFoundFormatted);

            var statementBuilder = new StringBuilder();

            statementBuilder.AppendLine($"Account: {accountName}");
            statementBuilder.AppendLine(Domain.Constants.StatementConstants.Header);

            var openingBalance = _transactionProcessor.CalculateOpeningBalance(account, month);
            _transactionProcessor.AppendTransactionDetails(account, month, statementBuilder, ref openingBalance);

            var interestRules = _interestRuleRepository.GetAll();
            var interestPeriods = _interestProcessor.CalculateInterestPeriods(account.Transactions, interestRules, month);
            _interestProcessor.AppendInterestDetails(statementBuilder, interestPeriods);

            return statementBuilder.ToString();
        }
    }
}
