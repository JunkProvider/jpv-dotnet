using System;
using System.Collections.Generic;
using System.Linq;
using SpaceConsole.ConsoleApp.Controls;
using SpaceConsole.ConsoleApp.Quantities;
using SpaceConsole.ConsoleApp.ViewModel;

namespace SpaceConsole.ConsoleApp.UserControls
{
    public sealed class NavigationRoomControl : UserControl<IControl>, IUserControl
    {
        private const string DeepSpaceText = "Deep Space";

        private NavigationRoom ViewModel { get; }

        public NavigationRoomControl(NavigationRoom viewModel)
        {
            ViewModel = viewModel;

            Content = Factory.StackPanel(new List<IControl>
            {
                Factory.Headline1("NAVIGATION ROOM"),
                Factory.Box(
                    content: Factory.StackPanel(new List<IControl>
                    {
                        Factory.DefinitionList(new Dictionary<string, Func<string>>
                        {
                            { "Location:   ", () => ViewModel.CurrentStation?.Name ?? DeepSpaceText },
                            { "Available Fuel:   ", GetAvailableFuelText },
                        }),
                        Factory.Text(() => string.Empty),
                        Factory.Box(
                            content: Factory.Table(
                                () => ViewModel.Targets,
                                new Dictionary<string, Func<StationTarget, string>>
                                {
                                    { "Station", target => target.Station.Name },
                                    { "Distance", target => $"{target.Distance.InAstronomicUnits().ToStringInvariant()} AU" },
                                    { "Req. Delta V", target => $"{target.RequiredDeltaVelocity.InMeterPerSecond().ToStringInvariant()} m/s" },
                                    { "Req. Fuel", target => string.Join(", ", target.RequiredFuel.Select(stack => $"{stack.Amount} {stack.Name}")) }
                                },
                                "No destinations in range")
                        )
                    })
                )
            });
        }

        private string GetAvailableFuelText()
        {
            return ViewModel.AvailableFuel.Count == 0 ? "none" : string.Join(", ", ViewModel.AvailableFuel);
        }
    }
}