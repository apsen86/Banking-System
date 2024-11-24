namespace BankingSystem.Application.Constants
{
    public static class ErrorMessages
    {
        public const string AccountNotFound = "The specified account does not exist.";
        public const string InvalidTransactionType = "Transaction type must be 'D' for deposit or 'W' for withdrawal.";
        public const string InsufficientBalance = "Insufficient balance for withdrawal.";
        public const string InvalidTransactionAmount = "Transaction amount must be positive and have up to 2 decimal places.";
        public const string FirstTransactionCannotBeWithdrawal = "The first transaction for an account cannot be a withdrawal.";
        public const string AccountNotFoundFormatted = "Account '{0}' not found.";
        public const string InvalidAccountName = "Account name cannot be null or empty.";
        public const string InvalidInputFormat = "Invalid input format. Please try again.";
    }

    public static class InputMessages
    {
        public const string TransactionPrompt = "Please enter transaction details in <Date> <Account> <Type> <Amount> format";
        public const string InterestRulePrompt = "Please enter interest rules details in <Date> <RuleId> <Rate in %> format";
        public const string StatementPrompt = "Please enter account and month to generate the statement <Account> <Year><Month>";
        public const string BlankToExit = "(or enter blank to go back to main menu):";
    }

    public static class ProgramMessages
    {
        public const string ExistingTransactionsHeader = "| Date     | Txn Id      | Type |    Amount  |";
        public const string ExistingInterestRulesHeader = "| Date       | RuleId     | Rate (%)   |";
        public const string ExistingInterestRulesRowFormat = "| {0}   | {1,-10} | {2,10} |";
        public const string NoExistingTransactions = "No existing transactions found.";
        public const string NoExistingInterestRules = "No existing interest rules found.";
        public const string TransactionAddedSuccess = "Transaction added successfully.";
        public const string InterestRuleAddedSuccess = "Interest rule added successfully.";
        public const string DisplayTransactionsPrompt = "Existing transactions:";
        public const string DisplayInterestRulesPrompt = "Existing interest rules:";
    }
}