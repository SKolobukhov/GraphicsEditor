using System;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace DrawablesUI
{
    public class Transformation
    {
        public static Transformation Default => new Transformation(new Matrix());

        public static Transformation Rotate(PointF point, float angle)
        {
            var result = new Matrix();
            result.RotateAt(angle, point);
            return new Transformation(result);
        }

        public static Transformation Translate(PointF point)
        {
            var matrix = new Matrix();
            matrix.Translate(point.X, point.Y);
            return new Transformation(matrix);
        }

        public static Transformation Scale(PointF point, float scaleFactor)
        {
            var result = new Matrix();
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

        public static Transformation operator *(Transformation transformation1, Transformation transformation2)
        {
            var matrix = transformation1.Matrix.Clone();
            matrix.Multiply(transformation2.Matrix);
            return new Transformation(matrix);
        }

        public static Transformation Invert(Transformation transformation)
        {
            var matrix = transformation.Matrix.Clone();
            matrix.Invert();
            return new Transformation(matrix);
        }


        public readonly Matrix Matrix;

        private Transformation(Matrix matrix)
        {
            Matrix = matrix;
        }
    }
}