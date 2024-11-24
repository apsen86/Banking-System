using Microsoft.Extensions.DependencyInjection;
using BankingSystem.Application.Interfaces;
using BankingSystem.Application.Services;
using BankingSystem.Domain.Services;
using BankingSystem.Infrastructure.Repositories;
using BankingSystem.Domain.Interfaces;

namespace BankingSystem.ConsoleApp
{
    public static class DependencyInjection
    {
        public static ServiceProvider ConfigureServices()
        {
            var services = new ServiceCollection();

            // Application service registrations
            services.AddScoped<ITransactionService, TransactionService>();
            services.AddScoped<IInterestRuleService, InterestRuleService>();
            services.AddScoped<IStatementService, StatementService>();

            // Domain service registrations
            services.AddScoped<IStatementGenerator, StatementGenerator>();
            services.AddScoped<ITransactionProcessor, TransactionProcessor>();
            services.AddScoped<IInterestProcessor, InterestProcessor>();

            // Repository registrations
            services.AddSingleton<IAccountRepository, AccountRepository>();
            services.AddSingleton<IInterestRuleRepository, InterestRuleRepository>();

            return services.BuildServiceProvider();
        }
    }
}
