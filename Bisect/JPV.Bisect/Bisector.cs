using System;
using System.ComponentModel;

namespace JPV.Bisect
{
    public static class Bisector
    {
        public static double Find(double searchedValue, double tolerance, Func<double, double> func, int maxIterations)
        {
            return Find(ratio =>
            {
                var value = func(ratio);

                var valueDiff = value - searchedValue;

                if (Math.Abs(valueDiff) < tolerance)
                    return BisectResult.Match;

                return valueDiff < 0
                   ? BisectResult.TooLow
                   : BisectResult.TooHigh;
            }, maxIterations);
        }

        public static double Find(Func<double, BisectResult> func, int maxIterations)
        {
            var low = 0.00;
            var high = 1.00;
            var current = double.NaN;

            {
                var result = func(low);

                switch (result)
                {
                    case BisectResult.Match:
                        return low;
                    case BisectResult.TooLow:
                        break;
                    case BisectResult.TooHigh:
                        throw new InvalidOperationException("Searched value is not within range.");
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }

            {
                var result = func(high);

                switch (result)
                {
                    case BisectResult.Match:
                        return high;
                    case BisectResult.TooLow:
                        throw new InvalidOperationException("Searched value is not within range.");
                    case BisectResult.TooHigh:
                        break;
                    default:
                        throw new InvalidEnumArgumentException();
                }
            }

            for (var i = 0; i < maxIterations; i++)
            {
                current = low + ((high - low) / 2);

                var result = func(current);

                switch (result)
                {
                    case BisectResult.Match:
                        return current;
                    case BisectResult.TooLow:
                        low = current;
                        break;
                    case BisectResult.TooHigh:
                        high = current;
                        break;
                    default:
                        throw new InvalidEnumArgumentException();
                }
            }

            return current;
        }
    }
}
