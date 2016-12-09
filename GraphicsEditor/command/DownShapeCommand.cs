using System;
using ConsoleUI;

namespace GraphicsEditor
{
    public class DownShapeCommand : ICommand
    {
        private readonly Picture picture;
        
        public DownShapeCommand(Picture picture)
        {
            this.picture = picture;
        }

        public string Name => "down";
        public string Help => "Опустиь фигурy";
        public string Description => string.Empty;
        public string[] Synonyms => new string[0];

        public void Execute(params string[] parameters)
        {
            if (parameters.Length != 1)
            {
                Console.WriteLine($"Неверное количество параметров: {parameters.Length}");
                return;
            }

            try
            {
                picture.Move(parameters[0], -1);
            }
            catch (Exception exception)
            {
                Console.WriteLine($"Невозможно опустиь фигурy: {exception.Message}");
            }
        }
    }
}