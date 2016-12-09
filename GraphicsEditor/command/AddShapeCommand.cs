using System;
using System.Linq;
using ConsoleUI;

namespace GraphicsEditor
{
    public class AddShapeCommand : ICommand
    {
        private readonly Picture picture;
        private readonly ShapeProvider shapeProvider;

        public AddShapeCommand(Picture picture, ShapeProvider shapeProvider)
        {
            this.picture = picture;
            this.shapeProvider = shapeProvider;
        }

        public string Name => "add";
        public string Help => "Добавление фигуры на картину";
        public string Description => string.Empty;
        public string[] Synonyms => new[] { "+" };

        public void Execute(params string[] parameters)
        {
            if (parameters.Length < 1)
            {
                Console.WriteLine($"Неверное количество параметров: {parameters.Length}");
                return;
            }

            var shapeName = parameters.First();
            parameters = parameters.Skip(1).ToArray();
            try
            {
                var shape = shapeProvider.GetShape(shapeName, parameters);
                picture.Add(shape);
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
            }
        }
    }
}