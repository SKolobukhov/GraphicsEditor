using System;
using System.Drawing;

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

        public PointF this[PointF point] { get { throw new NotImplementedException(); } }
    }
}