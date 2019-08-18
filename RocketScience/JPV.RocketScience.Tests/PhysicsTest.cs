using System;
using JPV.RocketScience.FuelCalculation;
using Xunit;

namespace JPV.RocketScience.Tests
{
    public class PhysicsTest
    {
        [Fact]
        public void Test_GetConsumedFuel_WithExactAmount()
        {
            var exhaustMass = Physics.GetRequiredExhaustMass(9400, 10000, 350 * 9.81);

            Assert.Equal(144518.9, exhaustMass, 1);
        }

        [Fact]
        public void Test_GetDeltaVelocityBudget()
        {
            var maxDeltaVelocity = Physics.GetDeltaVelocityBudget(10000, 144518.9, 350 * 9.81);

            Assert.Equal(9400, maxDeltaVelocity, 1);
        }

        [Fact]
        public void Test_GetOrbitVelocity()
        {
            // Data of geo sync orbit
            var gravityConstant = 6.674 * Math.Pow(10, -11);
            var mass = 5.972 * Math.Pow(10, 24);
            var distance = 42157 * Math.Pow(10, 3);

            var velocity = Physics.GetOrbitVelocity(mass, distance, gravityConstant);

            Assert.Equal(3074.8, velocity, 1);
        }
    }
}
