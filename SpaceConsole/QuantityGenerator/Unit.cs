namespace QuantityGenerator
{
    public class Unit
    {
        public string Name { get; }

        public double Factor { get; }

        public Unit(string name, double factor)
        {
            Name = name;
            Factor = factor;
        }
    }
}