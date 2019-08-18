namespace SpaceConsole.ConsoleApp.Model.Celestials
{
    public class Celestial : ICelestial
    {
        public string Name { get; }

        public double Radius { get; }

        public double Mass { get; }

        public Celestial(string name, double radius, double mass)
        {
            Name = name;
            Radius = radius;
            Mass = mass;
        }

        public override string ToString()
        {
            return Name;
        }
    }
}