using System.Drawing;
using DrawablesUI;

namespace GraphicsEditor
{
    public class LineShape: IDrawable
    {
        public readonly PointF Start;
        public readonly PointF End;


        public LineShape(PointF start, PointF end)
        {
            Start = start;
            End = end;
        }
        
        public void Draw(IDrawer drawer)
        {
            drawer.DrawLine(Start, End);
        }
    }
}