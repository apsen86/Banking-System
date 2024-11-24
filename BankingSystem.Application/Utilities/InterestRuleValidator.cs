namespace BankingSystem.Application.Utilities
{
    public static class InterestRuleValidator
    {
        public static bool IsValidInterestRuleInput(
            string[] parts,
            out DateTime date,
            out string ruleId,
            out decimal rate)
        {
            date = default;
            ruleId = string.Empty;
            rate = 0;

            if (parts.Length != 3
                || !DateValidator.IsValidDate(parts[0], out date)
                || !StringValidator.IsNonEmptyString(parts[1])
                || !DecimalValidator.IsValidPositiveDecimal(parts[2], out rate)
                || rate >= 100)
            {
                return false;
            }

            ruleId = parts[1];
            return true;
        }
    }
}
