using ConsoleUI;
using DrawablesUI;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            if (parameters == null || !parameters.Any() || parameters.Length != 4) /// необходимость проверок
            {
                Console.WriteLine($"Неверное количество параметров: {parameters.Length}");
                return;
            }
            var rotatePoint = Convertor.Convert<PointF>(parameters.Take(2).ToArray());
            var rotationAngle = Convertor.Convert<float>(parameters.Skip(2).Take(1).ToArray());
            var shapeIndex = Convertor.Convert<int>(parameters.Skip(3).ToArray());
            try
            {
                
                Transformation tmpTransformation = Transformation.Rotate(rotatePoint, rotationAngle);
                picture.TransformAt(shapeIndex, tmpTransformation);
            }
            catch (Exception exception)
            {
                Console.WriteLine($"Невозможно выполнить вращение фигуры: {exception.Message}");
            }
        }
    }
}
