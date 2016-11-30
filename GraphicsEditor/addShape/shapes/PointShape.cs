using System;
using System.Drawing;
using DrawablesUI;

namespace GraphicsEditor
{
    public class PointShape: IShape
    {
        public readonly PointF Position;
        private Transformation trans = new Transformation();

        public PointShape(PointF position)
        {
            Position = position;
        }

        public void Draw(IDrawer drawer)
        {
            drawer.SetTransform(trans);
            drawer.DrawPoint(Position);
        }

        public void Transform(Transformation transformation)
        {
            trans.transformationMatrix.Multiply(transformation.transformationMatrix);
        }
    }
}