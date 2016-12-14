using System;

namespace DrawablesUI
{
    public abstract class Shape: IDrawable
    {
        private readonly Guid id;

        protected Shape()
        {
            id = Guid.NewGuid();
        }

        public virtual void Draw(IDrawer drawer){}

        public virtual void Transform(Transformation transformation){}

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