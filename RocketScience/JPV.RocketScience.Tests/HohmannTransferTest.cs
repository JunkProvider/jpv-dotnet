using System.Diagnostics.CodeAnalysis;
using Xunit;

namespace JPV.RocketScience.Tests
{
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public class HohmannTransferTest
    {
        [Fact]
        public void TestFromEarth300kmToMars300km()
        {
            var deltaVelocity = HohmannTransfer.GetDeltaVelocity(
                primaryGravitationalModifier: 1.32715e20,
                origin: new HohmannTransfer.Target(
                    gravitationalModifier: 3.9857e14,
                    orbit: 1.496e11,
                    parkingOrbit: 300000 + 6.3710e6), 
                destination: new HohmannTransfer.Target(
                    gravitationalModifier: 4.282e13,
                    orbit: 2.280e11,
                    parkingOrbit: 300000 + 3.3895e6));

            Assert.Equal(5684, deltaVelocity, 0);
        }

        [Fact]
        public void TestFromEarth300kmToMarsOrbit()
        {
            var deltaVelocity = HohmannTransfer.GetDeltaVelocity(
                primaryGravitationalModifier: 1.32715e20,
                origin: new HohmannTransfer.Target(
                    gravitationalModifier: 3.9857e14,
                    orbit: 1.496e11,
                    parkingOrbit: 300000 + 6.3710e6),
                destinationParkingOrbit: 2.280e11);

            Assert.Equal(6242, deltaVelocity, 0);
        }

        [Fact]
        public void TestFromEarth300kmToEarth500km()
        {
            var deltaVelocity = HohmannTransfer.GetDeltaVelocity(
                gravitationalModifier: 3.9857e14,
                originParkingOrbit: 300000 + 6.3710e6,
                destinationParkingOrbit: 500000 + 6.3710e6);

            Assert.Equal(113, deltaVelocity, 0);
        }

        [Fact]
        public void TestFromEarth300kmToEarth8000km()
        {
            var deltaVelocity = HohmannTransfer.GetDeltaVelocity(
                gravitationalModifier: 3.9857e14,
                originParkingOrbit: 300000 + 6.3710e6,
                destinationParkingOrbit: 8000000 + 6.3710e6);

            Assert.Equal(2377, deltaVelocity, 0);
        }

        [Fact]
        public void TestFromEarth300kmToEarth1024000km()
        {
            var deltaVelocity = HohmannTransfer.GetDeltaVelocity(
                gravitationalModifier: 3.9857e14,
                originParkingOrbit: 300000 + 6.3710e6,
                destinationParkingOrbit: 1024000000 + 6.3710e6);

            Assert.Equal(3718, deltaVelocity, 0);
        }
    }
}
