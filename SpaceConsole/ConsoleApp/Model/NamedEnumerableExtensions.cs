using System;
using System.Collections.Generic;
using System.Linq;

namespace SpaceConsole.ConsoleApp.Model
{
    public static class NamedEnumerableExtensions
    {
        public static T WithMatchingName<T>(this IEnumerable<T> items, string name)
            where T : INamed
        {
            var itemList = items.ToList();

            var itemWithExactName =
                itemList.FirstOrDefault(item => item.Name.Equals(name, StringComparison.InvariantCultureIgnoreCase));

            if (itemWithExactName != null)
                return itemWithExactName;

            var itemsStartingWithName = itemList.Where(item =>
                item.Name.StartsWith(name, StringComparison.InvariantCultureIgnoreCase))
                .ToList();

            return itemsStartingWithName.Count == 1
                ? itemsStartingWithName.First()
                : default(T);
        }
    }
}