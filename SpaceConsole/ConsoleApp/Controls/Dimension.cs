using System.Globalization;

namespace SpaceConsole.ConsoleApp.Controls
{
    public struct Dimension
    {
        public static Dimension Zero { get; } = new Dimension(0);

        public static Dimension Infinite { get; } = new Dimension(int.MaxValue);

        public static Dimension operator +(Dimension left, Dimension right)
        {
            if (left.IsInfinite || right.IsInfinite)
                return Infinite;

            return Real(left.Value + right.Value);
        }

        public static Dimension operator +(Dimension left, int right)
        {
            return left.IsInfinite
                ? Infinite
                : Real(left.Value + right);
        }

        public static Dimension Real(int value)
        {
            return new Dimension(value);
        }

        public int Value { get; }

        public bool IsInfinite => Value == int.MaxValue;

        public Dimension(int value)
        {
            Value = value;
        }

        public override string ToString()
        {
            return IsInfinite
                ? "Infinite"
                : Value.ToString(CultureInfo.InvariantCulture);
        }
    }
}