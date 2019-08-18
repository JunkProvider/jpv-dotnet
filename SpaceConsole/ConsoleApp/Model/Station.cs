using System.Collections.Generic;
using SpaceConsole.ConsoleApp.Model.Celestials;
using SpaceConsole.ConsoleApp.Model.Items;

namespace SpaceConsole.ConsoleApp.Model
{
    public sealed class Station : INamed
    {
        public string Name { get; }

        public ICelestialSystem CelestialSystem { get; }

        public double ParkingOrbit { get; }

        public List<MarketplaceItem> OfferedItems { get; set; } = new List<MarketplaceItem>();

        public List<MarketplaceItem> RequestedItems { get; set; } = new List<MarketplaceItem>();

        public Station(string name, ICelestialSystem celestialSystem, double parkingOrbit)
        {
            Name = name;
            CelestialSystem = celestialSystem;
            ParkingOrbit = parkingOrbit;
        }

        public void RemoveRequestedItemAmount(MarketplaceItem item, int amount)
        {
            var index = RequestedItems.FindIndex(i => i == item);

            if (index == -1)
                return;

            var requestedItem = RequestedItems[index];

            if (amount >= requestedItem.ItemStack.Amount)
            {
                RequestedItems.RemoveAt(index);
            }
            else
            {
                RequestedItems[index] = new MarketplaceItem(new ItemStack(requestedItem.ItemStack.Item, requestedItem.ItemStack.Amount - amount), requestedItem.Price);
            }
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
