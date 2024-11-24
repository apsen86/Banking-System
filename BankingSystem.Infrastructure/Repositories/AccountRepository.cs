using BankingSystem.Domain.Entities;

namespace BankingSystem.Infrastructure.Repositories
{
    public class AccountRepository : IAccountRepository
    {
        private readonly Dictionary<string, Account> _accounts = new();

        public Account? GetByName(string accountName)
        {
            _accounts.TryGetValue(accountName, out var account);
            return account;
        }

        public void Save(Account account)
        {
            _accounts[account.AccountName] = account;
        }
    }
}