using System.Drawing;
using DrawablesUI;

namespace GraphicsEditor
{
    public class LineShape: Shape
    {
        public readonly PointF Start;
        public readonly PointF End;


        public LineShape(PointF start, PointF end)
        {
            Start = start;
            End = end;
        }
        
        protected override void DrawShape(IDrawer drawer)
        {
            drawer.DrawLine(Start, End);
        }
    }
}