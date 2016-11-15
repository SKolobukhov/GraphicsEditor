using System;
using System.Linq;

namespace ConsoleUI
{
    public class ExplainCommand : ICommand
    {
        private readonly Application app;

        public string Name => "explain";
        public string Help => "Рассказать о команде или командах";
        public string[] Synonyms => new[] { "elaborate" };
        public string Description => "Выводит всю доступную информацию по команде или командам. Имена команд передаются как параметры";

        public ExplainCommand(Application app)
        {
            this.app = app;
        }

        public void Execute(params string[] parameters)
        {
            foreach (var parameter in parameters)
            {
                var p = app.Variables.Resolve<string>(parameter) ?? parameter;
                var command = app.FindCommand(p);
                Console.WriteLine(line);
                var synonyms = command.Synonyms.ToList();
                if (command.Name == p)
                {
                    Console.WriteLine("{0}: {1}", command.Name, command.Help);
                }
                else
                {
                    Console.WriteLine("{0}: {1}", p, command.Help);
                    synonyms.Remove(p);
                    synonyms.Add(command.Name);
                }
                if (synonyms.Count > 0)
                {
                    Console.WriteLine("Синонимы: {0}", string.Join(", ", synonyms));
                }
                if (command.Description != string.Empty)
                {
                    Console.WriteLine(line1);
                    Console.WriteLine(command.Description);
                }
            }
            Console.WriteLine(line);
        }

        private const string line = "================================================";
        private const string line1 = "...............................................";

    }

}
