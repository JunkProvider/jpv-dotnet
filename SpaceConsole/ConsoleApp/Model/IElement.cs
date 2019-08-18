namespace SpaceConsole.ConsoleApp.Model
{
    public interface IElement
    {
        string Name { get; }
        string Symbol { get; }
        double Mass { get; }
        double SolidDensity { get; }
        double LiquidDensity { get; }
        double GasDensity { get; }
    }
}
