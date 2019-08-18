using System;
using SpaceConsole.ConsoleApp.Rendering;

namespace SpaceConsole.ConsoleApp.Controls
{
    public sealed class Text : Control
    {
        public string Value { get; set; } = string.Empty;

        public ConsoleColor ForegroundColor { get; set; } = ConsoleColor.White;

        public Action<Text> UpdateFunc { get; set; }

        protected override void DoUpdate()
        {
            UpdateFunc?.Invoke(this);
        }

        protected override MeasureSize DoMeassure()
        {
            return new MeasureSize(new Dimension(Value.Length), new Dimension(1));
        }

        protected override ConsoleBitmap DoRender()
        {
            return new ConsoleBitmap(Value, ForegroundColor)
                .WithSize(ActualSize);
        }
    }
}
