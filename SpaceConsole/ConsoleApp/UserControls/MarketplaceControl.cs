using System;
using System.Collections.Generic;
using SpaceConsole.ConsoleApp.Controls;
using SpaceConsole.ConsoleApp.Model;
using SpaceConsole.ConsoleApp.ViewModel;

namespace SpaceConsole.ConsoleApp.UserControls
{
    public sealed class MarketplaceControl : UserControl<IControl>, IUserControl
    {
        private IngameModel ViewModel { get; }

        private Ship Ship => ViewModel.Ship;

        public MarketplaceControl(IngameModel viewModel)
        {
            ViewModel = viewModel;

            Content = Factory.StackPanel(new List<IControl>
            {
                Factory.Headline1(() => $"{Ship.CurrentStation.Name} - MARKETPLACE"),
                Factory.Box(
                    content: Factory.StackPanel(new List<IControl>
                    {
                        Factory.Text(() => $"Credits: ${Ship.Credits.ToStringInvariant()}   Free Space: {Ship.CargoBay.FreeSpace}"),
                        Factory.Text(() => string.Empty),
                        Factory.HorizintalSplit(
                            Factory.Table(() => Ship.CurrentStation.OfferedItems, new Dictionary<string, Func<MarketplaceItem, string>>
                            {
                                { "Offered Item", item => item.ItemStack.Name },
                                { "Price", item => item.Price.ToStringInvariant() },
                                { "Amount", item => item.ItemStack.Amount.ToStringInvariant() }
                            }, "No items offered"),
                            50,
                            Factory.Table(() => Ship.CurrentStation.RequestedItems, new Dictionary<string, Func<MarketplaceItem, string>>
                            {
                                { "Requested Item", item => item.ItemStack.Name },
                                { "Price", item => item.Price.ToStringInvariant() },
                                { "Amount", item => item.ItemStack.Amount.ToStringInvariant() }
                            }, "No items requested"))
                    })
                )
            });
        }

        protected override void DoUpdate()
        {
            if (Ship.CurrentStation == null)
                return;

            base.DoUpdate();
        }
    }
}