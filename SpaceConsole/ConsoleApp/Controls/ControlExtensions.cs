using System.Drawing;

namespace SpaceConsole.ConsoleApp.Controls
{
    public static class ControlExtensions
    {
        public static void Arrange(this IControl control, int width, int height)
        {
            control.Arrange(new Size(width, height));
        }
    }
}