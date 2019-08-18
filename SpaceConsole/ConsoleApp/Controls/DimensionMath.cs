using System;

namespace SpaceConsole.ConsoleApp.Controls
{
    public static class DimensionMath
    {
        public static Dimension Min(Dimension a, Dimension b)
        {
            return new Dimension(Math.Min(a.Value, b.Value));
        }

        public static int Min(Dimension a, int b)
        {
            return Math.Min(a.Value, b);
        }
    }
}
