using ConsoleUI;
using DrawablesUI;
using System;
using System.Drawing;

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
        public string Description => "translate x y shape1 [shape2 ...]";
        public string[] Synonyms => new[] { "tr", "move" };

        public void Execute(params string[] parameters)
        {
            if (parameters.Length < 3)
            {
                Console.WriteLine($"Неверное количество параметров: {parameters.Length}");
                return;
            }
            
            try
            {
	            var parametersProvider = new ParametersProvider(parameters);
                var point = Convertor.Convert<PointF>(parametersProvider.GetParameters(2));
                var transformation = Transformation.Translate(point);
                picture.Transform(transformation, parametersProvider.RemainingParameters());
            }
            catch (Exception exception)
            {
                Console.WriteLine($"Невозможно выполнить параллельный перенос: {exception.Message}");
            }
        }
    }
}
