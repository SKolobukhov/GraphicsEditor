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

        private Transformation(System.Drawing.Drawing2D.Matrix matrix)
        {
            transformationMatrix = matrix;
        }

        public static Transformation Rotate(PointF point, float angle)
        {
            var result = new System.Drawing.Drawing2D.Matrix();
            result.RotateAt(angle, point);
            return new Transformation(result);
        }

        public static Transformation Translate(PointF point)
        {
            var result = new System.Drawing.Drawing2D.Matrix();
            result.Translate(point.X, point.Y);
            return new Transformation(result);
        }

        public static Transformation Scale(PointF point, float scaleFactor)
        {
            var result = new System.Drawing.Drawing2D.Matrix();
            result.Translate(-point.X, -point.Y);
            result.Scale(scaleFactor, scaleFactor);
            result.Translate(point.X, point.Y);
            return new Transformation(result);
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

        public PointF this[PointF point] => Transformate(point); ///Возвращает то, что нужно?

        public PointF Transformate(PointF point)
        {
            if (cache.Contains(point.ToString()))
            {
                return (PointF)cache.GetCacheItem(point.ToString()).Value;
            }
            cache.Add(point.ToString(), point, DateTimeOffset.MaxValue);
            return point;
        }
    }
}