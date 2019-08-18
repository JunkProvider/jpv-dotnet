using System;
using System.Collections.Generic;
using System.Linq;
using SpaceConsole.ConsoleApp.Model;
using SpaceConsole.ConsoleApp.Model.Items;

namespace SpaceConsole.ConsoleApp.ViewModel
{
    public sealed class NavigationRoom
    {
        private List<StationTarget> _targets = new List<StationTarget>();

        public Station CurrentStation { get; private set; }

        public IReadOnlyCollection<ItemStack> AvailableFuel { get; private set; } 

        public IReadOnlyList<StationTarget> Targets => _targets;

        public void Update(IEnumerable<Station> stations, Ship ship)
        {
            CurrentStation = ship.CurrentStation;

            AvailableFuel = ship.GetAvailableFuel().ToList();

            if (CurrentStation == null)
            {
                _targets.Clear();
                return;
            }

            var deltaVelocityBudget = ship.GetDeltaVelocityBudget();

            _targets = stations
                .Where(station => station != CurrentStation)
                .Select(station =>
                {
                    var distance = Math.Abs(ship.CurrentStation.CelestialSystem.Orbit - station.CelestialSystem.Orbit);
                    var requiredDeltaVelocity = ship.GetRequiredDeltaVelocity(station);
                    var requiredFuel = ship.GetRequiredFuel(requiredDeltaVelocity);
                    return new StationTarget(station, distance, requiredDeltaVelocity: requiredDeltaVelocity, requiredFuel: requiredFuel);
                })
                .Where(target => target.Distance <= ship.NavigationRange)
                .OrderBy(target => target.Distance)
                .ToList();
        }
    }
}
