using BankingSystem.Domain.Constants;
using BankingSystem.Domain.Entities;
using BankingSystem.Domain.Interfaces;
using System.Text;

namespace BankingSystem.Domain.Services
{
    public class InterestProcessor : IInterestProcessor
    {
        public List<(DateTime Start, DateTime End, decimal Balance, InterestRule Rule)> CalculateInterestPeriods(
            IEnumerable<BankTransaction> transactions,
            IEnumerable<InterestRule> rules,
            DateTime month)
        {
            ArgumentNullException.ThrowIfNull(transactions);
            ArgumentNullException.ThrowIfNull(rules);

            var openingBalance = CalculateOpeningBalance(transactions, month);
            var sortedRules = rules.OrderBy(r => r.EffectiveDate).ToList();

            return GenerateInterestPeriods(transactions, sortedRules, month, openingBalance);
        }

        private static decimal CalculateOpeningBalance(IEnumerable<BankTransaction> transactions, DateTime month)
        {
            var firstDayOfMonth = new DateTime(month.Year, month.Month, 1, 0, 0, 0, DateTimeKind.Utc);
            return transactions
                .Where(t => t.Date < firstDayOfMonth)
                .Sum(t => t.Type == TransactionType.D ? t.Amount : -t.Amount);
        }

        private static List<(DateTime Start, DateTime End, decimal Balance, InterestRule Rule)> GenerateInterestPeriods(
            IEnumerable<BankTransaction> transactions,
            List<InterestRule> sortedRules,
            DateTime month,
            decimal openingBalance)
        {
            var periods = new List<(DateTime Start, DateTime End, decimal Balance, InterestRule Rule)>();
            var currentStart = new DateTime(month.Year, month.Month, 1, 0, 0, 0, DateTimeKind.Utc);
            var monthEnd = new DateTime(month.Year, month.Month, DateTime.DaysInMonth(month.Year, month.Month), 23, 59, 59, DateTimeKind.Utc);
            var runningBalance = openingBalance;

            foreach (var rule in sortedRules)
            {
                if (rule.EffectiveDate > monthEnd) break;

                currentStart = UpdateStartForRule(currentStart, rule.EffectiveDate);
                var ruleEnd = GetRuleEnd(sortedRules, rule, monthEnd);

                ProcessTransactionsForRule(transactions, periods, ref runningBalance, ref currentStart, rule, ruleEnd);
            }

            return periods;
        }

        private static DateTime UpdateStartForRule(DateTime currentStart, DateTime effectiveDate)
        {
            return effectiveDate > currentStart ? effectiveDate : currentStart;
        }

        private static void ProcessTransactionsForRule(IEnumerable<BankTransaction> transactions,
            List<(DateTime Start, DateTime End, decimal Balance, InterestRule Rule)> periods,
            ref decimal runningBalance, ref DateTime currentStart, InterestRule rule, DateTime ruleEnd)
        {
            var localStart = currentStart;

            var transactionsInPeriod = transactions
                .Where(t => t.Date >= localStart && t.Date <= ruleEnd)
                .OrderBy(t => t.Date);

            foreach (var transaction in transactionsInPeriod)
            {
                if (transaction.Date > localStart)
                {
                    AddPeriod(periods, localStart, transaction.Date.AddDays(-1), runningBalance, rule);
                    localStart = transaction.Date;
                }

                runningBalance += transaction.Type == TransactionType.D ? transaction.Amount : -transaction.Amount;
            }

            if (localStart <= ruleEnd)
            {
                AddPeriod(periods, localStart, ruleEnd, runningBalance, rule);
            }

            currentStart = localStart;
        }

        private static void AddPeriod(List<(DateTime Start, DateTime End, decimal Balance, InterestRule Rule)> periods,
            DateTime start, DateTime end, decimal balance, InterestRule rule)
        {
            periods.Add((start, end, balance, rule));
        }

        private static DateTime GetRuleEnd(List<InterestRule> sortedRules, InterestRule currentRule, DateTime monthEnd)
        {
            var nextRule = sortedRules.Find(r => r.EffectiveDate > currentRule.EffectiveDate);
            return nextRule?.EffectiveDate.AddDays(-1) ?? monthEnd;
        }

        public void AppendInterestDetails(StringBuilder statementBuilder,
            IEnumerable<(DateTime Start, DateTime End, decimal Balance, InterestRule Rule)> interestPeriods)
        {
            ArgumentNullException.ThrowIfNull(statementBuilder);
            ArgumentNullException.ThrowIfNull(interestPeriods);

            decimal totalInterest = 0;

            foreach (var period in interestPeriods)
            {
                var periodInterest = CalculateInterestForPeriod(period);
                if (periodInterest > 0)
                {
                    totalInterest += periodInterest;
                }
            }

            AppendTotalInterest(statementBuilder, interestPeriods, totalInterest);
        }

        private static decimal CalculateInterestForPeriod((DateTime Start, DateTime End, decimal Balance, InterestRule Rule) period)
        {
            var numDays = (period.End - period.Start).Days + 1;
            return CalculateAnnualizedInterest(period.Balance, period.Rule.Rate, numDays);
        }

        private static void AppendTotalInterest(StringBuilder statementBuilder,
            IEnumerable<(DateTime Start, DateTime End, decimal Balance, InterestRule Rule)> interestPeriods,
            decimal totalInterest)
        {
            if (totalInterest > 0)
            {
                var lastPeriod = interestPeriods.Last();
                statementBuilder.AppendLine(string.Format(
                    StatementConstants.InterestRowFormat,
                    lastPeriod.End.ToString("yyyyMMdd"),
                    totalInterest.ToString("F2"),
                    (lastPeriod.Balance + totalInterest).ToString("F2")
                ));
            }
        }

        private static decimal CalculateAnnualizedInterest(decimal balance, decimal rate, int days)
        {
            var dailyRate = rate / 100 / InterestConstants.DaysInYear;
            return Math.Round(balance * dailyRate * days, InterestConstants.DecimalPlaces);
        }
    }
}
