using System;
using System.Collections.Generic;
using System.Linq;

namespace SpaceConsole.ConsoleApp
{
    public static class TextHelper
    {
        public static string JoinToListing<T>(IEnumerable<T> items, Func<T, string> toStringFunc, string emptyText)
        {
            var itemList = items
                .Select(toStringFunc)
                .ToList();

            switch (itemList.Count)
            {
                case 0:
                    return emptyText;
                case 1:
                    return itemList.First();
                default:
                    return string.Join(", ", itemList.Take(itemList.Count - 1)) + " and " + itemList.Last();
            }
        }
    }
}
