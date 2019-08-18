using System.Collections.Generic;
using SpaceConsole.ConsoleApp.Model.Items;

namespace SpaceConsole.ConsoleApp.Model.Parts
{
    public interface IEngine
    {
        string Name { get; }

        double Mass { get; }

        double ExhaustVelocity { get; }

        IReadOnlyCollection<ItemStack> FuelComponents { get; }
    }
}