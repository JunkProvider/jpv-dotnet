using System;

namespace SpaceConsole.ConsoleApp.Model.Items
{
    public sealed class ItemStack : INamed
    {
        public static bool IsNotNullOrEmpty(ItemStack itemStack)
        {
            return !IsNullOrEmpty(itemStack);
        }

        public static bool IsNullOrEmpty(ItemStack itemStack)
        {
            return itemStack == null || itemStack.Amount == 0;
        }

        public IItem Item { get; }

        public string Name => Item.Name;

        public int Amount { get; }

        public double Mass => Item.Mass * Amount;

        public ItemStack(IItem item, int amount)
        {
            Item = item;
            Amount = amount;
        }

        public void Split(int takenAmount, out ItemStack remaining, out ItemStack taken)
        {
            if (takenAmount < 0 || takenAmount > Amount)
                throw new ArgumentOutOfRangeException(nameof(takenAmount));

            taken = takenAmount == 0 ? null : new ItemStack(Item, takenAmount);
            remaining = takenAmount == Amount ? null : new ItemStack(Item, Amount - takenAmount);
        }

        public bool Merge(ItemStack addedStack, out ItemStack mergedStack, out ItemStack remainingStack)
        {
            mergedStack = this;
            remainingStack = addedStack;

            if (!Equals(addedStack.Item, Item) || Amount == Item.MaxStackSize)
                return false;

            var mergedAmount = Math.Min(Item.MaxStackSize - Amount, addedStack.Amount);

            mergedStack = new ItemStack(Item, Amount + mergedAmount);

            remainingStack = mergedAmount != addedStack.Amount
                ? new ItemStack(Item, addedStack.Amount - mergedAmount)
                : null;

            return true;
        }

        public ItemStack Put(int amount)
        {
            if (Amount + amount > Item.MaxStackSize)
                throw new ArgumentOutOfRangeException(nameof(amount));

            return new ItemStack(Item, Amount + amount);
        }

        public override string ToString()
        {
            return $"{Amount.ToStringInvariant()} {Item.Name}";
        }
    }
}