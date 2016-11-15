using System.Drawing;
using DrawablesUI;

namespace GraphicsEditor
{
    public class PointShape: IDrawable
    {
        public readonly PointF Position;


        public PointShape(PointF position)
        {
            Position = position;
        }

        public void Draw(IDrawer drawer)
        {
            drawer.DrawPoint(Position);
        }
    }
}