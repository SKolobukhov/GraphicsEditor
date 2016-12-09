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
        public string[] Synonyms => new[] { "tr" };

        public void Execute(params string[] parameters)
        {
            if (parameters == null || !parameters.Any() || parameters.Length != 3) /// необходимость проверок
            {
                Console.WriteLine($"Неверное количество параметров: {parameters.Length}");
                return;
            }
            
            try
            {
                var translatePoint = Convertor.Convert<PointF>(parameters.Take(2).ToArray());
                var shapeIndex = parameters.Last();
                var transformation = Transformation.Translate(translatePoint);
                var shape = picture.GetShape(shapeIndex);
                shape.Transform(transformation);
                picture.Redraw();
            }
            catch (Exception exception)
            {
                Console.WriteLine($"Невозможно выполнить параллельный перенос: {exception.Message}");
            }
        }
    }
}
