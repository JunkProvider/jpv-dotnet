namespace SpaceConsole.ConsoleApp.Model
{
    public sealed class Element : IElement
    {
        public string Name { get; }

        public string Symbol { get; }

        public double Mass { get; }

        public double SolidDensity { get; }

        public double LiquidDensity { get; }

        public double GasDensity { get; }

        public Element(string name, string symbol, double mass, double solidDensity, double liquidDensity, double gasDensity)
        {
            Name = name;
            Symbol = symbol;
            Mass = mass;
            SolidDensity = solidDensity;
            LiquidDensity = liquidDensity;
            GasDensity = gasDensity;
        }
    }
}
