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
    class ScaleShapeCommand:ICommand
    {
        private readonly Picture picture;

        public ScaleShapeCommand(Picture picture)
        {
            this.picture = picture;
        }

        public string Name => "scale";
        public string Help => "Масштабирование фигуры";
        public string Description => string.Empty;
        public string[] Synonyms => new[] { "sc" };

        public void Execute(params string[] parameters)
        {
            if (parameters == null || !parameters.Any() || parameters.Length != 4)
            {
                Console.WriteLine($"Неверное количество параметров: {parameters.Length}");
                return;
            }
            var scalePoint = Convertor.Convert<PointF>(parameters.Take(2).ToArray());
            var scaleFactor = Convertor.Convert<float>(parameters.Skip(2).Take(1).ToArray());
            var shapeIndex = Convertor.Convert<int>(parameters.Skip(3).ToArray());
            try
            {
                
                Transformation tmpTransformation = Transformation.Scale(scalePoint, scaleFactor);
                picture.TransformAt(shapeIndex, tmpTransformation);
            }
            catch (Exception exception)
            {
                Console.WriteLine($"Невозможно удалить: {exception.Message}");
            }
        }
    }
}
