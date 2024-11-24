namespace BankingSystem.Application.Utilities
{
    public static class StringValidator
    {
        public static bool IsNonEmptyString(string input)
        {
            return !string.IsNullOrWhiteSpace(input);
        }
    }
}
