namespace BankingSystem.Domain.Entities
{
    public class InterestPeriod
    {
        public DateTime Start { get; }
        public DateTime End { get; }
        public decimal Balance { get; }
        public InterestRule Rule { get; }

        public InterestPeriod(DateTime start, DateTime end, decimal balance, InterestRule rule)
        {
            Start = start;
            End = end;
            Balance = balance;
            Rule = rule;
        }
    }
}