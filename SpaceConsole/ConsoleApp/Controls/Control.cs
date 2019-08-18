using System.Drawing;
using SpaceConsole.ConsoleApp.Rendering;

namespace SpaceConsole.ConsoleApp.Controls
{
    public class Control : IControl
    {
        public MeasureSize MeasuredSize { get; private set; }

        public Size ActualSize { get; private set; }

        public void Update()
        {
            DoUpdate();
        }

        protected virtual void DoUpdate()
        {
        }

        public void Meassure()
        {
            MeasuredSize = DoMeassure();
        }

        protected virtual MeasureSize DoMeassure()
        {
            return MeasureSize.Zero;
        }

        public void Arrange(Size size)
        {
            ActualSize = size;
            DoArrange();
        }

        protected virtual void DoArrange()
        {
        }

        public ConsoleBitmap Render()
        {
            return DoRender().WithSize(ActualSize);
        }

        protected virtual ConsoleBitmap DoRender()
        {
            return new ConsoleBitmap(0, 0);
        }

        public override string ToString()
        {
            return $"{GetType().Name} {ActualSize.Width.ToStringInvariant()}x{ActualSize.Height.ToStringInvariant()}";
        }
    }
}
