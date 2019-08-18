using System.Collections.Generic;
using System.Text;

namespace SpaceConsole.ConsoleApp.Commands.Invoker
{
    public static class CommandParser
    {
        public static IReadOnlyList<string> Parse(string input)
        {
            var arguments = new List<string>();

            var lastArgument = new StringBuilder();
            var readingQuoted = false;

            foreach (var character in input)
            {
                if (char.IsWhiteSpace(character) && !readingQuoted)
                {
                    if (lastArgument.Length == 0)
                        continue;

                    arguments.Add(lastArgument.ToString());
                    lastArgument = new StringBuilder();
                    continue;
                }

                if (character == '"')
                {
                    if (!readingQuoted)
                    {
                        readingQuoted = true;
                        continue;
                    }

                    readingQuoted = false;

                    if (lastArgument.Length == 0)
                        continue;

                    arguments.Add(lastArgument.ToString());
                    lastArgument = new StringBuilder();
                    continue;
                }

                lastArgument.Append(character);
            }

            if (lastArgument.Length > 0)
                arguments.Add(lastArgument.ToString());

            return arguments;
        }
    }
}
