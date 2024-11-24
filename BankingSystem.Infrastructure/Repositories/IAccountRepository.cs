using BankingSystem.Domain.Entities;

namespace BankingSystem.Infrastructure.Repositories
{
    public interface IAccountRepository
    {
        Account? GetByName(string accountName);
        void Save(Account account);
    }
}