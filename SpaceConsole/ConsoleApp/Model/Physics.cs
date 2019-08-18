/*using System;

namespace SpaceConsole.ConsoleApp.Model
{
    public static class Physics
    {
        public const double GravitationalConstant = 0.0001;

        public static double GetOptimumExhaustMass(double deltaVelocity, double minEmptyMass, double maxExhaustMass, double exhaustVelocity, double deltaVelocityTolerance = 0.1, int maxIterations = 100)
        {
            // Nothing to be accelerated
            if (deltaVelocity <= 0 || minEmptyMass <= 0)
                return 0;

            // No acceleration possible
            if (exhaustVelocity <= 0)
                return double.PositiveInfinity;

            var deltaVelocityBudget = GetMaxDeltaVelocity(minEmptyMass, maxExhaustMass, exhaustVelocity);

            // Not enough exhaust mass to reach delta velocity
            // TODO: minEmptyMass can be wrong in this case if the ship alredy has part of the fuel (e.g: only hydrogen no oxygen) which is considered as dry mass
            // Example 1: Ship requires 50kg of A and 50kg of B and already contains 50kg of A (exact anoutn of A, not enough of B).
            // Example 2: Ship requires 50kg of A and 50kg of B and already contains 100kg of A (too much of A, not enugh of B).
            if (deltaVelocityBudget < deltaVelocity)
                return GetExhaustMass(deltaVelocity, minEmptyMass, exhaustVelocity);

            // Complete exhaust mass required to reach delta velocity
            if (Math.Abs(deltaVelocityBudget - deltaVelocity) < deltaVelocityTolerance)
                return maxExhaustMass;

            // Bisect to find best minimum required exhaust mass to reach desired velocity
            var optimumExhaustMassRatio = Bisect(ratio =>
            {
                var exhaustMass = maxExhaustMass * ratio;

                // Check how fast we can get with this amount of fuel burned
                var emptyMass = minEmptyMass + (maxExhaustMass - exhaustMass);
                deltaVelocityBudget = GetMaxDeltaVelocity(emptyMass, exhaustMass, exhaustVelocity);

                var velocityDiff = deltaVelocityBudget - deltaVelocity;

                // We found the minimum required exhaust mass (with tolerance)
                if (Math.Abs(velocityDiff) < deltaVelocityTolerance)
                    return BisectReturn.Match;

                return velocityDiff < 0
                    ? BisectReturn.TooLow
                    : BisectReturn.TooHigh;
            });

            return maxExhaustMass * optimumExhaustMassRatio;
        }

        private static double Bisect(Func<double, BisectReturn> func, int maxIterations = 100)
        {
            var lowRatio = 0.00;
            var highRatio = 1.00;
            var ratio = double.NaN;

            for (var i = 0; i < maxIterations; i++)
            {
                ratio = lowRatio + ((highRatio - lowRatio) / 2);

                var iterationResult = func(ratio);

                if (iterationResult == BisectReturn.Match)
                    break;

                if (iterationResult == BisectReturn.TooHigh)
                {
                    highRatio = ratio;
                }
                else
                {
                    lowRatio = ratio;
                }
            }

            return ratio;
        }

        public enum BisectReturn
        {
            Match,
            TooLow,
            TooHigh
        }

        public class FuelTank
        {

        }

        public class FuelComponent
        {
            public double MinMass { get; }

            public double Ratio { get; }
        }

        /// <summary>
        /// Gets the exhaust mass required to accelerate the <see cref="emptyMass"/> to <see cref="deltaVelocity"/> with the given <see cref="exhaustVelocity"/>.
        /// </summary>
        /// <param name="deltaVelocity">in m/s</param>
        /// <param name="emptyMass">in kg</param>
        /// <param name="exhaustVelocity">in m/s (CAUTION: Isp often is given in seconds and has to be multiplied by 9.81 m/s²!)</param>
        /// <returns>Exhaust mass in kg</returns>
        public static double GetExhaustMass(double deltaVelocity, double emptyMass, double exhaustVelocity)
        {
            // See: https://wiki.kerbalspaceprogram.com/wiki/Tutorial:Advanced_Rocket_Design

            // Equivalent to:
            // return massEmpty / (Math.Exp(- (deltaVelocity / exhaustVelocity))) - massEmpty;

            return (emptyMass * Math.Exp(deltaVelocity / exhaustVelocity)) - emptyMass;
        }

        /// <summary>
        /// Gets the max delta velocity the <see cref="emptyMass"/> can be accelerated to with the given <see cref="exhaustMass"/> and <see cref="exhaustVelocity"/>.
        /// </summary>
        /// <param name="emptyMass">in kg</param>
        /// <param name="exhaustMass">in kg</param>
        /// <param name="exhaustVelocity">in m/s (CAUTION: Isp often is given in seconds and has to be multiplied by 9.81 m/s²!)</param>
        /// <returns>Max delta velocity in m/s</returns>
        public static double GetMaxDeltaVelocity(double emptyMass, double exhaustMass, double exhaustVelocity)
        {
            return exhaustVelocity * Math.Log((emptyMass + exhaustMass) / emptyMass);
        }

        /// <param name="centralMass">in kg</param>
        /// <param name="distance">in m</param>
        /// <param name="gravitationalConstant">in m³/(kg * s²)</param>
        /// <returns>Orbit velocity in m/s</returns>
        public static double GetOrbitVelocity(double centralMass, double distance, double gravitationalConstant)
        {
            return Math.Sqrt(GetStandardGravitationalParameter(centralMass, gravitationalConstant) / distance);
        }

        /// <param name="centralMass">in kg</param>
        /// <param name="gravitationalConstant">in m³/(kg * s²)</param>
        /// <returns>Standard gravitational parameter in m³/s²</returns>
        public static double GetStandardGravitationalParameter(double centralMass, double gravitationalConstant)
        {
            return gravitationalConstant * centralMass;
        }
    }
}
*/