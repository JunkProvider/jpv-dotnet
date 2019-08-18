using System.Collections.Generic;

namespace SpaceConsole.ConsoleApp.Model
{
    public sealed class World
    {
        public IList<Station> Stations { get; set; } = new List<Station>();

        public Ship Ship { get; set; }
    }
}
