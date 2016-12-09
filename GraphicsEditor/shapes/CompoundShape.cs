using System.Collections.Generic;
using System.Linq;
using DrawablesUI;

namespace GraphicsEditor
{
    public class CompoundShape : Shape
    {
        protected readonly object locker = new object();

        public IList<Shape> Shapes { get; private set; }


        public CompoundShape(IEnumerable<Shape> shapes = null)
        {
            Shapes = shapes?.ToList() ?? new List<Shape>();
        }

        protected override void DrawShape(IDrawer drawer)
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
    }
}