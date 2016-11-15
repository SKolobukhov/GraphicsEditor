using System;

namespace ConsoleUI
{
    class NotFoundCommand : ICommand
    {
        public string Name { get; set; }
        public string Help => "команда не найдена";
        public string[] Synonyms => new string[] {};
        public string Description => string.Empty;


        public void Execute(params string[] parameters)
        {
            Console.WriteLine("Команда `{0}`  не найдена ", Name);
        }
    }

}
