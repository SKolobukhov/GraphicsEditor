using System;

namespace DrawablesUI
{
    public abstract class Shape: IDrawable
    {
        private readonly Guid id;
        private Transformation transformation;

        protected Shape()
        {
            id = Guid.NewGuid();
            transformation = Transformation.Default;
        }

        public virtual void Draw(IDrawer drawer)
        {
            var lastMatrix = drawer.Transform ?? Transformation.Default.Matrix;
            if (!transformation.Matrix.IsIdentity)
            {
                var matrix = lastMatrix.Clone();
                matrix.Multiply(transformation.Matrix);
                drawer.Transform = matrix;
            }
            DrawShape(drawer);
            drawer.Transform = lastMatrix;
        }

        protected abstract void DrawShape(IDrawer drawer);

        public virtual void Transform(Transformation transformation)
        {
            this.transformation *= transformation;
        }

        public override bool Equals(object obj)
        {
            var shape = obj as Shape;
            if (shape == null)
            {
                return false;
            }
            return id == shape.id;
        }

        public override int GetHashCode()
        {
            return id.GetHashCode();
        }
    }
}