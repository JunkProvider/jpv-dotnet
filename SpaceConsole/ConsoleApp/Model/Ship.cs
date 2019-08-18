using System;
using System.Collections.Generic;
using System.Linq;
using JPV.RocketScience;
using JPV.RocketScience.FuelCalculation;
using SpaceConsole.ConsoleApp.Model.Celestials;
using SpaceConsole.ConsoleApp.Model.Items;
using SpaceConsole.ConsoleApp.Model.Parts;
using SpaceConsole.ConsoleApp.Quantities;

namespace SpaceConsole.ConsoleApp.Model
{
    public sealed class Ship
    {
        public Station CurrentStation { get; set; }

        public Inventory CargoBay { get; } = new Inventory(200);

        public IEngine Engine { get; } = new Engine(
            name: "Pacer",
            mass: 2.000.Tons(),
            specificImpulse: 3500.00.MeterPerSecond(),
            fuelComponents: new[]
            {
                new ItemStack(new LiquidElementItem(Elements.Hydrogen), 2),
                new ItemStack(new LiquidElementItem(Elements.Oxygen), 1)
            });

        public int Crew { get; set; }

        public decimal Credits { get; set; }

        public double PowerOutput { get; set; }

        public double PowerUsage { get; set; }

        public double NavigationRange => 50.000.AstronomicUnits();

        public double ChasisMass => (CargoBay.Space / 20).Tons() /* > 10% of fuel mass if H2 and O2 used */ + 10.Tons() /* Infrastructure and Interior */;

        public double EmptyMass => ChasisMass + Engine.Mass;

        public double Mass => EmptyMass + CargoBay.Mass;

        public CanNavigateToResult CanNavigateTo(Station station)
        {
            if (CurrentStation == null)
                return new CanNavigateToResult.No("Ship is already travelling.");

            var requiredDeltaVelocity = GetRequiredDeltaVelocity(station);

            if (GetDeltaVelocityBudget() < requiredDeltaVelocity)
                return new CanNavigateToResult.No("Ship has not enough fuel.");

            return new CanNavigateToResult.Yes();
        }

        public IEnumerable<ItemStack> GetRequiredFuel(double deltaVelocity)
        {
            var requiredFuelMass = GetRequiredFuelMass(deltaVelocity);
            return Engine.GetRequiredFuel(requiredFuelMass);
        }

        public double GetDeltaVelocityBudget()
        {
            var totalMass = Mass;
            var burnableFuel = Engine.GetBurnableFuel(CargoBay);
            var burnableMass = burnableFuel.TotalMass();
            return Physics.GetDeltaVelocityBudget(totalMass - burnableMass, burnableMass, Engine.ExhaustVelocity);
        }

        public double GetRequiredFuelMass(double deltaVelocity)
        {
            var totalMass = Mass;

            // TODO: Refactor Enginge.Components to be more user friendly, dont do this shit here
            var massPerCycleSum = Engine.FuelComponents.Sum(i => i.Mass);
            var componentsByItems = Engine.FuelComponents.ToDictionary(i => i.Item);
            var fuelTank = new FuelTank(Engine.GetAvailableFuel(CargoBay).Select(i =>
            {
                var portion = componentsByItems[i.Item].Mass / massPerCycleSum;
                return new FuelComponent(portion, i.Mass);
            }).ToList());

            var dryMass = totalMass - fuelTank.GetAvailableMass();

            return FuelCalculator.GetConsumedFuel(deltaVelocity, dryMass, fuelTank, Engine.ExhaustVelocity, 1.MeterPerSecond());
        }

        public double GetRequiredDeltaVelocity(Station destination)
        {
            var primaryGravitySource = CurrentStation.CelestialSystem.Parent.CentralBody;
            var originSystem = CurrentStation.CelestialSystem;
            var destinationSystem = destination.CelestialSystem;

            return HohmannTransfer.GetDeltaVelocity(
                primaryGravitySource.GetGravitationalModifier(),
                new HohmannTransfer.Target(
                    gravitationalModifier: originSystem.CentralBody.GetGravitationalModifier(),
                    orbit: originSystem.Orbit,
                    parkingOrbit: CurrentStation.ParkingOrbit),
                new HohmannTransfer.Target(
                    gravitationalModifier: destinationSystem.CentralBody.GetGravitationalModifier(),
                    orbit: destinationSystem.Orbit,
                    parkingOrbit: destination.ParkingOrbit));
        }

        public IEnumerable<ItemStack> GetAvailableFuel()
        {
            return Engine.GetAvailableFuel(CargoBay);
        }

        public IEnumerable<ItemStack> GetBurnableFuel()
        {
            return Engine.GetBurnableFuel(CargoBay);
        }

        public double GetEngineRange()
        {
            return 66666; //((Engine.GetMaxGeneratedImpulse(CargoBay).InJoule()/ Mass.InKiloGram()) / 1).AstronomicUnits();
        }

        public IEnumerable<ItemStack> ConsumeFuel(Station station)
        {
            if (CurrentStation == null)
                return Enumerable.Empty<ItemStack>();

            var deltaVelocity = GetRequiredDeltaVelocity(station);

            var burnedMass = GetRequiredFuelMass(deltaVelocity);

            return Engine.ConsumeFuel(CargoBay, burnedMass);
        }

        public abstract class CanNavigateToResult
        {
            public sealed class Yes : CanNavigateToResult
            {
            }

            public sealed class No : CanNavigateToResult
            {
                public string Reason { get; }

                public No(string reason)
                {
                    Reason = reason;
                }
            }
        }
    }
}
