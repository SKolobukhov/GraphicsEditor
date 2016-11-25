using System.Collections.Generic;

namespace DrawablesUI
{
    public class CompoundShape : IShape
    {
        protected readonly object locker = new object();

        public IList<IShape> Shapes { get; private set; }


        public CompoundShape()
        {
            Shapes = new List<IShape>();
        }

        public void Draw(IDrawer drawer)
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

        public void Transform(Transformation transformation)
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