using System.Collections.Generic;

namespace QuantityGenerator
{
    public class Quantity
    {
        public string Name { get; }

        public List<Unit> Units { get; }

        public Quantity(string name, List<Unit> units)
        {
            Units = units;
            Name = name;
        }
    }
}