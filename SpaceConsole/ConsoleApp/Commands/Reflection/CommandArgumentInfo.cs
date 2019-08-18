namespace SpaceConsole.ConsoleApp.Commands.Reflection
{
    public sealed class CommandArgumentInfo
    {
        public CommandArgumentDataType DataType { get; }

        public CommandArgumentInfo(CommandArgumentDataType dataType)
        {
            DataType = dataType;
        }
    }
}