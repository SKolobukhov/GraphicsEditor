using System;
using ConsoleUI;

namespace GraphicsEditor
{
    public class UpShapeCommand : ICommand
    {
        private readonly Picture picture;
        
        public UpShapeCommand(Picture picture)
        {
            this.picture = picture;
        }

        public string Name => "up";
        public string Help => "Поднять фигурy";
        public string Description => "up shape1 [shape2 ...]";
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
                picture.Move(-1, parameters);
            }
            catch (Exception exception)
            {
                Console.WriteLine($"Невозможно поднять фигурy: {exception.Message}");
            }
        }
    }
}