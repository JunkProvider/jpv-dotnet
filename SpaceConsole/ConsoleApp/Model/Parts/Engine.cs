using System.Collections.Generic;
using System.Linq;
using SpaceConsole.ConsoleApp.Model.Items;

namespace SpaceConsole.ConsoleApp.Model.Parts
{
    public sealed class Engine : IEngine
    {
        public string Name { get; }

        public double Mass { get; }

        public double ExhaustVelocity { get; }

        public IReadOnlyCollection<ItemStack> FuelComponents { get; }

        public Engine(string name, double mass, double specificImpulse, IEnumerable<ItemStack> fuelComponents)
        {
            Name = name;
            Mass = mass;
            ExhaustVelocity = specificImpulse;
            FuelComponents = fuelComponents.ToList();
        }
    }
}