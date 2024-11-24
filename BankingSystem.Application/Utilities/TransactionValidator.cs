namespace BankingSystem.Application.Utilities
{
    public static class TransactionValidator
    {
        public static bool IsValidTransactionInput(
            string[] parts,
            out DateTime date,
            out string accountName,
            out string type,
            out decimal amount)
        {
            date = default;
            accountName = string.Empty;
            type = string.Empty;
            amount = 0;

            if (parts.Length != 4
                || !DateValidator.IsValidDate(parts[0], out date)
                || !StringValidator.IsNonEmptyString(parts[1])
                || !TransactionTypeValidator.IsValidTransactionType(parts[2])
                || !DecimalValidator.IsValidPositiveDecimal(parts[3], out amount))
            {
                return false;
            }

            accountName = parts[1];
            type = parts[2].ToUpper();
            return true;
        }
    }
}
