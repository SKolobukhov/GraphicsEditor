using System.Drawing;
using System.Linq;

namespace GraphicsEditor
{
    public class CircleShapeParser: Parser<CircleShape>
    {
        public override int ParametersNeeded => 3;

        protected override CircleShape ParseParameter(string[] args)
        {
            var center = Convertor.Convert<PointF>(args.Take(2).ToArray());
            var radius = Convertor.Convert<float>(args.Skip(2).ToArray());
            return new CircleShape(center, radius);
        }
    }
}