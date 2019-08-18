using System;
using System.Collections.Generic;
using System.Linq;
using SpaceConsole.ConsoleApp.Model.Items;

namespace SpaceConsole.ConsoleApp.Model
{
    public sealed class Inventory
    {
        private List<ItemStack> _itemStacks = new List<ItemStack>();

        public int Space { get; }

        public int UsedSpace => ItemStacks.Sum(item => item.Amount);

        public int FreeSpace => Math.Max(0, Space - UsedSpace);

        public double Mass => ItemStacks.Sum(item => item.Mass);

        public IReadOnlyList<ItemStack> ItemStacks => _itemStacks;

        public Inventory(int space)
        {
            Space = space;
        }

        public bool HasAmount(int requiredAmount, Func<IItem, bool> matchFunc)
        {
            return GetAmount(matchFunc) >= requiredAmount;
        }

        public int GetAmount(IItem item)
        {
            return ItemStacks.Where(itemStack => Equals(itemStack.Item, item)).Sum(i => i.Amount);
        }

        public int GetAmount(Func<IItem, bool> matchFunc)
        {
            return ItemStacks.Where(itemStack => matchFunc(itemStack.Item)).Sum(i => i.Amount);
        }

        public IEnumerable<ItemStack> RemoveAmount(int amountToBeRemoved, IItem item)
        {
            return RemoveAmount(amountToBeRemoved, i => Equals(i, item));
        }

        public IEnumerable<ItemStack> RemoveAmount(int amountToBeRemoved, Func<IItem, bool> matchFunc)
        {
            IEnumerable<ItemStack> removedStacks = new List<ItemStack>();

            _itemStacks = _itemStacks
                .Select(stack =>
                {
                    if (amountToBeRemoved == 0 || !matchFunc(stack.Item))
                        return stack;

                    var removedAmount = Math.Min(amountToBeRemoved, stack.Amount);

                    stack.Split(removedAmount, out var remainingStack, out var removedStack);

                    removedStacks = removedStacks.Stack(removedStack);

                    amountToBeRemoved -= removedStack.Amount;

                    return remainingStack;
                })
                .Where(ItemStack.IsNotNullOrEmpty)
                .ToList();

            return removedStacks;
        }

        public bool Add(ItemStack stackToAdd)
        {
            if (UsedSpace + stackToAdd.Amount > Space)
                return false;

            _itemStacks = _itemStacks
                .Stack(stackToAdd)
                .ToList();

            return true;
        }
    }
}