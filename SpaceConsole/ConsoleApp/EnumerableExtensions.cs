using System;
using System.Collections.Generic;
using System.Linq;

namespace SpaceConsole.ConsoleApp
{
    public static class EnumerableExtensions
    {
        public static int MinOrDefault(this IEnumerable<int> items, int defaultValue = 0)
        {
            var itemList = items.ToList();

            return itemList.Count != 0 ? itemList.Min() : defaultValue;
        }

        public static IEnumerable<T> AppendOptional<T>(this IEnumerable<T> items, Func<Optional<T>> valueFunc)
        {
            return items.Concat(new OptionalEnumerable<T>(valueFunc));
        }

        public static IEnumerable<T> Append<T>(this IEnumerable<T> items, Func<T> itemToAppend)
        {
            return items.Concat(new OptionalEnumerable<T>(() => new Optional<T>(itemToAppend())));
        }

        public static IEnumerable<T> Except<T>(this IEnumerable<T> items, T excludedItem)
        {
            return items.Except(new[] {excludedItem});
        }
    }
}
