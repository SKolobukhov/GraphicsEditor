using ConsoleUI;
using DrawablesUI;
using System;
using System.Drawing;
using System.Linq;

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
        public string Description => string.Empty;
        public string[] Synonyms => new[] { "rt" };

        public void Execute(params string[] parameters)
        {
            if (parameters.Length != 4)
            {
                Console.WriteLine($"Неверное количество параметров: {parameters.Length}");
                return;
            }
            
            try
            {
                var rotatePoint = Convertor.Convert<PointF>(parameters.Take(2).ToArray());
                var rotationAngle = Convertor.Convert<float>(parameters.Skip(2).Take(1).ToArray());
                var shapeIndex = parameters.Last();
                var transformation = Transformation.Rotate(rotatePoint, rotationAngle);
                picture.Transform(shapeIndex, transformation);
            }
            catch (Exception exception)
            {
                Console.WriteLine($"Невозможно выполнить вращение фигуры: {exception.Message}");
            }
        }
    }
}
