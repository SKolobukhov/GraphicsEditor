using ConsoleUI;
using DrawablesUI;
using System;
using System.Drawing;

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
        public string Description => "scale x y factor shape1 [shape2 ...]";
        public string[] Synonyms => new[] { "sc", "zoom" };

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
                var scalePoint = Convertor.Convert<PointF>(parametersProvider.GetParameters(2));
                var scaleFactor = Convertor.Convert<float>(parametersProvider.GetParameters(1));
                var transformation = Transformation.Scale(scalePoint, scaleFactor);
                picture.Transform(transformation, parametersProvider.RemainingParameters());
            }
            catch (Exception exception)
            {
                Console.WriteLine($"Невозможно выполнить масштабирование фигуры: {exception.Message}");
            }
        }
    }
}
