using System.Drawing;
using System.Linq;

namespace GraphicsEditor
{
    public class LineShapeParser: Parser<LineShape>
    {
        public override int ParametersNeeded => 4;
        protected override LineShape ParseParameter(string[] args)
        {
            var start = Convertor.Convert<PointF>(args.Take(2).ToArray());
            args = args.Skip(2).ToArray();

            var end = Convertor.Convert<PointF>(args);

            return new LineShape(start, end);
        }
    }
}