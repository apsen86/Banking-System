namespace BankingSystem.Application.Utilities
{
    public static class StatementValidator
    {
        public static bool IsValidStatementInput(string[] parts, out string accountName, out DateTime month)
        {
            accountName = string.Empty;
            month = default;

            if (parts.Length != 2) return false;

            accountName = parts[0];
            return DateTime.TryParseExact(
                parts[1] + "01",
                "yyyyMMdd",
                System.Globalization.CultureInfo.InvariantCulture,
                System.Globalization.DateTimeStyles.None,
                out month);
        }
    }
}
