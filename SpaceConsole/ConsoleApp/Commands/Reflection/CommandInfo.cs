using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace SpaceConsole.ConsoleApp.Commands.Reflection
{
    public sealed class CommandInfo
    {
        public IReadOnlyCollection<string> Names { get; }

        public MethodInfo ExecuteMethod { get; }

        public IReadOnlyList<CommandArgumentInfo> Arguments { get; }

        public CommandInfo(IEnumerable<string> names, MethodInfo executeMethod, IReadOnlyList<CommandArgumentInfo> arguments)
        {
            Names = names.ToList();
            ExecuteMethod = executeMethod;
            Arguments = arguments;
        }
    }
}
