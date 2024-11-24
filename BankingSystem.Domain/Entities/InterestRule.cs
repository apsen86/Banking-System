using BankingSystem.Domain.Constants;

namespace BankingSystem.Domain.Entities
{
    public class InterestRule
    {
        public DateTime EffectiveDate { get; private set; }
        public string RuleId { get; private set; }
        public decimal Rate { get; private set; }

        public InterestRule(DateTime effectiveDate, string ruleId, decimal rate)
        {
            if (rate < InterestRuleConstants.MinRate || rate > InterestRuleConstants.MaxRate)
                throw new ArgumentException(InterestRuleConstants.InvalidRateMessage);

            if (string.IsNullOrWhiteSpace(ruleId))
                throw new ArgumentException(InterestRuleConstants.InvalidRuleIdMessage);

            EffectiveDate = effectiveDate;
            RuleId = ruleId;
            Rate = rate;
        }
    }
}
