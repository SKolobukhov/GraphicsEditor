using System;
using System.Drawing;
using System.Runtime.Caching;

namespace DrawablesUI
{
    public class Transformation
    {
        public readonly System.Drawing.Drawing2D.Matrix transformationMatrix;

        public PointF this[PointF point] => Transform(point);

        public Vector this[Vector vector] => Transform(vector);

        public Transformation()
        {
            transformationMatrix = new System.Drawing.Drawing2D.Matrix();
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
            result.Translate(point.X, point.Y);
            result.Scale(scaleFactor, scaleFactor);
            result.Translate(-point.X, -point.Y);
            return new Transformation(result);
        }

        public static Transformation Scale(PointF point1, PointF point2, float scaleFactor)
        {
            if (point1 == point2)
            {
                throw new InvalidOperationException();
            }
            var rotateFactor = Math.Atan((point1.X - point2.X) / (point1.Y - point2.Y));
            var result = new System.Drawing.Drawing2D.Matrix();
            result.Translate();
        }
        
        public static Transformation operator*(Transformation transformation1, Transformation transformation2)
        {
            var result = transformation1.transformationMatrix.Clone();
            result.Multiply(transformation2.transformationMatrix);
            return new Transformation(result);
        }

        private Vector Transform(Vector vector)
        {
            var result = new PointF[1] { vector.value };
            transformationMatrix.TransformVectors(result);
            return Vector.ToVector(result[0]);
        }

        private PointF Transform(PointF point)
        {
            var result = new PointF[1] { point };
            transformationMatrix.TransformPoints(result);
            return result[0];
        }

    }
}