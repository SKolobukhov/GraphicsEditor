using ConsoleUI;
using DrawablesUI;
using System;
using System.Drawing;
using System.Linq;

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
        public string[] Synonyms => new[] { "sc", "zoom" };

        public void Execute(params string[] parameters)
        {
            if (parameters.Length != 4)
            {
                Console.WriteLine($"Неверное количество параметров: {parameters.Length}");
                return;
            }
            
            try
            {
                var scalePoint = Convertor.Convert<PointF>(parameters.Take(2).ToArray());
                var scaleFactor = Convertor.Convert<float>(parameters.Skip(2).Take(1).ToArray());
                var shapeIndex = parameters.Last();
                var transformation = Transformation.Scale(scalePoint, scaleFactor);
                picture.Transform(shapeIndex, transformation);
            }
            catch (Exception exception)
            {
                Console.WriteLine($"Невозможно выполнить масштабирование фигуры: {exception.Message}");
            }
        }
    }
}
