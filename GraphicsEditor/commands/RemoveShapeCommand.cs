using System;
using ConsoleUI;

namespace GraphicsEditor
{
    public class RemoveShapeCommand : ICommand
    {
        private readonly Picture picture;

        public RemoveShapeCommand(Picture picture)
        {
            this.picture = picture;
        }

        public string Name => "remove";
        public string Help => "удаление фигуры с картинки";
        public string Description => "remove shape1 [shape2 ...]";
        public string[] Synonyms => new[] { "-", "rm" };
        
        public void Execute(params string[] parameters)
        {
            if (parameters.Length < 1)
            {
                Console.WriteLine($"Неверное количество параметров: {parameters.Length}");
                return;
            }

            try
            {
                picture.Remove(parameters);
            }
            catch (Exception exception)
            {
                Console.WriteLine($"Невозможно удалить: {exception.Message}");
            }
        }
    }
}