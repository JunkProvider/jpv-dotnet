using SpaceConsole.ConsoleApp.Model.Items;

namespace SpaceConsole.ConsoleApp.Model
{
    public sealed class MarketplaceItem : INamed
    {
        public ItemStack ItemStack { get; set; }

        public decimal Price { get; }

        public string Name => ItemStack.Name;

        public MarketplaceItem(ItemStack itemStack, decimal price)
        {
            ItemStack = itemStack;
            Price = price;
        }
    }
}
