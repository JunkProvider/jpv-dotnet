using System.Collections.Generic;
using System.Linq;

namespace SpaceConsole.ConsoleApp.Controls
{
    public static class DimensionEnumerableExtensions
    {
        public static Dimension Max(this IEnumerable<Dimension> dimensions)
        {
            var definedDimensions = dimensions.ToList();

            if (definedDimensions.Count == 0)
                return Dimension.Zero;

            return new Dimension(definedDimensions
                .Select(dimension => dimension.Value)
                .Max());
        }

        public static Dimension Min(this IEnumerable<Dimension> dimensions)
        {
            var definedDimensions = dimensions.ToList();

            if (definedDimensions.Count == 0)
                return Dimension.Zero;

            return new Dimension(definedDimensions
                .Select(dimension => dimension.Value)
                .Min());
        }

        public static Dimension Sum(this IEnumerable<Dimension> dimensions)
        {
            var definedDimensions = dimensions
                .ToList();

            if (definedDimensions.Count == 0)
                return Dimension.Zero;

            if (definedDimensions.Any(dimension => dimension.IsInfinite))
                return Dimension.Infinite;

            return new Dimension(definedDimensions
                .Select(dimension => dimension.Value)
                .Sum());
        }
    }
}
