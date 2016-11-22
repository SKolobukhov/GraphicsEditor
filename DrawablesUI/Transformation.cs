using System;
using System.Drawing;
using System.Runtime.Caching;

namespace DrawablesUI
{
    public class Transformation
    {
        public static Transformation Rotate(float angle, PointF? point = null)
        {
            throw new NotImplementedException();
        }

        public static Transformation Translate(PointF point)
        {
            throw new NotImplementedException();
        }

        public static Transformation Scale(float scaleFactor, PointF? point = null)
        {
            throw new NotImplementedException();
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
            throw new NotImplementedException();
        }


        private readonly MemoryCache cache;


        protected Transformation()
        {
            cache = new MemoryCache($"{GetType().Name}Cache");
        }

        public PointF this[PointF point] => Transformate(point);

        public PointF Transformate(PointF point)
        {
            if (cache.Contains(point.ToString()))
            {
                return (PointF)cache.GetCacheItem(point.ToString()).Value;
            }
            point = GetPointF(point);
            cache.Add(point.ToString(), point, DateTimeOffset.MaxValue);
            return point;
        }

        private PointF GetPointF(PointF point)
        {
            throw new NotImplementedException();
        }
    }
}