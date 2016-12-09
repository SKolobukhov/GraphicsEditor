using System;
using System.Linq;
using ConsoleUI;

namespace GraphicsEditor
{
    public class GroupCommand : ICommand
    {
        private readonly Picture picture;

        public GroupCommand(Picture picture)
        {
            this.picture = picture;
        }

        public string Name => "group";
        public string Help => "Группировка фигур";
        public string Description => string.Empty;
        public string[] Synonyms => new string[0];

        public void Execute(params string[] parameters)
        {
            if (parameters.Length < 1)
            {
                Console.WriteLine($"Неверное количество параметров: {parameters.Length}");
                return;
            }

            try
            {
                var shapes = parameters.Select(parameter => picture.GetShapeByIndex(parameter));
                var group = new CompoundShape(shapes);
                picture.Remove(parameters);
                picture.Add(group);
            }
            catch (Exception exception)
            {
                Console.WriteLine($"Невозможно сгруппировать: {exception.Message}");
            }
        }
    }
}