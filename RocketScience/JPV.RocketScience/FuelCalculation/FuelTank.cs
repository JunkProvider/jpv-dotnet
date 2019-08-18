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
            if (Components.Count == 0)
                return 0;

            return Components.Sum(c => c.AvailableMass);
        }

        public bool CanBeFullyBurned()
        {
            return Equals(GetBurnableMass(), GetAvailableMass());
        }

        public double GetBurnableMass()
        {
            if (Components.Count == 0)
                return 0;

            return Components.Min(c => c.AvailableMass / c.Portion);
        }

        public double GetExcessMass()
        {
            return GetExcessMass(GetBurnableMass());
        }

        public double GetExcessMass(double exhaustedMass)
        {
            if (Components.Count == 0)
                return 0;

            return Components.Sum(c => Math.Max(0, c.AvailableMass - (exhaustedMass * c.Portion)));
        }
    }
}