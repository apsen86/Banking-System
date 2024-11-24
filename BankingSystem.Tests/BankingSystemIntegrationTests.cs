using BankingSystem.Application.Services;
using BankingSystem.Domain.Entities;
using BankingSystem.Domain.Services;
using BankingSystem.Infrastructure.Repositories;

namespace BankingSystem.Tests
{
    [TestClass]
    public class BankingSystemIntegrationTests
    {
        private InMemoryAccountRepository? _accountRepository;
        private InMemoryInterestRuleRepository? _interestRuleRepository;
        private TransactionService? _transactionService;
        private InterestRuleService? _interestRuleService;
        private StatementService? _statementService;

        [TestInitialize]
        public void Setup()
        {
            _accountRepository = new InMemoryAccountRepository();
            _interestRuleRepository = new InMemoryInterestRuleRepository();

            var transactionProcessor = new TransactionProcessor();
            var interestProcessor = new InterestProcessor();

            _transactionService = new TransactionService(_accountRepository);
            _interestRuleService = new InterestRuleService(_interestRuleRepository);
            _statementService = new StatementService(
                _accountRepository,
                _interestRuleRepository,
                transactionProcessor,
                interestProcessor);
        }

        [TestMethod]
        public void ValidateTransactionConstraints()
        {
            _transactionService?.AddTransaction("20230505 AC001 D 100.00");
            _transactionService?.AddTransaction("20230601 AC001 D 150.00");
            _transactionService?.AddTransaction("20230626 AC001 W 20.00");
            _transactionService?.AddTransaction("20230626 AC001 W 100.00");

            var account = _accountRepository?.GetByName("AC001");
            Assert.IsNotNull(account);
            Assert.AreEqual(4, account.Transactions.Count);
            Assert.AreEqual(130.00m, account.Balance);
        }

        [TestMethod]
        public void AddTransaction_InvalidFirstTransaction_ThrowsException()
        {
            Assert.ThrowsException<InvalidOperationException>(() =>
                _transactionService?.AddTransaction("20230505 AC001 W 100.00"));
        }

        [TestMethod]
        public void AddTransaction_InvalidDateFormat_ThrowsException()
        {
            Assert.ThrowsException<ArgumentException>(() =>
                _transactionService?.AddTransaction("05-05-2023 AC001 D 100.00"));
        }

        [TestMethod]
        public void AddTransaction_InvalidType_ThrowsException()
        {
            Assert.ThrowsException<ArgumentException>(() =>
                _transactionService?.AddTransaction("20230505 AC001 X 100.00"));
        }

        [TestMethod]
        public void AddTransaction_InvalidAmount_ThrowsException()
        {
            Assert.ThrowsException<ArgumentException>(() =>
                _transactionService?.AddTransaction("20230505 AC001 D -50.00"));
            Assert.ThrowsException<ArgumentException>(() =>
                _transactionService?.AddTransaction("20230505 AC001 D 0.0001"));
        }

        [TestMethod]
        public void ValidateInterestRuleConstraints()
        {
            _interestRuleService?.AddInterestRule("20230101 RULE01 1.95");
            _interestRuleService?.AddInterestRule("20230520 RULE02 1.90");
            _interestRuleService?.AddInterestRule("20230615 RULE03 2.20");

            var rules = _interestRuleRepository?.GetAll();
            Assert.AreEqual(3, rules?.Count());
        }

        [TestMethod]
        public void AddInterestRule_InvalidDateFormat_ThrowsException()
        {
            Assert.ThrowsException<ArgumentException>(() =>
                _interestRuleService?.AddInterestRule("01-01-2023 RULE01 1.95"));
        }

        [TestMethod]
        public void AddInterestRule_InvalidRate_ThrowsException()
        {
            Assert.ThrowsException<ArgumentException>(() =>
                _interestRuleService?.AddInterestRule("20230101 RULE01 -1.00"));
            Assert.ThrowsException<ArgumentException>(() =>
                _interestRuleService?.AddInterestRule("20230101 RULE01 101.00"));
        }

        [TestMethod]
        public void AddInterestRule_DuplicateDate_ReplacesExistingRule()
        {
            _interestRuleService?.AddInterestRule("20230101 RULE01 1.95");
            _interestRuleService?.AddInterestRule("20230101 RULE01 2.00");

            var rules = _interestRuleService?.GetAllInterestRules();
            Assert.AreEqual(1, rules?.Count(), "Duplicate date was not handled correctly.");
            Assert.AreEqual(2.00m, rules?.First().Rate, "Existing rule was not replaced with the new rate.");
        }

        [TestMethod]
        public void ValidateStatementConstraints()
        {
            _transactionService?.AddTransaction("20230505 AC001 D 100.00");
            _transactionService?.AddTransaction("20230601 AC001 D 150.00");
            _transactionService?.AddTransaction("20230626 AC001 W 20.00");
            _transactionService?.AddTransaction("20230626 AC001 W 100.00");

            _interestRuleService?.AddInterestRule("20230101 RULE01 1.95");
            _interestRuleService?.AddInterestRule("20230520 RULE02 1.90");
            _interestRuleService?.AddInterestRule("20230615 RULE03 2.20");

            var statement = _statementService?.GetStatement("AC001 202306");

            Assert.IsNotNull(statement);
            Assert.IsTrue(statement.Contains("20230630"));
            Assert.IsTrue(statement.Contains("0.39"));
            Assert.IsTrue(statement.Contains("130.39"));
        }

        [TestMethod]
        public void GetStatement_InvalidAccount_ThrowsException()
        {
            Assert.ThrowsException<KeyNotFoundException>(() =>
                _statementService?.GetStatement("InvalidAccount 202306"));
        }

        [TestMethod]
        public void GetStatement_InvalidMonthFormat_ThrowsException()
        {
            Assert.ThrowsException<ArgumentException>(() =>
                _statementService?.GetStatement("AC001 2023-06"));
        }

        private class InMemoryAccountRepository : IAccountRepository
        {
            private readonly Dictionary<string, Account> _accounts = new();

            public Account? GetByName(string accountName)
            {
                _accounts.TryGetValue(accountName, out var account);
                return account;
            }

            public void Save(Account account)
            {
                _accounts[account.AccountName] = account;
            }
        }

        private class InMemoryInterestRuleRepository : IInterestRuleRepository
        {
            private readonly List<InterestRule> _rules = new();

            public IEnumerable<InterestRule> GetAll()
            {
                return _rules;
            }

            public void Add(InterestRule rule)
            {
                _rules.Add(rule);
            }

            public void Remove(InterestRule rule)
            {
                _rules.Remove(rule);
            }
        }
    }
}
