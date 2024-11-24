using BankingSystem.Application.Constants;
using BankingSystem.Application.Interfaces;
using BankingSystem.Application.Utilities;
using BankingSystem.Domain.Entities;
using BankingSystem.Infrastructure.Repositories;

namespace BankingSystem.Application.Services
{
    public class InterestRuleService : IInterestRuleService
    {
        private readonly IInterestRuleRepository _interestRuleRepository;

        public InterestRuleService(IInterestRuleRepository interestRuleRepository)
        {
            _interestRuleRepository = interestRuleRepository ?? throw new ArgumentNullException(nameof(interestRuleRepository));
        }

        public void AddInterestRule(string input)
        {
            if (!InterestRuleValidator.IsValidInterestRuleInput(input.Split(' '), out var date, out var ruleId, out var rate))
                throw new ArgumentException(ErrorMessages.InvalidInputFormat);

            var existingRule = _interestRuleRepository.GetAll().FirstOrDefault(r => r.EffectiveDate == date);
            if (existingRule != null)
                _interestRuleRepository.Remove(existingRule);

            _interestRuleRepository.Add(new InterestRule(date, ruleId, rate));

            Console.WriteLine(ProgramMessages.InterestRuleAddedSuccess);
            DisplayExistingInterestRules();
        }

        public IEnumerable<InterestRule> GetAllInterestRules()
        {
            var rules = _interestRuleRepository.GetAll().OrderBy(r => r.EffectiveDate).ToList();

            if (rules.Count == 0)
            {
                Console.WriteLine(ProgramMessages.NoExistingInterestRules);
                return Enumerable.Empty<InterestRule>();
            }

            Console.WriteLine(ProgramMessages.DisplayInterestRulesPrompt);
            PrintInterestRules(rules);

            return rules;
        }

        private void DisplayExistingInterestRules()
        {
            var rules = _interestRuleRepository.GetAll().OrderBy(r => r.EffectiveDate).ToList();

            if (rules.Count == 0)
            {
                Console.WriteLine(ProgramMessages.NoExistingInterestRules);
                return;
            }

            Console.WriteLine(ProgramMessages.DisplayInterestRulesPrompt);
            PrintInterestRules(rules);
        }

        private static void PrintInterestRules(IEnumerable<InterestRule> rules)
        {
            Console.WriteLine(ProgramMessages.ExistingInterestRulesHeader);

            foreach (var rule in rules)
            {
                Console.WriteLine(string.Format(
                    ProgramMessages.ExistingInterestRulesRowFormat,
                    rule.EffectiveDate.ToString("yyyyMMdd"),
                    rule.RuleId,
                    rule.Rate.ToString("F2")
                ));
            }
        }
    }
}
