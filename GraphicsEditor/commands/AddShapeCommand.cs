using System;
using ConsoleUI;

namespace GraphicsEditor
{
    public class AddShapeCommand : ICommand
    {
        private readonly Picture picture;
        private readonly ShapeBuilder shapeBuilder;

        public AddShapeCommand(Picture picture, ShapeBuilder shapeBuilder)
        {
            this.picture = picture;
            this.shapeBuilder = shapeBuilder;
        }

        public string Name => "add";
        public string Help => "Добавление фигуры на картину";
        public string Description => "add shapeName arg1 [arg2 ...]";
        public string[] Synonyms => new[] { "+" };

        public void Execute(params string[] parameters)
        {
            if (parameters.Length < 1)
            {
                Console.WriteLine($"Неверное количество параметров: {parameters.Length}");
                return;
            }

            var parametersProvider = new ParametersProvider(parameters);
            var shapeName = parametersProvider.GetParameter();
            try
            {
                var shape = shapeBuilder.GetShape(shapeName, parametersProvider.RemainingParameters());
                picture.Add(shape);
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
            }
        }
    }
}