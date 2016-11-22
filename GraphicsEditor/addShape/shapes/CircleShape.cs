using System.Drawing;
using DrawablesUI;

namespace GraphicsEditor
{
    public class CircleShape : IShape
    {
        private readonly PointF center;
        private readonly float radius;

        public CircleShape(PointF center, float radius)
        {
            this.center = center;
            this.radius = radius;
        }
        
        public void Draw(IDrawer drawer)
        {
            drawer.DrawEllipseArc(center, new SizeF(2*radius, 2*radius));
        }

        public void Transform(Transformation transformation)
        {
            throw new System.NotImplementedException();
        }
    }
}