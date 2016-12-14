using System;
using System.Drawing;
using DrawablesUI;

namespace GraphicsEditor
{
    public class CircleShape : Shape
    {
        private PointF center;
        private float radius;

        public CircleShape(PointF center, float radius)
        {
            this.center = center;
            this.radius = radius;
        }

        public override void Draw(IDrawer drawer)
        {
            drawer.DrawEllipseArc(center, new SizeF(2*radius, 2*radius));
        }

        public override void Transform(Transformation transformation)
        {
            radius = RadiusParser(transformation[new Vector(new PointF(radius, 0))]);
            center = CoordinatesOptimize(transformation[center]);
        }

        private float RadiusParser(Vector vector)
        {
            var tmpValue = Math.Sqrt(Math.Pow(vector.value.X, 2) + Math.Pow(vector.value.Y, 2));
            return (float)Math.Round(tmpValue, 5, MidpointRounding.ToEven);
        }

        private PointF CoordinatesOptimize(PointF point)
        {
            return new PointF((float)Math.Round(point.X, 5, MidpointRounding.ToEven), (float)Math.Round(point.Y, 5, MidpointRounding.ToEven));
        }
    }
}