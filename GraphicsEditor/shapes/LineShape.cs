using System;
using System.Drawing;
using DrawablesUI;

namespace GraphicsEditor
{
    public class LineShape: Shape
    {
        public PointF Start;
        public PointF End;

        public LineShape(PointF start, PointF end)
        {
            Start = start;
            End = end;
        }

        public override void Draw(IDrawer drawer)
        {
            drawer.DrawLine(Start, End);
        }

        public override void Transform(Transformation transformation)
        {
            Start = transformation[Start];
            End = transformation[End];
        }
    }
}