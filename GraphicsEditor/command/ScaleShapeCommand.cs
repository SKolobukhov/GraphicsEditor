﻿using ConsoleUI;
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
            if (parameters == null || !parameters.Any() || parameters.Length != 4) /// необходимость проверок
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
                var shape = picture.GetShape(shapeIndex);
                shape.Transform(transformation);
                picture.Redraw();
            }
            catch (Exception exception)
            {
                Console.WriteLine($"Невозможно выполнить масштабирование фигуры: {exception.Message}");
            }
        }
    }
}
