using System.Drawing;
using System.Linq;

namespace GraphicsEditor
{
    public class EllipseShapeParser: Parser<EllipseShape>
    {
        public override int ParametersNeeded => 5;

        protected override EllipseShape ParseParameter(string[] args)
        {
            var center = Convertor.Convert<PointF>(args.Take(2).ToArray());
            args = args.Skip(2).ToArray();

            var size = Convertor.Convert<SizeF>(args.Take(2).ToArray());
            args = args.Skip(2).ToArray();

            var rotate = Convertor.Convert<float>(args);

            return new EllipseShape(center, size, rotate);
        }
    }
}