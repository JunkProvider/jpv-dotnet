using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace SpaceConsole.ConsoleApp.Commands.Reflection
{
    public static class CommandReflector
    {
        public static CommandInfo Reflect(Type type)
        {
            var names = new List<string> { type.Name };

            var unCamelCasedName = UnCamelCaseString(names[0]);

            if (!unCamelCasedName.Equals(names[0], StringComparison.InvariantCultureIgnoreCase))
                names.Add(unCamelCasedName);

            var executeMethod = type
                .GetMethods()
                .First(method => method.Name.Equals("Execute", StringComparison.InvariantCultureIgnoreCase));

            var parameters = executeMethod
                .GetParameters()
                .Select(parameter => new CommandArgumentInfo(MapDataType(parameter.ParameterType)))
                .ToList();

            return new CommandInfo(names, executeMethod, parameters);
        }

        private static CommandArgumentDataType MapDataType(MemberInfo type)
        {
            switch (type.Name.ToLowerInvariant())
            {
                case "string":
                    return CommandArgumentDataType.String;
                case "int32":
                case "int64":
                    return CommandArgumentDataType.Integer;
                default:
                    throw new ArgumentException();
            }
        }

        private static string UnCamelCaseString(string text)
        {
            var s = new StringBuilder();

            foreach (var character in text)
            {
                if (s.Length > 0 && char.IsUpper(character))
                {
                    s.Append(' ');
                }

                s.Append(character);
            }

            return s.ToString();
        }
    }
}
