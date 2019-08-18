using System;
using JPV.Bisect;

namespace JPV.RocketScience.FuelCalculation
{
    public static class FuelCalculator
    {
        /// <summary>
        /// Gets the optimum/required amount of fuel required to accelerate a ship to the specified velocity.
        /// </summary>
        /// <param name="deltaVelocity">in m/s</param>
        /// <param name="dryMass">in kg</param>
        /// <param name="fuelTank"></param>
        /// <param name="exhaustVelocity">in m/s</param>
        /// <param name="deltaVelocityTolerance">in m/s</param>
        /// <returns>The exhaust mass in kg</returns>
        public static double GetConsumedFuel(
            double deltaVelocity,
            double dryMass, 
            FuelTank fuelTank, 
            double exhaustVelocity,
            double deltaVelocityTolerance)
        {
            var burnableFuelMass = fuelTank.GetBurnableMass();

            var deltaVelocityBudget = Physics.GetDeltaVelocityBudget(dryMass, burnableFuelMass, exhaustVelocity);

            var deltaVelocityDiff = deltaVelocityBudget - deltaVelocity;

            if (Math.Abs(deltaVelocityDiff) < deltaVelocityTolerance)
                return burnableFuelMass;

            // We can calculate the exact amount, no need for aproximation
            if (deltaVelocityDiff < 0 && fuelTank.CanBeFullyBurned())
                return Physics.GetRequiredExhaustMass(deltaVelocity, dryMass, exhaustVelocity);

            var minFuelMass = Physics.GetRequiredExhaustMass(deltaVelocity, dryMass, exhaustVelocity);;

            double maxFuelMass;
            if (deltaVelocityDiff < 0)
            {
                maxFuelMass = Physics.GetRequiredExhaustMass(deltaVelocity, dryMass + fuelTank.GetAvailableMass(), exhaustVelocity);
            }
            else
            {
                maxFuelMass = burnableFuelMass;
            }

            return BisectExhaustMass(
                deltaVelocity: deltaVelocity,
                dryMassFunc: exhaustMass => dryMass + fuelTank.GetExcessMass(exhaustMass),
                exhaustVelocity: exhaustVelocity,
                minFuelMass: minFuelMass,
                maxFuelMass: maxFuelMass,
                deltaVelocityTolerance: deltaVelocityTolerance,
                maxIterations: 100);
        }

        private static double BisectExhaustMass(double deltaVelocity, Func<double, double> dryMassFunc, double exhaustVelocity, double minFuelMass, double maxFuelMass, double deltaVelocityTolerance = 0.1, int maxIterations = 100)
        {
            var fuelMassRange = maxFuelMass - minFuelMass;

            var optimumBurnedFuelRatio = Bisector.Find(
                deltaVelocity,
                deltaVelocityTolerance,
                ratio =>
                {
                    var burnedFuelMass = minFuelMass + fuelMassRange * ratio;

                    // Check how fast we can get with this amount of fuel burned
                    var dryMass = dryMassFunc(burnedFuelMass);
                    var deltaVelocityBudget = Physics.GetDeltaVelocityBudget(dryMass, burnedFuelMass, exhaustVelocity);

                    return deltaVelocityBudget;
                }, maxIterations);

            return minFuelMass + optimumBurnedFuelRatio * fuelMassRange;
        }
    }
}
