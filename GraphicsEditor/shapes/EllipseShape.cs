using System.Drawing;
using DrawablesUI;

namespace GraphicsEditor
{
    public class EllipseShape: Shape
    {
        private readonly PointF center;
        private readonly SizeF size;
        private readonly float rotate;
        

        public EllipseShape(PointF center, SizeF size, float rotate)
        {
            this.center = center;
            this.size = size;
            this.rotate = rotate;
        }

        protected override void DrawShape(IDrawer drawer)
        {
            drawer.DrawEllipseArc(center, size, 0, 360, rotate);
        }
    }
}