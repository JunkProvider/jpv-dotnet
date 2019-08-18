using System;
using SpaceConsole.ConsoleApp.Model;
using SpaceConsole.ConsoleApp.ViewModel;

namespace SpaceConsole.ConsoleApp.Commands
{
    public sealed class MoveTo
    {
        private IngameModel ViewModel { get; }

        private World World => ViewModel.World;

        private Ship Ship => World.Ship;

        public MoveTo(IngameModel viewModel)
        {
            ViewModel = viewModel;
        }

        public void Execute(string targetName)
        {
            var target = World.Stations.WithMatchingName(targetName);

            if (target == null)
            {
                ViewModel.SetMessage($"{targetName} is not an available target.");
                return;
            }

            switch (Ship.CanNavigateTo(target))
            {
                case Ship.CanNavigateToResult.No no:
                    ViewModel.SetMessage($"Can not navigate to {target.Name}. {no.Reason}");
                    break;
                case Ship.CanNavigateToResult.Yes _:
                    var consumedFuel = Ship.ConsumeFuel(target);
                    var consumedListing = TextHelper.JoinToListing(consumedFuel, stack => $"{stack.Amount} {stack.Name}", "no fuel");
                    ViewModel.SetMessage($"Navigation set to to {target.Name}. Burned {consumedListing}.");
                    Ship.CurrentStation = target;
                    break;
                default:
                    throw new InvalidOperationException();
            }
        }
    }
}
