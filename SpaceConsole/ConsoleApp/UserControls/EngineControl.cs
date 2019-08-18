using System;
using System.Collections.Generic;
using System.Linq;
using SpaceConsole.ConsoleApp.Controls;
using SpaceConsole.ConsoleApp.Model;
using SpaceConsole.ConsoleApp.Quantities;

namespace SpaceConsole.ConsoleApp.UserControls
{
    public sealed class EngineControl : UserControl<IControl>, IUserControl
    {
        private Ship Ship { get; }

        public EngineControl(Ship ship)
        {
            Ship = ship;

            Content = Factory.StackPanel(new List<IControl>
            {
                Factory.Headline1("ENGINE"),
                Factory.Box(
                    content: Factory.DefinitionList(new Dictionary<string, Func<string>>
                    {
                        { "Fuel Type:   ", () => string.Join(", ", Ship.Engine.FuelComponents.Select(fuelComponent => $"{fuelComponent.Amount.ToStringInvariant()} {fuelComponent.Name}")) },
                        { "Specific Impulse:   ", () => $"{(Ship.Engine.ExhaustVelocity.InMeterPerSecond()).ToStringInvariant()} m/s" },
                        { "Mass:   ", () => $"{(Ship.Engine.Mass.InTons()).ToStringInvariant()} t" },
                        { "", () => "" },
                        { "Fuel Available:   ", () => string.Join(", ", Ship.GetAvailableFuel().Select(stack => $"{stack.Amount.ToStringInvariant()} {stack.Name}")) },
                        { "Fuel Burnable:   ", () => string.Join(", ", Ship.GetBurnableFuel().Select(stack => $"{stack.Amount.ToStringInvariant()} {stack.Name}")) },
                        { "Range:   ", () => $"{Ship.GetEngineRange().InAstronomicUnits().ToStringInvariant()} AU" }
                    })
                )
            });
        }
    }
}