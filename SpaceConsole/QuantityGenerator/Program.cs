using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;

namespace QuantityGenerator
{
    public static class Program
    {
        static void Main(string[] args)
        {
            var quantities = new List<Quantity>
            {
                new Quantity(
                    "Distance",
                    new List<Unit>
                    {
                        new Unit("Meter", 1),
                        new Unit("Millimeter", 0.001),
                        new Unit("Centimeter", 0.01),
                        new Unit("Decimeter", 0.1),
                        new Unit("Kilometer", 1000),
                        new Unit("AstronomicUnits", 149597870700)
                    }),
                new Quantity(
                    "Mass",
                    new List<Unit>
                    {
                        new Unit("KiloGram", 1),
                        new Unit("Gram", 0.001),
                        new Unit("Tons", 1000)
                    }),
                new Quantity(
                    "Volume",
                    new List<Unit>
                    {
                        new Unit("CubicMeter", 1),
                        new Unit("CubicCentimerter", 1.0 / (100 * 100 * 100)),
                        new Unit("Liter", 1.0 / (10 * 10 * 10)),
                    }),
                new Quantity(
                    "Density",
                    new List<Unit>
                    {
                        new Unit("GramPerCubicCentimeter", 1),
                        new Unit("TonsPerCubicMeter", 1),
                        new Unit("KiloGramPerCubicMeter", 0.001)
                    }),
                new Quantity(
                    "Velocity",
                    new List<Unit>
                    {
                        new Unit("MeterPerSecond", 1)
                    }),
                new Quantity(
                    "Energy",
                    new List<Unit>
                    {
                        new Unit("Joule", 1),
                        new Unit("KiloJoule", 1000),
                        new Unit("MegaJoule", 1000 * 1000)/*,
                        new Unit("WattHours", 1),
                        new Unit("KiloWattHours", 1000),
                        new Unit("MegaWattHours", 1000 * 1000)*/
                    })
            };

            var path = @"G:\Projects\DotNet\JPV\SpaceConsole\ConsoleApp\Quantities\UnitExtensions.cs";

            File.Delete(path);

            using (var stream = File.OpenWrite(path))
            {
                using (var writer = new StreamWriter(stream))
                {
                    Write(writer, "SpaceConsole.ConsoleApp.Quantities", "UnitExtensions", quantities);
                }
            }
        }

        public static void Write(TextWriter writer, string classNamespace, string className, IEnumerable<Quantity> quantities)
        {
            writer.WriteLine($"namespace {classNamespace}");
            writer.WriteLine("{");

            writer.WriteLine(1, $"public static class {className}");
            writer.WriteLine(1, "{");

            foreach (var quantity in quantities)
            {
                foreach (var unit in quantity.Units)
                {
                    var parameterName = quantity.Name.FirstLetterToLower();
                    var factorString = unit.Factor.ToString(CultureInfo.InvariantCulture) + "d";

                    writer.WriteLine(2, $"public static double {unit.Name}(this double {parameterName})");
                    writer.WriteLine(2, "{");
                    writer.WriteLine(3, $"return {parameterName} * {factorString};");
                    writer.WriteLine(2, "}");
                    writer.WriteLine();

                    writer.WriteLine(2, $"public static double In{unit.Name}(this double {parameterName})");
                    writer.WriteLine(2, "{");
                    writer.WriteLine(3, $"return {parameterName} / {factorString};");
                    writer.WriteLine(2, "}");
                    writer.WriteLine();

                    writer.WriteLine(2, $"public static double {unit.Name}(this int {parameterName})");
                    writer.WriteLine(2, "{");
                    writer.WriteLine(3, $"return {parameterName} * {factorString};");
                    writer.WriteLine(2, "}");
                    writer.WriteLine();

                    writer.WriteLine(2, $"public static double In{unit.Name}(this int {parameterName})");
                    writer.WriteLine(2, "{");
                    writer.WriteLine(3, $"return {parameterName} / {factorString};");
                    writer.WriteLine(2, "}");
                    writer.WriteLine();
                }
            }

            writer.WriteLine(1, "}");
            writer.WriteLine("}");
        }
    }
}
