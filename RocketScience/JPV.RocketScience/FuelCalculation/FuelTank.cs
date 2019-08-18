using System;
using System.Collections.Generic;
using System.Linq;

namespace JPV.RocketScience.FuelCalculation
{
    public class FuelTank
    {
        public IReadOnlyCollection<FuelComponent> Components { get; }

        public FuelTank(double fuelMass)
            : this(new FuelComponent(1, fuelMass))
        {
        }

        public FuelTank(params FuelComponent[] components)
        {
            Components = components;
        }

        public FuelTank(IReadOnlyCollection<FuelComponent> components)
        {
            Components = components;
        }

        public double GetAvailableMass()
        {
            return Components.Sum(c => c.AvailableMass);
        }

        public bool CanBeFullyBurned()
        {
            return Equals(GetBurnableMass(), GetAvailableMass());
        }

        public double GetBurnableMass()
        {
            return Components.Min(c => c.AvailableMass / c.Portion);
        }

        public double GetExcessMass()
        {
            return GetExcessMass(GetBurnableMass());
        }

        public double GetExcessMass(double exhaustedMass)
        {
            return Components.Sum(c => Math.Max(0, c.AvailableMass - (exhaustedMass * c.Portion)));
        }
    }
}