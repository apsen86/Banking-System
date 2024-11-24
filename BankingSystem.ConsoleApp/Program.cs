using Microsoft.Extensions.DependencyInjection;
using BankingSystem.Application.Interfaces;
using BankingSystem.ConsoleApp.Constatnts;

namespace BankingSystem.ConsoleApp
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            var serviceProvider = DependencyInjection.ConfigureServices();

            var transactionService = serviceProvider.GetService<ITransactionService>();
            var interestRuleService = serviceProvider.GetService<IInterestRuleService>();
            var statementService = serviceProvider.GetService<IStatementService>();

            // Validate services to prevent null reference exceptions
            if (transactionService == null || interestRuleService == null || statementService == null)
            {
                Console.WriteLine("Error: Failed to initialize required services. Please check the configuration.");
                return;
            }

            while (true)
            {
                Console.WriteLine(ProgramMessages.MainMenuOptions);

                var choice = Console.ReadLine()?.Trim().ToUpper();

                if (string.IsNullOrEmpty(choice))
                {
                    Console.WriteLine("Error: Invalid input. Please select a valid option.");
                    continue;
                }

                if (choice == "Q")
                {
                    Console.WriteLine(ProgramMessages.ExitMessage);
                    break;
                }

                HandleAction(choice, transactionService, interestRuleService, statementService);
            }
        }

        static void HandleAction(string choice, ITransactionService transactionService, IInterestRuleService interestRuleService, IStatementService statementService)
        {
            if (transactionService == null) throw new ArgumentNullException(nameof(transactionService));
            if (interestRuleService == null) throw new ArgumentNullException(nameof(interestRuleService));
            if (statementService == null) throw new ArgumentNullException(nameof(statementService));

            try
            {
                switch (choice)
                {
                    case "T":
                        HandleTransactionInput(transactionService);
                        break;
                    case "I":
                        HandleInterestRuleInput(interestRuleService);
                        break;
                    case "P":
                        HandlePrintStatement(statementService);
                        break;
                    default:
                        Console.WriteLine(ProgramMessages.InvalidOption);
                        break;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        static void HandleTransactionInput(ITransactionService transactionService)
        {
            if (transactionService == null) throw new ArgumentNullException(nameof(transactionService));

            while (true)
            {
                Console.WriteLine(ProgramMessages.TransactionPrompt);
                var input = Console.ReadLine()?.Trim();
                if (string.IsNullOrEmpty(input)) break;

                ExceptionWrapper(() => transactionService.AddTransaction(input));
            }
        }

        static void HandleInterestRuleInput(IInterestRuleService interestRuleService)
        {
            if (interestRuleService == null) throw new ArgumentNullException(nameof(interestRuleService));

            while (true)
            {
                Console.WriteLine(ProgramMessages.InterestRulePrompt);
                var input = Console.ReadLine()?.Trim();
                if (string.IsNullOrEmpty(input)) break;

                ExceptionWrapper(() => interestRuleService.AddInterestRule(input));
            }
        }

        static void HandlePrintStatement(IStatementService statementService)
        {
            if (statementService == null) throw new ArgumentNullException(nameof(statementService));

            while (true)
            {
                Console.WriteLine(ProgramMessages.StatementPrompt);
                var input = Console.ReadLine()?.Trim();
                if (string.IsNullOrEmpty(input)) break;

                ExceptionWrapper(() => Console.WriteLine(statementService.GetStatement(input)));
            }
        }

        static void ExceptionWrapper(Action action)
        {
            try
            {
                action();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }
    }
}
