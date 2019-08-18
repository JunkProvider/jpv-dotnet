using SpaceConsole.ConsoleApp.UserControls;
using SpaceConsole.ConsoleApp.ViewModel;

namespace SpaceConsole.ConsoleApp.Commands
{
    public sealed class Goto
    {
        private IngameModel IngameModel { get; }

        public Goto(IngameModel ingameModel)
        {
            IngameModel = ingameModel;
        }

        public void Execute(string destination)
        {
            var route = MapRoute(destination);

            if (!route.HasValue)
            {
                IngameModel.SetMessage($"Did not find {destination}");
                return;
            }

            IngameModel.UserInterfaceRoute = route.Value;
        }

        private static UserInterfaceRoute? MapRoute(string input)
        {
            input = input.Trim().ToLowerInvariant();

            switch (input)
            {
                case "overview":
                case "o":
                    return UserInterfaceRoute.ShipOverview;
                case "cargobay":
                case "cargo bay":
                case "cargo":
                case "c":
                    return UserInterfaceRoute.CargoBay;
                case "navigationroom":
                case "navigation room":
                case "navroom":
                case "nav room":
                case "nav":
                case "n":
                    return UserInterfaceRoute.NavigationRoom;
                case "marketplace":
                case "market":
                case "m":
                case "shop":
                    return UserInterfaceRoute.Marketplace;
                case "engine":
                case "e":
                    return UserInterfaceRoute.Engine;
                default:
                    return null;
            }
        }
    }
}

