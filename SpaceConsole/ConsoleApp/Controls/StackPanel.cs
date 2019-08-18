using System.Collections.Generic;
using System.Linq;
using SpaceConsole.ConsoleApp.Rendering;

namespace SpaceConsole.ConsoleApp.Controls
{
    public sealed class StackPanel : Control
    {
        public IList<IControl> Rows { get; set; } = new List<IControl>();

        protected override void DoUpdate()
        {
            foreach (var cell in Rows)
            {
                cell.Update();
            }
        }

        protected override MeasureSize DoMeassure()
        {
            if (Rows.Count == 0)
                return MeasureSize.Zero;

            foreach (var child in Rows)
            {
                child.Meassure();
            }

            var childMeassures = Rows
                .Select(child => child.MeasuredSize)
                .ToList();

            var width = childMeassures
                .Select(childMeassure => childMeassure.Width)
                .Max();

            var height = childMeassures
                .Select(childMeassure => childMeassure.Height)
                .Sum();

            return new MeasureSize(width, height);
        }

        protected override void DoArrange()
        {
            var remainingHeight = ActualSize.Height;

            foreach (var child in Rows.Where(c => !c.MeasuredSize.Height.IsInfinite))
            {
                var width = DimensionMath.Min(child.MeasuredSize.Width, ActualSize.Width);
                var height = DimensionMath.Min(child.MeasuredSize.Height, remainingHeight);

                remainingHeight -= height;

                child.Arrange(width, height);
            }

            foreach (var child in Rows.Where(c => c.MeasuredSize.Height.IsInfinite))
            {
                var width = DimensionMath.Min(child.MeasuredSize.Width, ActualSize.Width);
                var height = DimensionMath.Min(child.MeasuredSize.Height, remainingHeight);

                remainingHeight -= height;

                child.Arrange(width, height);
            }
        }

        protected override ConsoleBitmap DoRender()
        {
            return Rows.Select(child => child.Render()).StackedVertical();
        }
    }
}
