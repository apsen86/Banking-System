namespace BankingSystem.Domain.Constants
{
    public static class StatementConstants
    {
        public const string Header = "| Date       | Txn Id      | Type | Amount    | Balance   |";
        public const string TransactionRowFormat = "| {0:yyyyMMdd} | {1,-10} | {2,-4} | {3,10:F2} |";
        public const string TransactionRowStatmentFormat = "| {0:yyyyMMdd} | {1,-10} | {2,-4} | {3,10:F2} | {4,10:F2} |";
        public const string InterestRowFormat = "| {0:yyyyMMdd} |             | I    | {1,10:F2} | {2,10:F2} |";
        public const string Separator = "--------------------------------------------------------";
        public const string AccountHeader = "Account: {0}";
        public static string InterestPeriodRowFormat => "| {0} - {1} | {2,10} | {3,10} | {4,8} | {5,6} | {6} |";

    }
}
