using System.Drawing;

namespace SpaceConsole.ConsoleApp.Controls
{
    public static class SizeExtensions
    {
        public static bool IsZero(this Size size)
        {
            return size.Width == 0 || size.Height == 0;
        }
    }
}
