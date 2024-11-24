using BankingSystem.Domain.Entities;

namespace BankingSystem.Domain.Interfaces
{
    public interface IStatementGenerator
    {
        string Generate(Account account, DateTime month, IEnumerable<InterestRule> rules);
    }
}
