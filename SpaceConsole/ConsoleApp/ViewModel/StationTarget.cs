using System.Collections.Generic;
using System.Linq;
using SpaceConsole.ConsoleApp.Model;
using SpaceConsole.ConsoleApp.Model.Items;

namespace SpaceConsole.ConsoleApp.ViewModel
{
    public sealed class StationTarget
    {
        public Station Station { get; }

        public double Distance { get; }

        public double RequiredDeltaVelocity { get; }
        
        public IReadOnlyCollection<ItemStack> RequiredFuel { get; }

        public StationTarget(Station station, double distance, double requiredDeltaVelocity, IEnumerable<ItemStack> requiredFuel)
        {
            Station = station;
            Distance = distance;
            RequiredDeltaVelocity = requiredDeltaVelocity;
            RequiredFuel = requiredFuel.ToList();
        }
    }
}
