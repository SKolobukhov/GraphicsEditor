using System.Drawing;
using DrawablesUI;

namespace GraphicsEditor
{
    public class PointShape: Shape
    {
        public PointF Position;
        

        public PointShape(PointF position)
        {
            Position = position;
        }

        protected override void DrawShape(IDrawer drawer)
        {
            drawer.DrawPoint(Position);
        }
    }
}