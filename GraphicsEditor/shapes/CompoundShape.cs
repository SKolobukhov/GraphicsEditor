using System.Collections.Generic;
using System.Linq;
using DrawablesUI;

namespace GraphicsEditor
{
    public class CompoundShape : IShape
    {
        protected readonly object locker = new object();

        public IList<IShape> Shapes { get; private set; }


        public CompoundShape(IEnumerable<IShape> shapes = null)
        {
            Shapes = shapes?.ToList() ?? new List<IShape>();
        }

        public virtual void Draw(IDrawer drawer)
        {
            lock (locker)
            {
                drawer.StartDraw();
                foreach (var shape in Shapes)
                {
                    shape.Draw(drawer);
                }
                drawer.EndDraw();
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