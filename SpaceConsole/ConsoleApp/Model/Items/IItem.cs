namespace SpaceConsole.ConsoleApp.Model.Items
{
    public interface IItem : INamed
    {
        int MaxStackSize { get; }

        double Mass { get; }

        double Volume { get; }
    }
}
