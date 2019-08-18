using System;
using System.Drawing;

namespace SpaceConsole.ConsoleApp.Controls
{
    public static class MeasureMath
    {
        public static Size Min(MeasureSize a, Size b)
        {
            return new Size(
                Min(a.Width, b.Width),
                Min(a.Height, b.Height));
        }

        public static int Min(Dimension a, int b)
        {
            return Math.Min(a.Value, b);
        }
    }
}
