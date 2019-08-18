using System;
using System.Collections.Generic;
using System.Linq;
using SpaceConsole.ConsoleApp.Model.Items;

namespace SpaceConsole.ConsoleApp.Model.Parts
{
    public static class EngineExtensions
    {
        public static IEnumerable<ItemStack> GetAvailableFuel(this IEngine engine, Inventory fuelTank)
        {
            return engine.FuelComponents
                .SelectMany(fuelDescription => fuelTank.ItemStacks.Where(itemStack => Equals(itemStack.Item, fuelDescription.Item)))
                .ReduceIgnoringMaxStackSize();
        }

        public static IEnumerable<ItemStack> GetBurnableFuel(this IEngine engine, Inventory fuelTank)
        {
            var maxBurnCycles = engine.GetMaxBurnCycles(fuelTank);

            return engine.FuelComponents
                .Select(fuelDescription => new ItemStack(fuelDescription.Item, maxBurnCycles * fuelDescription.Amount));
        }

        public static IEnumerable<ItemStack> GetRequiredFuel(this IEngine engine, double exhaustMass)
        {
            var requiredBurnCycles = engine.CalculateBurnCycles(exhaustMass);

            return engine.FuelComponents
                .Select(fuelDescription => new ItemStack(fuelDescription.Item, requiredBurnCycles * fuelDescription.Amount));
        }

        public static int GetMaxBurnCycles(this IEngine engine, Inventory fuelTank)
        {
            return engine.FuelComponents
                .Select(fuelDescription =>
                {
                    var amount = fuelTank.ItemStacks
                        .Where(itemStack => Equals(itemStack.Item, fuelDescription.Item))
                        .TotalAmount();

                    return amount / fuelDescription.Amount;
                })
                .MinOrDefault();
        }

        public static IEnumerable<ItemStack> ConsumeFuel(this IEngine engine, Inventory fuelTank, double burnedMass)
        {
            var burnCycles = engine.CalculateBurnCycles(burnedMass);

            return engine.FuelComponents
                .SelectMany(fuelDescription => fuelTank.RemoveAmount(burnCycles * fuelDescription.Amount, item => Equals(item, fuelDescription.Item)))
                .ReduceIgnoringMaxStackSize();
        }

        private static int CalculateBurnCycles(this IEngine engine, double burnedMass)
        {
            return (int)(Math.Ceiling(burnedMass / engine.FuelComponents.TotalMass()) + 0.5);
        }
    }
}