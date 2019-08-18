using SpaceConsole.ConsoleApp.Rendering;

namespace SpaceConsole.ConsoleApp.Controls
{
    public class UserControl<TContent> : Control
        where TContent : class, IControl
    {
        protected Box Box { get; }

        protected TContent Content { get => Box.Content as TContent; set => Box.Content = value; }

        public UserControl()
        {
            Box = new Box();
        }

        protected override void DoUpdate()
        {
            Box.Update();
        }

        protected override MeasureSize DoMeassure()
        {
            Box.Meassure();
            return Box.MeasuredSize;
        }

        protected override void DoArrange()
        {
            Box.Arrange(ActualSize);
        }

        protected override ConsoleBitmap DoRender()
        {
            return Box.Render();
        }
    }
}
