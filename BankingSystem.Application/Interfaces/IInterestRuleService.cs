using BankingSystem.Domain.Entities;

namespace BankingSystem.Application.Interfaces
{
    public interface IInterestRuleService
    {
        void AddInterestRule(string input);
        IEnumerable<InterestRule> GetAllInterestRules();
    }
}
