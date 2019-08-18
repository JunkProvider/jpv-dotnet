namespace QuantityGenerator
{
    public static class StringExtensions
    {
        public static string FirstLetterToLower(this string s)
        {
            if (string.IsNullOrEmpty(s))
                return s;

            return s.Substring(0, 1).ToLowerInvariant() + s.Substring(1);
        }
    }
}