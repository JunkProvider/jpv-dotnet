using System.Collections.Generic;
using System.Linq;

namespace SpaceConsole.ConsoleApp.Model.Celestials
{
    public class CelestialFactory
    {
        public CelestialSystem Create(string name, double orbit, double radius, double density)
        {
            return Create(name, orbit, radius, density, new CelestialSystem[0]);
        }

        public CelestialSystem Create(string name, double orbit, double radius, double density, IEnumerable<CelestialSystem> children)
        {
            return Create($"{name} System", name, orbit, radius, density, children);
        }

        public CelestialSystem Create(string systemName, string name, double orbit, double radius, double density)
        {
            return Create(systemName, name, orbit, radius, density, new CelestialSystem[0]);
        }

        public CelestialSystem Create(string systemName, string name, double orbit, double radius, double density, IEnumerable<CelestialSystem> children)
        {
            return new CelestialSystem(
                name: systemName,
                orbit: orbit,
                centralBody: new Celestial(name, radius, CelestialMath.GetMass(radius, density)),
                children: children.ToList());
        }
    }
}
