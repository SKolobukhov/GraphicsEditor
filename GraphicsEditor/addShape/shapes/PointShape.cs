using System;
using System.Drawing;
using DrawablesUI;

namespace GraphicsEditor
{
    public class PointShape: IShape
    {
        public PointF Position;


        public PointShape(PointF position)
        {
            Position = position;
        }

        public void Draw(IDrawer drawer)
        {
            drawer.DrawPoint(Position);
        }

        public void Transform(Transformation transformation)
        {
            throw new NotImplementedException();
        }
    }
}