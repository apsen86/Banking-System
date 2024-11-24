namespace BankingSystem.Application.Utilities
{
    public static class TransactionTypeValidator
    {
        public static bool IsValidTransactionType(string type)
        {
            return !string.IsNullOrWhiteSpace(type) &&
                   (string.Equals(type, "D", StringComparison.OrdinalIgnoreCase) ||
                    string.Equals(type, "W", StringComparison.OrdinalIgnoreCase));
        }
    }
}
