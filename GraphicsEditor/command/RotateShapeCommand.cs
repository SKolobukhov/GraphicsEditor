using ConsoleUI;
using DrawablesUI;
using System;
using System.Drawing;

namespace GraphicsEditor
{
    class RotateShapeCommand:ICommand
    {
        private readonly Picture picture;

        public RotateShapeCommand(Picture picture)
        {
            this.picture = picture;
        }

        public string Name => "rotate";
        public string Help => "Поворот фигуры";
        public string Description => "rotate x y angle shape1 [shape2 ...]";
        public string[] Synonyms => new[] { "rt" };

        public void Execute(params string[] parameters)
        {
            if (parameters.Length < 4)
            {
                Console.WriteLine($"Неверное количество параметров: {parameters.Length}");
                return;
            }
            
            try
            {
                var parametersProvider = new ParametersProvider(parameters);
                var rotatePoint = Convertor.Convert<PointF>(parametersProvider.GetParameters(2));
                var rotationAngle = Convertor.Convert<float>(parametersProvider.GetParameters(1));
                var transformation = Transformation.Rotate(rotatePoint, rotationAngle);
                picture.Transform(transformation, parametersProvider.RemainingParameters());
            }
            catch (Exception exception)
            {
                Console.WriteLine($"Невозможно выполнить вращение фигуры: {exception.Message}");
            }
        }
    }
}
