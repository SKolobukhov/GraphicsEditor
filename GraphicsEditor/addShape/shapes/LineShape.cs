using System;
using System.Drawing;
using DrawablesUI;

namespace GraphicsEditor
{
    public class LineShape: IShape
    {
        public readonly PointF Start;
        public readonly PointF End;
        private Transformation trans = new Transformation();

        public LineShape(PointF start, PointF end)
        {
            Start = start;
            End = end;
        }
        
        public void Draw(IDrawer drawer)
        {
            drawer.SetTransform(trans);
            drawer.DrawLine(Start, End);
        }

        public void Transform(Transformation transformation)
        {
            trans.transformationMatrix.Multiply(transformation.transformationMatrix);
        }
    }
}