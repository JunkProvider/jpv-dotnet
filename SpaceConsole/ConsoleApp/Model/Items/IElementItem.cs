namespace SpaceConsole.ConsoleApp.Model.Items
{
    public interface IElementItem :IItem
    {
        IElement Element { get; }
    }
}