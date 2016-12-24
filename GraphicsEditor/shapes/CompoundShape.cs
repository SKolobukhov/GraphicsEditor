using System.Collections.Generic;
using DrawablesUI;

namespace GraphicsEditor
{
    public class CompoundShape : IShape
    {
        protected readonly object locker = new object();

        public List<IShape> Shapes { get; private set; }


        public CompoundShape(List<IShape> shapes)
        {
            Shapes = shapes ?? new List<IShape>();
        }

        public virtual void Draw(IDrawer drawer)
        {
            lock (locker)
            {
                foreach (var shape in Shapes)
                {
                    shape.Draw(drawer);
                }
            }
        }

        public virtual void Transform(Transformation transformation)
        {
            lock (locker)
            {
                foreach (var shape in Shapes)
                {
                    shape.Transform(transformation);
                }
            }
        }
    }
}