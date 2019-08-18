using SpaceConsole.ConsoleApp.Model;
using SpaceConsole.ConsoleApp.ViewModel;

namespace SpaceConsole.ConsoleApp.Commands
{
    public sealed class Buy
    {
        private IngameModel ViewModel { get; }

        private Ship Ship => ViewModel.Ship;

        private Station Station => Ship.CurrentStation;

        public Buy(IngameModel viewModel)
        {
            ViewModel = viewModel;
        }

        public void Execute(int amount, string itemName)
        {
            if (Station == null)
            {
                ViewModel.SetMessage("Can not buy item. Your ship is not docked to a station.");
                return;
            }

            var offeredItem = Station.OfferedItems.WithMatchingName(itemName);

            if (offeredItem == null)
            {
                ViewModel.SetMessage($"Can not buy item. Marketplace does not sell \"{itemName}\".");
                return;
            }

            if (amount > offeredItem.ItemStack.Amount)
            {
                ViewModel.SetMessage($"Can not buy {amount.ToStringInvariant()} {itemName}. Marketplace has only {offeredItem.ItemStack.Amount} {itemName}.");
                return;
            }

            if (amount > Ship.CargoBay.FreeSpace)
            {
                ViewModel.SetMessage($"Can not buy {amount.ToStringInvariant()} {itemName}. Your ships cargo bay has only {Ship.CargoBay.FreeSpace} free space.");
                return;
            }

            var totalPrice = offeredItem.Price * amount;

            if (totalPrice > Ship.Credits)
            {
                ViewModel.SetMessage($"Can not buy {amount.ToStringInvariant()} {itemName} for ${totalPrice.ToStringInvariant()}. You only have ${Ship.Credits.ToStringInvariant()}.");
                return;
            }

            offeredItem.ItemStack.Split(amount, out var remainingStack, out var boughtStack);

            if (remainingStack != null)
            {
                offeredItem.ItemStack = remainingStack;
            }
            else
            {
                Station.OfferedItems.Remove(offeredItem);
            }

            if (boughtStack != null)
            {
                Ship.CargoBay.Add(boughtStack);
                Ship.Credits -= boughtStack.Amount * offeredItem.Price;
                ViewModel.SetMessage($"Bought {amount.ToStringInvariant()} {boughtStack.Name} for ${totalPrice.ToStringInvariant()}. Your ships cargo bay has now {Ship.CargoBay.FreeSpace} free space left.");
                return;
            }

            ViewModel.SetMessage($"Could not buy {amount.ToStringInvariant()} {offeredItem.Name}. Something went wrong.");
        }
    }
}