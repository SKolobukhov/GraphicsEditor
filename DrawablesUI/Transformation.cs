using System;
using System.Drawing;
using System.Runtime.Caching;

namespace DrawablesUI
{
    public class Transformation
    {
        public readonly System.Drawing.Drawing2D.Matrix transformationMatrix;
        private readonly MemoryCache cache;

        public Transformation()
        {
            transformationMatrix = new System.Drawing.Drawing2D.Matrix();
            cache = new MemoryCache($"{GetType().Name}Cache");
        }

        public Transformation(System.Drawing.Drawing2D.Matrix matrix)
        {
            transformationMatrix = matrix;
        }

        public static Transformation Rotate(PointF point, float angle)
        {
            var tmpMatrix = new System.Drawing.Drawing2D.Matrix();
            tmpMatrix.RotateAt(angle, point);
            return new Transformation(tmpMatrix);
        }

        public static Transformation Translate(PointF point)
        {
            var tmpMatrix = new System.Drawing.Drawing2D.Matrix();
            tmpMatrix.Translate(point.X, point.Y);
            return new Transformation(tmpMatrix);
        }

        public static Transformation Scale(PointF point, float scaleFactor)
        {
            var tmpMatrix = new System.Drawing.Drawing2D.Matrix();
            tmpMatrix.Translate(-point.X, -point.Y);
            tmpMatrix.Scale(scaleFactor, scaleFactor);
            tmpMatrix.Translate(point.X, point.Y);
            return new Transformation(tmpMatrix);
        }

        public static Transformation Scale(PointF point1, PointF point2, float scaleFactor)
        {
            if (point1 == point2)
            {
                throw new InvalidOperationException();
            }
            throw new NotImplementedException();
        }

        public static Transformation operator*(Transformation transformation1, Transformation transformation2)
        {
            var result = transformation1.transformationMatrix.Clone();
            result.Multiply(transformation2.transformationMatrix);
            return new Transformation(result);
        }

        public PointF this[PointF point] => Transformate(point);

        public PointF Transformate(PointF point)
        {
            if (cache.Contains(point.ToString()))
            {
                return (PointF)cache.GetCacheItem(point.ToString()).Value;
            }
            cache.Add(point.ToString(), point, DateTimeOffset.MaxValue);
            return point;
        }

        private PointF GetPointF(PointF point)
        {
            throw new NotImplementedException();
        }
    }
}