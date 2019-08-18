using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using SpaceConsole.ConsoleApp.Commands.Reflection;

namespace SpaceConsole.ConsoleApp.Commands.Invoker
{
    public sealed class CommandInvoker
    {
        private IReadOnlyDictionary<string, Item> Items { get; }

        public CommandInvoker(IEnumerable<Item> items)
        {
            var itemDict = new Dictionary<string, Item>();

            foreach (var item in items)
            {
                foreach (var name in item.CommandInfo.Names)
                {
                    itemDict.Add(name.ToUpperInvariant(), item);
                }
            }

            Items = itemDict;
        }

        public bool Invoke(IReadOnlyList<string> input)
        {
            if (input.Count == 0)
                return false;

            if (!Items.TryGetValue(input[0].ToUpperInvariant(), out var item))
                return false;

            var arguments = ParseArguments(item.CommandInfo.Arguments, input.Skip(1).ToArray());

            item.CommandInfo.ExecuteMethod.Invoke(item.Command, arguments);

            return true;
        }

        private static object[] ParseArguments(IEnumerable<CommandArgumentInfo> argumentInfos, IReadOnlyList<string> arguments)
        {
            return argumentInfos.Select((argument, index) => ParseArgument(argument, arguments.Count > index ? arguments[index] : string.Empty)).ToArray();
        }

        private static object ParseArgument(CommandArgumentInfo argumentInfo, string argument)
        {
            argument = argument ?? string.Empty;

            switch (argumentInfo.DataType)
            {
                case CommandArgumentDataType.String:
                    return argument;
                case CommandArgumentDataType.Integer:
                    return int.TryParse(argument, NumberStyles.Any, CultureInfo.InvariantCulture, out var intValue) ? intValue : 0;
                default:
                    throw new InvalidEnumArgumentException();
            }
        }

        public sealed class Item
        {
            public CommandInfo CommandInfo { get; }

            public object Command { get; }

            public Item(CommandInfo commandInfo, object command)
            {
                CommandInfo = commandInfo;
                Command = command;
            }
        }
    }
}
