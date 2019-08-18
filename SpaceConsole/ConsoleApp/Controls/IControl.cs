using System.Drawing;
using SpaceConsole.ConsoleApp.Rendering;

namespace SpaceConsole.ConsoleApp.Controls
{
    public interface IControl
    {
        MeasureSize MeasuredSize { get; }

        Size ActualSize { get; }

        void Update();

        void Meassure();

        void Arrange(Size size);

        ConsoleBitmap Render();
    }
}
