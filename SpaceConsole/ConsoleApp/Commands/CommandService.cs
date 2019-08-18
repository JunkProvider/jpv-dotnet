using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using SpaceConsole.ConsoleApp.Commands.Invoker;
using SpaceConsole.ConsoleApp.Commands.Reflection;
using SpaceConsole.ConsoleApp.ViewModel;

namespace SpaceConsole.ConsoleApp.Commands
{
    public sealed class CommandService
    {
        private CommandInvoker CommandInvoker { get; }

        public CommandService(IngameModel viewModel)
        {
            var commands = new List<object>
            {
                new Goto(viewModel),
                new MoveTo(viewModel),
                new Buy(viewModel),
                new Sell(viewModel)
            };

            CommandInvoker = new CommandInvoker(
                commands.Select(c => new CommandInvoker.Item(CommandReflector.Reflect(c.GetType().GetTypeInfo()), c)).ToList());
        }

        public bool Invoke(IReadOnlyList<string> input)
        {
            return CommandInvoker.Invoke(input);
        }
    }
}
