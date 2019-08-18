using System;
using System.Collections.Generic;
using System.Globalization;
using SpaceConsole.ConsoleApp.Controls;
using SpaceConsole.ConsoleApp.Model;
using SpaceConsole.ConsoleApp.Model.Items;
using SpaceConsole.ConsoleApp.Quantities;

namespace SpaceConsole.ConsoleApp.UserControls
{
    public sealed class CargoBayControl : UserControl<IControl>, IUserControl
    {
        private Ship Ship { get; }

        public CargoBayControl(Ship ship)
        {
            Ship = ship;

            Content = Factory.StackPanel(new List<IControl>
            {
                Factory.Headline1("CARGO BAY"),
                Factory.Table(
                    () => Ship.CargoBay.ItemStacks,
                    new Dictionary<string, Func<ItemStack, string>>
                    {
                        {"Amount", stack => stack.Amount.ToStringInvariant()},
                        {"Item", stack => stack.Name},
                        {"Mass", stack => $"{stack.Mass.InTons().ToStringInvariant()}t" }
                    },
                    "Cargo Bay empty")
            });
        }
    }
}
