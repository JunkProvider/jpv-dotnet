using System.Collections.Generic;
using System.Linq;

namespace SpaceConsole.ConsoleApp.Model.Celestials
{
    public class CelestialSystem : ICelestialSystem
    {
        public string Name { get; }

        public double Orbit { get; }

        public double Mass => (CentralBody?.Mass ?? 0) + (Children.Count != 0 ? Children.Sum(c => c.Mass) : 0);

        public ICelestial CentralBody { get; }

        public ICelestialSystem Parent { get; private set; }

        public IReadOnlyCollection<ICelestialSystem> Children { get; }

        public CelestialSystem(string name, double orbit, ICelestial centralBody, IReadOnlyCollection<CelestialSystem> children)
        {
            Name = name;
            Orbit = orbit;
            CentralBody = centralBody;
            Children = children;

            foreach (var child in children)
            {
                child.Parent = this;
            }
        }

        public override string ToString()
        {
            return Name;
        }
    }
}