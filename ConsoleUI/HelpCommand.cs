using System;

namespace ConsoleUI
{
    public class HelpCommand : ICommand
    {
        private readonly Application app;

        public string Name => "help";
        public string Help => "Краткая помощь по всем командам";
        public string[] Synonyms => new[] { "?" };
        public string Description => "Выводит список  команд с краткой помощью";

        public HelpCommand(Application app)
        {
            this.app = app;
        }

        public void Execute(params string[] parameters)
        {
            Console.WriteLine(line);
            foreach (var command in app.Commands)
            {
                Console.WriteLine("{0}: {1}", command.Name, command.Help);
            }
            Console.WriteLine(line);
        }

        private const string line = "================================================";

    }

}
