using System.Drawing;

namespace GraphicsEditor
{
    public class PointShapeParser: Parser<PointShape>
    {
        public override int ParametersNeeded => 2;

        protected override PointShape ParseParameter(string[] args)
        {
            var position = Convertor.Convert<PointF>(args);
            return new PointShape(position);
        }
    }
}