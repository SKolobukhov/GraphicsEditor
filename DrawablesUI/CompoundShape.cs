using System.Collections.Generic;

namespace DrawablesUI
{
    public class CompoundShape : IShape
    {
        public IList<IShape> Shapes { get; private set; }
    }
}