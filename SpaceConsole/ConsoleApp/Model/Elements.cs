using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using SpaceConsole.ConsoleApp.Quantities;

namespace SpaceConsole.ConsoleApp.Model
{
    public sealed class Elements
    {
        public static readonly IElement Hydrogen = new Element(
            name: "Hydrogen",
            symbol: "H", 
            mass: 1.000,
            solidDensity: 0.0763.GramPerCubicCentimeter(),
            liquidDensity: 0.07.GramPerCubicCentimeter(),
            gasDensity: 0.00008988.GramPerCubicCentimeter());

        public static readonly IElement Oxygen = new Element(
            name: "Oxygen",
            symbol: "O",
            mass: 16.000, 
            solidDensity: 1.141.GramPerCubicCentimeter(),
            liquidDensity: 1.141.GramPerCubicCentimeter(),
            gasDensity: 0.001429.GramPerCubicCentimeter());

        private static IReadOnlyList<IElement> All;

        public static IReadOnlyList<IElement> GetAll()
        {
            return All ?? (All = ReflectElements().ToList());
        }

        private static IEnumerable<IElement> ReflectElements()
        {
            return typeof(Elements)
                .GetFields(BindingFlags.Static)
                .Where(field => field.FieldType.IsAssignableFrom(typeof(IElement)))
                .Select(field => (IElement) field.GetValue(null));
        }
    }
}