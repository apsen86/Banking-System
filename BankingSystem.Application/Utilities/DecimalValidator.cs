namespace BankingSystem.Application.Utilities
{
    public static class DecimalValidator
    {
        public static bool IsValidPositiveDecimal(string input, out decimal result)
        {
            result = 0;
            return decimal.TryParse(input, out result)
                   && result > 0
                   && Math.Round(result, 2) == result;
        }
    }
}
