using System.Collections.Generic;

namespace SpaceConsole.ConsoleApp.Model.Celestials
{
    public interface ICelestialSystem
    {
        string Name { get; }

        double Orbit { get; }

        double Mass { get; }

        ICelestial CentralBody { get; }

        ICelestialSystem Parent { get; }

        IReadOnlyCollection<ICelestialSystem> Children { get; }
    }
}