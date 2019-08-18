using System;
using System.Drawing;
using SpaceConsole.ConsoleApp.Rendering;

namespace SpaceConsole.ConsoleApp.Controls
{
    public sealed class Box : Control
    {
        public Dimension? Width { get; set; } = null;

        public Dimension? Height { get; set; } = null;

        public int PaddingLeft { get; set; }

        public int PaddingTop { get; set; }

        public int PaddingRight { get; set; }

        public int PaddingBottom { get; set; }

        public int Padding
        {
            set
            {
                PaddingLeft = value;
                PaddingTop = value;
                PaddingRight = value;
                PaddingBottom = value;
            }
        }

        public Size ActualInnerSize => new Size(
            ActualSize.Width - (PaddingLeft + PaddingRight),
            ActualSize.Height - (PaddingTop + PaddingBottom));

        public ConsoleColor? BackgroundColor { get; set; }

        public IControl Content { get; set; }

        protected override void DoUpdate()
        {
            Content?.Update();
        }

        protected override MeasureSize DoMeassure()
        {
            Content?.Meassure();

            return new MeasureSize(MeasureWidth(), MeasureHeight());
        }

        private Dimension MeasureWidth()
        {
            if (Width.HasValue)
                return Width.Value;

            var contentWidth = Content?.MeasuredSize.Width ?? Dimension.Zero;

            return contentWidth + PaddingLeft + PaddingRight;
        }

        private Dimension MeasureHeight()
        {
            if (Height.HasValue)
                return Height.Value;

            var contentHeight = Content?.MeasuredSize.Height ?? Dimension.Zero;

            return contentHeight + PaddingTop + PaddingBottom;
        }

        protected override void DoArrange()
        {
            Content?.Arrange(MeasureMath.Min(Content.MeasuredSize, ActualSize));
        }

        protected override ConsoleBitmap DoRender()
        {
            if (ActualSize.IsZero())
                return ConsoleBitmap.Empty;

            var contentBitmap = Content?.Render().WithMaxSize(ActualInnerSize) ?? ConsoleBitmap.Empty;

            var bitmap = new ConsoleBitmap(ActualSize)
                .WithInserted(PaddingLeft, PaddingTop, contentBitmap);

            if (BackgroundColor.HasValue)
                bitmap = bitmap.WithFilledBackground(BackgroundColor.Value);

            return bitmap;
        }
    }
}
