using System;
using System.Drawing;
using DrawablesUI;

namespace GraphicsEditor
{
    public class LineShape: IShape
    {
        public PointF Start;
        public PointF End;

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
            Start = transformation[Start];
            End = transformation[End];
        }
    }
}