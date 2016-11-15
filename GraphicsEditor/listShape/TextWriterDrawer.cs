using System;
using System.Drawing;
using System.IO;
using DrawablesUI;

namespace GraphicsEditor
{
    public class TextWriterDrawer : IDrawer, IDisposable
    {
        private readonly TextWriter writer;
        private int index;

        public TextWriterDrawer(TextWriter writer = null)
        {
            this.writer = writer ?? Console.Out;
        }
        public void SelectPen(Color color, int width = 1)
        { }

        public void Reset()
        {
            index = 0;
        }

        public void DrawPoint(PointF point)
        {
            writer.WriteLine(index + ")" + ToString(point));
            index++;
        }

        public void DrawLine(PointF start, PointF end)
        {
            writer.WriteLine($"{index})Линия({ToString(start)}, {ToString(end)})");
            index++;
        }

        public void DrawEllipseArc(PointF center, SizeF sizes, float startAngle = 0, float endAngle = 360, float rotate = 0)
        {
            if (Math.Abs(sizes.Width - sizes.Height) < 0.00001 && endAngle - startAngle >= 360)
            {
                writer.WriteLine($"{index})Круг({ToString(center)}, Радиус={sizes.Height / 2})");
            }
            else if (endAngle - startAngle >= 360)
            {
                writer.WriteLine($"{index})Элипс({ToString(center)}, {ToString(sizes)}, Угл поворота={rotate})");
            }
            else
            {
                writer.WriteLine($"{index})Дуга({ToString(center)}, {ToString(sizes)}, Дуга=({startAngle}, {endAngle}), Угл поворота={rotate})");
            }
            index++;
        }

        private string ToString(PointF point)
        {
            return $"Точка({point.X}, {point.Y})";
        }

        private string ToString(SizeF size)
        {
            return $"Размеры({size.Width}, {size.Height})";
        }

        public void Dispose()
        {
            index = 0;
        }
    }
}