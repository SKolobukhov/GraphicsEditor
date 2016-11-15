using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleUI
{
    public class Application
    {
        private bool keepRunning = true;
        private readonly NameTable vars = new NameTable();

        private readonly NotFoundCommand notFound = new NotFoundCommand();
        private readonly List<ICommand> commands = new List<ICommand>();
        private readonly Dictionary<string, ICommand> commandMap = new Dictionary<string, ICommand>();

        public NameTable Variables => vars;

        public IList<ICommand> Commands => commands;

        public void Exit()
        {
            keepRunning = false;
        }

        public void AddCommand(ICommand command)
        {
            commands.Add(command);
            if (commandMap.ContainsKey(command.Name))
            {
                throw new Exception($"Команда {command.Name} уже добавлена");
            }
            commandMap.Add(command.Name, command);
            foreach (var synonyms in command.Synonyms)
            {
                if (commandMap.ContainsKey(synonyms))
                {
                    Console.WriteLine("ERROR: Игнорирую синоним {0} для команды {1}, поскольку имя {0}  уже использовано", synonyms, command.Name);
                    continue;
                }
                commandMap.Add(synonyms, command);
            }
        }

        public ICommand FindCommand(string name)
        {
            if (commandMap.ContainsKey(name))
            {
                return commandMap[name];
            }
            notFound.Name = name;
            return notFound;
        }

        public void Run()
        {
            string[] cmdline, parameters;
            string rawInput;
            while (keepRunning)
            {
                Console.Write("> ");
                rawInput = Console.ReadLine();
                cmdline = rawInput.Split(new[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);
                if (cmdline.Length == 0)
                {
                    continue;
                }

                parameters = cmdline.Skip(1).ToArray();
                FindCommand(cmdline[0]).Execute(parameters);
            }
        }
    }
}
