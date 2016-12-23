using DrawablesUI;

namespace GraphicsEditor
{
    public class ShapeWrapper
    {
        public readonly IShape Shape;
        public readonly CompoundShapeWrapper Parent;

        public ShapeWrapper(IShape shape, CompoundShapeWrapper parent)
        {
            Shape = shape;
            Parent = parent;
        }

	    public string GetIndex()
	    {
		    return '[' + GetIndex(string.Empty, Shape, Parent).TrimEnd(':') + ']';
	    }

	    private string GetIndex(string prefix, IShape shape, CompoundShapeWrapper parent)
	    {
		    if (parent == null)
		    {
			    return prefix;
		    }
		    var index = parent.Shapes.IndexOf(shape);
		    return GetIndex(index + ":" + prefix, parent.Shape, parent.Parent);
	    }
    }
}