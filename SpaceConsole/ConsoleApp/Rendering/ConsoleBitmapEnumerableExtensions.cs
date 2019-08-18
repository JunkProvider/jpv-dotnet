using System.Collections.Generic;
using System.Linq;

namespace SpaceConsole.ConsoleApp.Rendering
{
    public static class ConsoleBitmapEnumerableExtensions
    {
        public static ConsoleBitmap StackedVertical(this IEnumerable<ConsoleBitmap> bitmaps)
        {
            var bitmapList = bitmaps.ToList();

            var stackedBitmap = ConsoleBitmap.Empty;
            var yOffset = 0;

            foreach (var bitmap in bitmapList)
            {
                stackedBitmap = stackedBitmap.CombinedWith(0, yOffset, bitmap);
                yOffset += bitmap.Height;
            }

            return stackedBitmap;
        }
    }
}
