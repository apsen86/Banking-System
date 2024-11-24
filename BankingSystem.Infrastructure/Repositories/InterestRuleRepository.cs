using BankingSystem.Domain.Entities;

namespace BankingSystem.Infrastructure.Repositories
{
    public class InterestRuleRepository : IInterestRuleRepository
    {
        private readonly List<InterestRule> _rules = new();

        public void Add(InterestRule rule)
        {
            _rules.Add(rule);
        }

        public void Remove(InterestRule rule)
        {
            _rules.Remove(rule);
        }

        public IEnumerable<InterestRule> GetAll()
        {
            return _rules;
        }
    }
}