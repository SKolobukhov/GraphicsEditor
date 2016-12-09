using System.Drawing;
using DrawablesUI;

namespace GraphicsEditor
{
    public class CircleShape : Shape
    {
        private readonly PointF center;
        private readonly float radius;

        public CircleShape(PointF center, float radius)
        {
            this.center = center;
            this.radius = radius;
        }
        
        protected override void DrawShape(IDrawer drawer)
        {
            drawer.DrawEllipseArc(center, new SizeF(2*radius, 2*radius));
        }
    }
}