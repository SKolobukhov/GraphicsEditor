using System;
using System.Drawing;
using DrawablesUI;

namespace GraphicsEditor
{
    public class EllipseShape: IShape
    {
        private PointF center;
        private SizeF size;
        private float rotate;

        public EllipseShape(PointF center, SizeF size, float rotate)
        {
            this.center = center;
            this.size = size;
            this.rotate = rotate;
        }

        public void Draw(IDrawer drawer)
        {
            drawer.DrawEllipseArc(center, size, 0, 360, rotate);
        }

        public void Transform(Transformation transformation)
        {
            size.Height = VectorParser(transformation[new Vector(new PointF(0, size.Height))]);
            size.Width = VectorParser(transformation[new Vector(new PointF(size.Width, 0))]);
            center = CoordinatesOptimize(transformation[center]);
            rotate += RotateParser(-transformation.transformationMatrix.Elements[2], transformation.transformationMatrix.Elements[0]);
        }

        private float VectorParser(Vector vector)
        {
            var tmpValue = Math.Sqrt(Math.Pow(vector.value.X, 2) + Math.Pow(vector.value.Y, 2));
            return (float)Math.Round(tmpValue, 5, MidpointRounding.ToEven);
        }

        private float RotateParser(float x, float y)
        {
            return (float)Math.Round((Math.Atan2(x, y) * 180 / Math.PI), 3, MidpointRounding.ToEven);
        }

        private PointF CoordinatesOptimize(PointF point)
        {
            return new PointF((float)Math.Round(point.X, 5, MidpointRounding.ToEven), (float)Math.Round(point.Y, 5, MidpointRounding.ToEven));
        }
    }
}