using System;

namespace JPV.RocketScience
{
    public static class HohmannTransfer
    {
        public static double GetDeltaVelocity(
            double primaryGravitationalModifier,
            Target origin,
            Target destination)
        {
            var transferSemiMajorAxis = (origin.Orbit + destination.Orbit) / 2;

            var originDeltaVelocity = GetInsertionBurnDeltaVelocity(
                transferSemiMajorAxis,
                primaryGravitationalModifier,
                origin);

            var destinationDeltaVelocity = GetInsertionBurnDeltaVelocity(
                transferSemiMajorAxis,
                primaryGravitationalModifier,
                destination);

            return originDeltaVelocity + destinationDeltaVelocity;
        }

        public static double GetDeltaVelocity(
            double primaryGravitationalModifier,
            Target origin,
            double destinationParkingOrbit)
        {
            var transferSemiMajorAxis = (origin.Orbit + destinationParkingOrbit) / 2;

            var originDeltaVelocity = GetInsertionBurnDeltaVelocity(
                transferSemiMajorAxis,
                primaryGravitationalModifier,
                origin);

            var destinationDeltaVelocity = GetInsertionBurnDeltaVelocity(
                transferSemiMajorAxis,
                primaryGravitationalModifier,
                destinationParkingOrbit);

            return originDeltaVelocity + destinationDeltaVelocity;
        }

        public static double GetDeltaVelocity(
            double gravitationalModifier,
            double originParkingOrbit,
            double destinationParkingOrbit)
        {
            var transferSemiMajorAxis = (originParkingOrbit + destinationParkingOrbit) / 2;

            var originDeltaVelocity = GetInsertionBurnDeltaVelocity(
                transferSemiMajorAxis,
                gravitationalModifier,
                originParkingOrbit);

            var destinationDeltaVelocity = GetInsertionBurnDeltaVelocity(
                transferSemiMajorAxis,
                gravitationalModifier,
                destinationParkingOrbit);

            return originDeltaVelocity + destinationDeltaVelocity;
        }

        public static double GetInsertionBurnDeltaVelocity(double transferSemiMajorAxis, double primaryGravitationalModifier, Target target)
        {
            // The velocity at the apsis of the transfer orbit
            var transferOrbitVelocity = Math.Sqrt(primaryGravitationalModifier * ((2 / target.Orbit) - (1 / transferSemiMajorAxis)));

            // The velocity the body already has
            var bodyOrbitVelocity = Math.Sqrt(primaryGravitationalModifier / target.Orbit);

            // The delta velocity we need to get to the speed at the apsis of the transfer orbit
            var insertionBurnDeltaVelocity = Math.Abs(transferOrbitVelocity - bodyOrbitVelocity);

            // The delta velocity wee need to escape the orbited body
            var escapeDeltaVelocity = Math.Sqrt((2 * target.GravitationalModifier) / target.ParkingOrbit);

            // The magic
            var hyperDeltaVelocity = Math.Sqrt(Math.Pow(insertionBurnDeltaVelocity, 2) + Math.Pow(escapeDeltaVelocity, 2));

            // The velocity we already have
            var parkingOrbitVelocity = Math.Sqrt(target.GravitationalModifier / target.ParkingOrbit);

            return Math.Abs(hyperDeltaVelocity - parkingOrbitVelocity);
        }

        public static double GetInsertionBurnDeltaVelocity(double transferSemiMajorAxis, double primaryGravitationalModifier, double targetParkingOrbit)
        {
            // The velocity at the apsis of the transfer orbit
            var transferOrbitVelocity = Math.Sqrt(primaryGravitationalModifier * ((2 / targetParkingOrbit) - (1 / transferSemiMajorAxis)));

            var targetOrbitVelocity = Math.Sqrt(primaryGravitationalModifier / targetParkingOrbit);

            var insertionBurnDeltaVelocity = Math.Abs(transferOrbitVelocity - targetOrbitVelocity);

            return Math.Abs(insertionBurnDeltaVelocity);
        }

        public class Target
        {
            public double GravitationalModifier { get; }

            public double Orbit { get; }

            public double ParkingOrbit { get; }

            public Target(double gravitationalModifier, double orbit, double parkingOrbit)
            {
                this.GravitationalModifier = gravitationalModifier;
                this.Orbit = orbit;
                this.ParkingOrbit = parkingOrbit;
            }
        }
    }
}
