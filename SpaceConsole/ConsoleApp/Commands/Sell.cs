using SpaceConsole.ConsoleApp.Model;
using SpaceConsole.ConsoleApp.ViewModel;

namespace SpaceConsole.ConsoleApp.Commands
{
    public sealed class Sell
    {
        private IngameModel ViewModel { get; }

        private Ship Ship => ViewModel.Ship;

        private Station Station => Ship.CurrentStation;

        public Sell(IngameModel viewModel)
        {
            ViewModel = viewModel;
        }

        public void Execute(int amount, string itemName)
        {
            if (Station == null)
            {
                ViewModel.SetMessage("Can not sell item. Your ship is not docked to a station.");
                return;
            }

            var requestedItem = Station.RequestedItems.WithMatchingName(itemName);

            if (requestedItem == null)
            {
                ViewModel.SetMessage($"Can not sell item. Marketplace does not buy \"{itemName}\".");
                return;
            }

            if (amount > requestedItem.ItemStack.Amount)
            {
                ViewModel.SetMessage($"Can not sell {amount.ToStringInvariant()} {itemName}. Marketplace only wants {requestedItem.ItemStack.Amount} {itemName}.");
                return;
            }

            var amountOnShip = Ship.CargoBay.GetAmount(requestedItem.ItemStack.Item);

            if (amountOnShip < amount)
            {
                ViewModel.SetMessage($"Can not sell {amount.ToStringInvariant()} {itemName}. You have only {requestedItem.ItemStack.Amount} {itemName}.");
                return;
            }

            var totalPrice = requestedItem.Price * amount;

            Ship.CargoBay.RemoveAmount(amount, requestedItem.ItemStack.Item);

            Station.RemoveRequestedItemAmount(requestedItem, amount);

            Ship.Credits += amount * requestedItem.Price;

            ViewModel.SetMessage($"Sold {amount.ToStringInvariant()} {requestedItem.Name} for ${totalPrice.ToStringInvariant()}. Your ships cargo bay has now {Ship.CargoBay.FreeSpace} free space left.");
        }
    }
}