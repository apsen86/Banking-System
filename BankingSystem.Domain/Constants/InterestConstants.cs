namespace BankingSystem.Domain.Constants
{
    public static class InterestConstants
    {
        public const decimal RateDivisor = 100m;
        public const int DaysInYear = 365;
        public const int DecimalPlaces = 2;
        public const string DefaultRuleId = "DEFAULT";
    }
    public static class InterestRateConstants
    {
        public const decimal MinRate = 0;
        public const decimal MaxRate = 100;
        public const string InvalidRateMessage = "Rate must be between 0 and 100.";
    }
    public static class InterestRuleConstants
    {
        public const decimal MinRate = 0;
        public const decimal MaxRate = 100;
        public const string InvalidRateMessage = "Rate must be between 0 and 100.";
        public const string InvalidRuleIdMessage = "RuleId cannot be null or empty.";
    }
}
