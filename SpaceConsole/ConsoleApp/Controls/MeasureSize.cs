namespace SpaceConsole.ConsoleApp.Controls
{
    public struct MeasureSize
    {
        public static MeasureSize Zero { get; } = new MeasureSize(Dimension.Zero, Dimension.Zero);

        public static MeasureSize Infinite { get; } = new MeasureSize(Dimension.Infinite, Dimension.Infinite);

        public Dimension Width { get; }

        public Dimension Height { get; }

        public bool IsZero => Width.Value == 0 || Height.Value == 0;

        public bool IsInfinite => Width.IsInfinite || Height.IsInfinite;

        public MeasureSize(Dimension width, Dimension height)
        {
            Width = width;
            Height = height;
        }

        public override string ToString()
        {
            return $"{Width} x {Height}";
        }
    }
}
