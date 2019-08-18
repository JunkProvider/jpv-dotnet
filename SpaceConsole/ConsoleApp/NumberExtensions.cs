using System.Globalization;

namespace SpaceConsole.ConsoleApp
{
    public static class NumberExtensions
    {
        public static string ToStringInvariant(this int value)
        {
            return value.ToString("0", CultureInfo.InvariantCulture);
        }

        public static string ToStringInvariant(this double value, int decimals = 2)
        {
            return value.ToString($"0.{new string('0', decimals)}", CultureInfo.InvariantCulture);
        }

        public static string ToStringInvariant(this decimal value)
        {
            return value.ToString("0.00", CultureInfo.InvariantCulture);
        }
    }
}
