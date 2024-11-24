using BankingSystem.Domain.Entities;

namespace BankingSystem.Infrastructure.Repositories
{
    public interface IInterestRuleRepository
    {
        void Add(InterestRule rule);
        void Remove(InterestRule rule);
        IEnumerable<InterestRule> GetAll();
    }
}