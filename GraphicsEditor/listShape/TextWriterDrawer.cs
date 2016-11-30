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
        private Transformation trans = new Transformation();

        public TextWriterDrawer(TextWriter writer = null)
        {
            this.writer = writer ?? Console.Out;
        }

        public void SelectPen(Color color, int width = 1)
        { }

        public void SetTransform(Transformation trans)
        {
            this.trans = trans;
        }

        public void Reset()
        {
            index = 0;
        }

        public void DrawPoint(PointF point)
        {
            var newPoint = new PointF[1] { point };
            if (!trans.transformationMatrix.IsIdentity)
            {
                trans.transformationMatrix.TransformPoints(newPoint);
            }
            writer.WriteLine(index + ")" + ToString(newPoint[0]));
            index++;
        }

        public void DrawLine(PointF start, PointF end)
        {
            var newPoints = new PointF[2] { start, end };
            if (!trans.transformationMatrix.IsIdentity)
            {
                trans.transformationMatrix.TransformPoints(newPoints);
            }
            writer.WriteLine($"{index})Линия({ToString(newPoints[0])}, {ToString(newPoints[1])})");
            index++;
        }

        public void DrawEllipseArc(PointF center, SizeF sizes, float startAngle = 0, float endAngle = 360, float rotate = 0)
        {
            var newPoint = new PointF[1] { center };
            if (!trans.transformationMatrix.IsIdentity)
            {
                trans.transformationMatrix.TransformPoints(newPoint);
                sizes.Height *= trans.transformationMatrix.Elements[3];
                sizes.Width *= trans.transformationMatrix.Elements[0];
                var radians = Math.Atan2(trans.transformationMatrix.Elements[2], trans.transformationMatrix.Elements[0]);
                rotate += (float)(radians * 180 / Math.PI);
            }
            if (Math.Abs(sizes.Width - sizes.Height) < 0.00001 && endAngle - startAngle >= 360)
            {
                writer.WriteLine($"{index})Круг({ToString(newPoint[0])}, Радиус={sizes.Height / 2})");
            }
            else if (endAngle - startAngle >= 360)
            {
                writer.WriteLine($"{index})Элипс({ToString(newPoint[0])}, {ToString(sizes)}, Угл поворота={rotate})");
            }
            else
            {
                writer.WriteLine($"{index})Дуга({ToString(newPoint[0])}, {ToString(sizes)}, Дуга=({startAngle}, {endAngle}), Угл поворота={rotate})");
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