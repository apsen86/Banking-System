using BankingSystem.Domain.Constants;

namespace BankingSystem.Domain.ValueObjects
{
    public class InterestRate
    {
        public decimal Rate { get; private set; }

        public InterestRate(decimal rate)
        {
            if (rate <= InterestRateConstants.MinRate || rate >= InterestRateConstants.MaxRate)
                throw new ArgumentException(InterestRateConstants.InvalidRateMessage);

            Rate = rate;
        }

        public override bool Equals(object? obj)
        {
            if (obj is InterestRate other)
                return Rate == other.Rate;

            return false;
        }

        public override int GetHashCode() => Rate.GetHashCode();

        public override string ToString() => $"{Rate}%";
    }
}
