namespace BankingSystem.ConsoleApp.Constatnts
{
    public static class ProgramMessages
    {
        public const string MainMenuOptions = "[T] Input Transactions\n[I] Define Interest Rules\n[P] Print Statement\n[Q] Quit";
        public const string ExitMessage = "Thank you for banking with AwesomeGIC Bank.\nHave a nice day!";
        public const string InvalidOption = "Invalid option. Please try again.";
        public const string InvalidInputFormat = "Invalid input format. Please try again.";
        public const string TransactionPrompt = "Please enter transaction details in <Date> <Account> <Type> <Amount> format\n(or enter blank to go back to main menu):";
        public const string InterestRulePrompt = "Please enter interest rules details in <Date> <RuleId> <Rate in %> format\n(or enter blank to go back to main menu):";
        public const string StatementPrompt = "Please enter account and month to generate the statement <Account> <Year><Month>\n(or enter blank to go back to main menu):";
        public const string TransactionHeader = "| Date     | Txn Id      | Type | Amount |";
        public const string InterestRuleHeader = "| Date     | RuleId | Rate (%) |";

        public const string ServicesInitializationError = "Error: Required services could not be initialized.";
    }
}
