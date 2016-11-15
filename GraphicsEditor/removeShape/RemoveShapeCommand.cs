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
        public string Description => string.Empty;
        public string[] Synonyms => new[] { "-" };

        //todo проверять на индекс
        public void Execute(params string[] parameters)
        {
            if (parameters.Length != 1)
            {
                Console.WriteLine($"Неверное количество параметров: {parameters.Length}");
            }
            int index;
            if (!int.TryParse(parameters[0], out index))
            {
                Console.WriteLine($"Неверный параметр \"{parameters[0]}\"");
            }
            try
            {
                picture.RemoveAt(index);
            }
            catch (Exception exception)
            {
                Console.WriteLine($"Невозможно удалить: {exception.Message}");
            }
        }
    }
}