using System.Collections.Generic;
using System.Linq;

namespace SpaceConsole.ConsoleApp.Model.Items
{
    public static class ItemStackEnumerableExtensions
    {
        public static int TotalAmount(this IEnumerable<ItemStack> stacks)
        {
            return stacks.Sum(stack => stack.Amount);
        }

        public static double TotalMass(this IEnumerable<ItemStack> stacks)
        {
            return stacks.Sum(stack => stack.Mass);
        }

        public static IEnumerable<ItemStack> Stack(this IEnumerable<ItemStack> stacks, ItemStack stackToAdd)
        {
            return stacks
                .Select(stack =>
                {
                    if (ItemStack.IsNullOrEmpty(stackToAdd) || !Equals(stack.Item, stackToAdd.Item))
                        return stack;

                    stack.Merge(stackToAdd, out var mergedStack, out stackToAdd);

                    return mergedStack;
                })
                .AppendOptional(() => ItemStack.IsNullOrEmpty(stackToAdd)
                    ? Optional.Empty<ItemStack>()
                    : Optional.Create(stackToAdd));
        }

        /// <summary>
        /// Combines all stacks of the same item type to a single stack.
        /// </summary>
        public static IEnumerable<ItemStack> ReduceIgnoringMaxStackSize(this IEnumerable<ItemStack> stacks)
        {
            return stacks
                .GroupBy(stack => stack.Item)
                .Select(stackGroup => new ItemStack(stackGroup.Key, stackGroup.Sum(stack => stack.Amount)));
        }
    }
}
