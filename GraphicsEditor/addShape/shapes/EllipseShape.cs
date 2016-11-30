using System;
using System.Drawing;
using DrawablesUI;

namespace GraphicsEditor
{
    public class EllipseShape: IShape
    {
        private readonly PointF center;
        private readonly SizeF size;
        private readonly float rotate;
        private Transformation trans = new Transformation();

        public EllipseShape(PointF center, SizeF size, float rotate)
        {
            this.center = center;
            this.size = size;
            this.rotate = rotate;
        }

        public void Draw(IDrawer drawer)
        {
            drawer.SetTransform(trans);
            drawer.DrawEllipseArc(center, size, 0, 360, rotate);
        }

        public void Transform(Transformation transformation)
        {
            trans.transformationMatrix.Multiply(transformation.transformationMatrix);
        }
    }
}