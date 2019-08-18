using System;

namespace JPV.RocketScience
{
    public static class Physics
    {
        /// <summary>
        /// Gets the exhaust mass required to accelerate the <see cref="dryMass"/> to <see cref="deltaVelocity"/> with the given <see cref="exhaustVelocity"/>.
        /// </summary>
        /// <param name="deltaVelocity">in m/s</param>
        /// <param name="dryMass">in kg</param>
        /// <param name="exhaustVelocity">in m/s (CAUTION: Isp often is given in seconds and has to be multiplied by 9.81 m/s²!)</param>
        /// <returns>Exhaust mass in kg</returns>
        public static double GetRequiredExhaustMass(double deltaVelocity, double dryMass, double exhaustVelocity)
        {
            // See: https://wiki.kerbalspaceprogram.com/wiki/Tutorial:Advanced_Rocket_Design

            // Equivalent to:
            // return massEmpty / (Math.Exp(- (deltaVelocity / exhaustVelocity))) - massEmpty;

            return (dryMass * Math.Exp(deltaVelocity / exhaustVelocity)) - dryMass;
        }

        /// <summary>
        /// Gets the max delta velocity the <see cref="dryMass"/> can be accelerated to with the given <see cref="exhaustMass"/> and <see cref="exhaustVelocity"/>.
        /// </summary>
        /// <param name="dryMass">in kg</param>
        /// <param name="exhaustMass">in kg</param>
        /// <param name="exhaustVelocity">in m/s (CAUTION: Isp often is given in seconds and has to be multiplied by 9.81 m/s²!)</param>
        /// <returns>Max delta velocity in m/s</returns>
        public static double GetDeltaVelocityBudget(double dryMass, double exhaustMass, double exhaustVelocity)
        {
            return exhaustVelocity * Math.Log((dryMass + exhaustMass) / dryMass);
        }

        /// <param name="centralMass">in kg</param>
        /// <param name="distance">in m</param>
        /// <param name="gravityConstant">in m³/(kg * s²)</param>
        /// <returns>Orbit velocity in m/s</returns>
        public static double GetOrbitVelocity(double centralMass, double distance, double gravityConstant)
        {
            return Math.Sqrt(GetStandardGravitationalParameter(centralMass, gravityConstant) / distance);
        }

        /// <param name="centralMass">in kg</param>
        /// <param name="gravityConstant">in m³/(kg * s²)</param>
        /// <returns>Standard gravitational parameter in m³/s²</returns>
        public static double GetStandardGravitationalParameter(double centralMass, double gravityConstant)
        {
            return gravityConstant * centralMass;
        }

        /// <param name="radius">in m</param>
        /// <param name="density">in g/cm³</param>
        /// <returns>Mass in kg</returns>
        public static double GetSphereMass(double radius, double density)
        {
            return GetSphereVolume(radius) * density;
        }

        /// <param name="radius">in m</param>
        /// <returns>Volume in m³</returns>
        public static double GetSphereVolume(double radius)
        {
            return Geometry.GetSphereVolume(radius);
        }
    }
}