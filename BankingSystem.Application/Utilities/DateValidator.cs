namespace BankingSystem.Application.Utilities
{
    public static class DateValidator
    {
        public static bool IsValidDate(string dateString, out DateTime date)
        {
            return DateTime.TryParseExact(
                dateString,
                "yyyyMMdd",
                System.Globalization.CultureInfo.InvariantCulture,
                System.Globalization.DateTimeStyles.None,
                out date);
        }
    }

}