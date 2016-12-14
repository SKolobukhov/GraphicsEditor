﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using DrawablesUI;

namespace GraphicsEditor
{
    public class TextWriterDrawer : IDrawer, IDisposable
    {
        private const int InitialValue = 0;
        private readonly TextWriter writer;

        private readonly List<int> index;

        public TextWriterDrawer(TextWriter writer = null)
        {
            this.writer = writer ?? Console.Out;
            index = new List<int> {InitialValue};
        }
        public void SelectPen(Color color, int width = 1)
        { }

        public void DrawPoint(PointF point)
        {
            writer.WriteLine(WriteIndex() + point.ToStringPosition());
            index[index.Count - 1]++;
        }

        public void DrawLine(PointF start, PointF end)
        {
            writer.WriteLine($"{WriteIndex()}Линия({start.ToStringPosition()}, {end.ToStringPosition()})");
            index[index.Count - 1]++;
        }

        public void DrawEllipseArc(PointF center, SizeF sizes, float startAngle = 0, float endAngle = 360, float rotate = 0)
        {
            if (Math.Abs(sizes.Width - sizes.Height) < 0.00001 && endAngle - startAngle >= 360)
            {
                writer.WriteLine($"{WriteIndex()}Круг({center.ToStringPosition()}, Радиус={sizes.Height / 2})");
            }
            else if (endAngle - startAngle >= 360)
            {
                writer.WriteLine($"{WriteIndex()}Элипс({center.ToStringPosition()}, {sizes.ToStringSize()}, Угл поворота={rotate})");
            }
            else
            {
                writer.WriteLine($"{WriteIndex()}Дуга({center.ToStringPosition()}, {sizes.ToStringSize()}, Дуга=({startAngle}, {endAngle}), Угл поворота={rotate})");
            }
            index[index.Count - 1]++;
        }

        private string WriteIndex()
        {
            return "[" + string.Join(":", index).Trim(':') + "] ";
        }

        public void StartDraw()
        {
            writer.WriteLine(WriteIndex() + "Составная фигура");
            index.Add(InitialValue);
        }

        public void EndDraw()
        {
            index.Remove(index.Count - 1);
        }

        public void Dispose()
        { }
    }
}