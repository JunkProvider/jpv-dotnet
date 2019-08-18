using System;
using SpaceConsole.ConsoleApp.Quantities;

namespace SpaceConsole.ConsoleApp.Model.Celestials
{
    public static class CelestialMath
    {
        public static double GetMass(double radius, double density)
        {
            return (GetVolume(radius).InCubicMeter() * density.InTonsPerCubicMeter()).Tons();
        }

        public static double GetVolume(double radius)
        {
            return (Math.Pow(radius.InMeter(), 3) * Math.PI * (1.00 / 4.00)).CubicMeter();
        }
    }
}
