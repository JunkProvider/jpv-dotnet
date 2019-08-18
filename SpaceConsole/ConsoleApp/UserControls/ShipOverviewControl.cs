using System;
using System.Collections.Generic;
using SpaceConsole.ConsoleApp.Controls;
using SpaceConsole.ConsoleApp.Model;
using SpaceConsole.ConsoleApp.Quantities;

namespace SpaceConsole.ConsoleApp.UserControls
{
    public sealed class ShipOverviewControl : UserControl<IControl>, IUserControl
    {
        private Ship Ship { get; }

        public ShipOverviewControl(Ship ship)
        {
            Ship = ship;

            Content = Factory.StackPanel(new List<IControl>
            {
                Factory.Headline1("SHIP OVERVIEW"),
                Factory.Box(
                    content: Factory.DefinitionList(new Dictionary<string, Func<string>>
                    {
                        {"Location:   ", () => Ship.CurrentStation?.Name ?? "Deep Space"},
                        // {"Crew:   ", () => Ship.Crew.ToStringInvariant()},
                        {"Credits:   ", () => $"${Ship.Credits.ToStringInvariant()}"},
                        {"", () => ""},
                        // {"Power Usage:   ", () => $"{Ship.PowerUsage.ToStringInvariant()} / {Ship.PowerOutput.ToStringInvariant()}"}
                        {"Mass:   ", () => $"{(Ship.Mass.InTons()).ToStringInvariant()} t"}
                    })
                )
            });
        }
    }
}