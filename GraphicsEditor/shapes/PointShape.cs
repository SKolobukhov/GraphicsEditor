using System;
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

        public override void Draw(IDrawer drawer)
        {
            drawer.DrawPoint(Position);
        }

        public override void Transform(Transformation transformation)
        {
            Position = transformation[Position];
        }
    }
}