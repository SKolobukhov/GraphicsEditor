using System;
using System.Drawing;
using DrawablesUI;

namespace GraphicsEditor
{
    public class CircleShape : IShape
    {
        private PointF center;
        private float radius;
        private Transformation trans = new Transformation();

        public CircleShape(PointF center, float radius)
        {
            this.center = center;
            this.radius = radius;
        }
        
        public void Draw(IDrawer drawer)
        {
            drawer.DrawEllipseArc(center, new SizeF(2*radius, 2*radius), trans);
        }

        public void Transform(Transformation transformation)
        {
            trans.transformationMatrix.Multiply(transformation.transformationMatrix);
        }
    }
}