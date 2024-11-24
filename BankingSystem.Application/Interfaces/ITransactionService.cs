using BankingSystem.Domain.Entities;

namespace BankingSystem.Application.Interfaces
{
    public interface ITransactionService
    {
        void AddTransaction(string input);
        Account GetAccount(string accountName);
    }
}
