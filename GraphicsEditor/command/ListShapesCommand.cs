using System;
using ConsoleUI;

namespace GraphicsEditor
{
    public class ListShapesCommand : ICommand
    {
        private readonly Picture picture;

        public ListShapesCommand(Picture picture)
        {
            this.picture = picture;
        }

        public string Name => "list";
        public string Help => "Выводит список фигур на картинке.";
        public string Description => "list";
        public string[] Synonyms => new[] { "ls" };

        public void Execute(params string[] parameters)
        {
            if (parameters.Length != 0)
            {
                Console.WriteLine($"Неверное количество параметров: {parameters.Length}");
                return;
            }

            picture.Draw(new TextWriterDrawer());
        }
    }
}