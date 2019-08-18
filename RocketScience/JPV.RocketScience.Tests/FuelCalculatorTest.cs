using System;
using JPV.RocketScience.FuelCalculation;
using Xunit;

namespace JPV.RocketScience.Tests
{
    public class FuelCalculatorTest
    {
        [Fact]
        public void Test_GetConsumedFuel_WithInsufficientFuel()
        {
            var x = Physics.GetRequiredExhaustMass(9400, 10000, 350 * 9.81);

            Assert.Equal(144518.9, FuelCalculator.GetConsumedFuel(9400, 10000, new FuelTank(0), 350 * 9.81, 0.1), 1);
            Assert.Equal(144518.9, FuelCalculator.GetConsumedFuel(9400, 10000, new FuelTank(1), 350 * 9.81, 0.1), 1);
            Assert.Equal(144518.9, FuelCalculator.GetConsumedFuel(9400, 10000, new FuelTank(10000), 350 * 9.81, 0.1), 1);
            Assert.Equal(144518.9, FuelCalculator.GetConsumedFuel(9400, 10000, new FuelTank(100000), 350 * 9.81, 0.1), 1);
        }

        [Fact]
        public void Test_GetConsumedFuel_WithExactFuel()
        {
            var exhaustMass = FuelCalculator.GetConsumedFuel(9400, 10000, new FuelTank(144518.9), 350 * 9.81, 0.1);

            Assert.Equal(144518.9, exhaustMass, 1);
        }

        [Fact]
        public void Test_GetConsumedFuel_WithExcessFuel()
        {
            var exhaustMass = FuelCalculator.GetConsumedFuel(9400, 5000, new FuelTank(144518.9), 350 * 9.81, 0.1);

            Assert.Equal(139842.3, exhaustMass, 1);
        }

        [Fact]
        public void Test_GetConsumedFuel_WithZeroDeltaVelocity()
        {
            var exhaustMass = FuelCalculator.GetConsumedFuel(0, 10000, new FuelTank(144518.9), 350 * 9.81, 0.1);

            Assert.Equal(0, exhaustMass, 1);
        }

        [Fact]
        public void Test_GetConsumedFuel_WithZeroDryMass()
        {
            var exhaustMass = FuelCalculator.GetConsumedFuel(9400, 0, new FuelTank(144518.9), 350 * 9.81, 0.1);

            Assert.Equal(135166.2, exhaustMass, 1);
        }
        
        [Fact]
        public void Test_GetConsumedFuel_WithZeroExhaustVelocity()
        {
            var exhaustMass = FuelCalculator.GetConsumedFuel(9400, 10000, new FuelTank(144518.9), 0, 0.1);

            Assert.Equal(double.PositiveInfinity, exhaustMass, 1);
        }
       
        [Fact]
        public void Test_GetConsumedFuel_WithPartialExcessFuelAndPartialInsuffiecientFuel()
        {
            var fuelTank = new FuelTank(
                new FuelComponent(0.5, 100000),
                new FuelComponent(0.5, 0));

            var dryMass = 10000;
            var fuelAMass = 100000;
            var exhaustVelocity = 350 * 9.81;
            var deltaVelocity = 9400d;

            var exhaustMass = FuelCalculator.GetConsumedFuel(
                deltaVelocity,
                dryMass,
                fuelTank,
                exhaustVelocity,
                0.01);

            var expectedBurnedFuelMass = 193256d;
            
            var expectedBurnedFuelAMass = expectedBurnedFuelMass / 2;

            var expectedExceedingFuelAMass = fuelAMass - expectedBurnedFuelAMass;

            var confirmedDeltaVelocity = Physics.GetDeltaVelocityBudget(
                dryMass + expectedExceedingFuelAMass,
                exhaustMass,
                exhaustVelocity);

            Assert.Equal(deltaVelocity, confirmedDeltaVelocity, 0);

            Assert.True(Math.Abs(expectedBurnedFuelMass - exhaustMass) < expectedBurnedFuelMass / 1000);
        }

        
    }
}
