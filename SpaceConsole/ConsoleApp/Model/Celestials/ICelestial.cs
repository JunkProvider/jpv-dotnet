namespace SpaceConsole.ConsoleApp.Model.Celestials
{
    public interface ICelestial
    {
        string Name { get; }

        double Radius { get; }

        double Mass { get; }
    }
}
