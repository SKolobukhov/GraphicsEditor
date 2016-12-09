using ConsoleUI;
using DrawablesUI;
using System;
using System.Drawing;
using System.Linq;

namespace GraphicsEditor
{
    class TranslateShapeCommand : ICommand
    {
        private readonly Picture picture;

        public TranslateShapeCommand(Picture picture)
        {
            this.picture = picture;
        }

        public string Name => "translate";
        public string Help => "Параллельный перенос фигуры";
        public string Description => string.Empty;
        public string[] Synonyms => new[] { "tr", "move" };

        public void Execute(params string[] parameters)
        {
            if (parameters.Length != 3)
            {
                Console.WriteLine($"Неверное количество параметров: {parameters.Length}");
                return;
            }
            
            try
            {
                var point = Convertor.Convert<PointF>(parameters.Take(2).ToArray());
                var shapeIndex = parameters.Last();
                var transformation = Transformation.Translate(point);
                picture.Transform(shapeIndex, transformation);
            }
            catch (Exception exception)
            {
                Console.WriteLine($"Невозможно выполнить параллельный перенос: {exception.Message}");
            }
        }
    }
}
