using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using DrawablesUI;

namespace GraphicsEditor
{
    public class TextWriterDrawer : IDrawer, IDisposable
	{
		private readonly string index;
		private readonly string[] aliases;
		private readonly TextWriter writer;


        public TextWriterDrawer(string index, string[] aliases = null, TextWriter writer = null)
        {
	        this.index = index;
	        this.aliases = aliases ?? new string[0];
	        this.writer = writer ?? Console.Out;
        }

        public void SelectPen(Color color, int width = 1)
        { }

		public void DrawCompoundShape()
		{
			writer.WriteLine($"{WriteIndex()}Составная фигура");
		}

		public void DrawPoint(PointF point)
        {
            writer.WriteLine(WriteIndex() + point.ToStringPosition());
        }

        public void DrawLine(PointF start, PointF end)
        {
            writer.WriteLine($"{WriteIndex()}Линия({start.ToStringPosition()}, {end.ToStringPosition()})");
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
        }

        private string WriteIndex()
        {
	        var aliases = string.Empty;
	        if (this.aliases.Any())
	        {
		        aliases = "," + string.Join(",", this.aliases);
	        }
            return index + aliases + " ";
        }

        public void Dispose()
        { }
    }
}