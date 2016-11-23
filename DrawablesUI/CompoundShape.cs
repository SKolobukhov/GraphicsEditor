using System;
using System.Collections.Generic;

namespace DrawablesUI
{
    public class CompoundShape : IShape
    {
        public IList<IShape> Shapes { get; private set; }

        public void Draw(IDrawer drawer)
        {
            throw new NotImplementedException();
        }

        public void Transform(Transformation transformation)
        {
            throw new NotImplementedException();
        }
    }
}