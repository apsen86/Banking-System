using BankingSystem.Application.Constants;
using BankingSystem.Application.Interfaces;
using BankingSystem.Application.Utilities;
using BankingSystem.Domain.Entities;

using BankingSystem.Infrastructure.Repositories;

namespace BankingSystem.Application.Services
{
    public class TransactionService : ITransactionService
    {
        private readonly IAccountRepository _accountRepository;

        public TransactionService(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository ?? throw new ArgumentNullException(nameof(accountRepository));
        }

        public void AddTransaction(string input)
        {
            if (!TransactionValidator.IsValidTransactionInput(input.Split(' '), out var date, out var accountName, out var type, out var amount))
                throw new ArgumentException(InputMessages.TransactionPrompt);

            var account = _accountRepository.GetByName(accountName) ?? new Account(accountName);


            if (type == "W" && account.Transactions.Count == 0)
                throw new InvalidOperationException(ErrorMessages.FirstTransactionCannotBeWithdrawal);

            if (type == "W" && account.Balance < amount)
                throw new InvalidOperationException(ErrorMessages.InsufficientBalance);

            var transaction = BankTransaction.Create(type, amount, date, account.GetNextTransactionNumber(date));
            account.AddTransaction(transaction);

            _accountRepository.Save(account);
            Console.WriteLine(ProgramMessages.TransactionAddedSuccess);

            if (account.Transactions.Any())
            {
                Console.WriteLine(ProgramMessages.DisplayTransactionsPrompt);
                Console.WriteLine(ProgramMessages.ExistingTransactionsHeader);
                foreach (var txn in account.Transactions.OrderBy(t => t.Date))
                {
                    Console.WriteLine(string.Format(Domain.Constants.StatementConstants.TransactionRowFormat, txn.Date, txn.Id, txn.Type, txn.Amount));
                }
            }
            else
            {
                Console.WriteLine(ProgramMessages.NoExistingTransactions);
            }
        }

        public Account GetAccount(string accountName)
        {
            if (string.IsNullOrWhiteSpace(accountName))
                throw new ArgumentException(ErrorMessages.InvalidAccountName);

            return _accountRepository.GetByName(accountName)
                   ?? throw new ArgumentException(string.Format(ErrorMessages.AccountNotFoundFormatted, accountName));
        }
    }
}
