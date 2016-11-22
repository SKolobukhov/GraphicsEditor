using System.Drawing;
using DrawablesUI;

namespace GraphicsEditor
{
    public class LineShape: IShape
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

        public void Transform(Transformation transformation)
        {
            throw new System.NotImplementedException();
        }
    }
}