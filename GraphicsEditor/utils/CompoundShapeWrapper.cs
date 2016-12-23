using System.Collections.Generic;
using DrawablesUI;

namespace GraphicsEditor
{
    public class CompoundShapeWrapper: ShapeWrapper
    {
        private readonly CompoundShape compoundShape;

        public List<IShape> Shapes => compoundShape.Shapes;


        public CompoundShapeWrapper(CompoundShape compoundShape, CompoundShapeWrapper parent)
            : base(compoundShape, parent)
        {
            this.compoundShape = compoundShape;
        }
    }
}