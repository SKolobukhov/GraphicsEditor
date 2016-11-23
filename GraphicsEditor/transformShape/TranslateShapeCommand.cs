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
            if (parameters == null || !parameters.Any() || parameters.Length != 3)
            {
                Console.WriteLine($"Неверное количество параметров: {parameters.Length}");
                return;
            }
            var translatePoint = Convertor.Convert<PointF>(parameters.Take(2).ToArray());
            var shapeIndex = Convertor.Convert<int>(parameters.Skip(2).ToArray());
            try
            {
                
                Transformation tmpTransformation = Transformation.Translate(translatePoint);
                picture.TransformAt(shapeIndex, tmpTransformation);
            }
            catch (Exception exception)
            {
                Console.WriteLine($"Невозможно удалить: {exception.Message}");
            }
        }
    }
}
