namespace BankingSystem.Domain.Constants
{
    public static class ErrorMessages
    {
        public const string EmptyAccountName = "Account name cannot be empty.";
        public const string InsufficientBalance = "Insufficient balance for this withdrawal.";
        public const string InvalidTransactionType = "Transaction type must be 'D' for deposit or 'W' for withdrawal.";
        public const string InvalidTransactionAmount = "Transaction amount must be positive and have up to 2 decimal places.";
        public const string InvalidInterestRate = "Interest rate must be between 0 and 100%.";
        public const string EmptyRuleId = "Rule ID cannot be null or empty.";
        public const string InvalidInterestPeriod = "Interest period must be at least one day.";
    }
}
