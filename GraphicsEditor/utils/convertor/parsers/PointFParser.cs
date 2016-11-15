using System.Drawing;

namespace GraphicsEditor
{
    public class PointFParser : Parser<PointF>
    {
        public override int ParametersNeeded => 2;

        protected override PointF ParseParameter(string[] args)
        {
            var x = Convertor.Convert<float>(new[] {args[0]});
            var y = Convertor.Convert<float>(new[] {args[1]});
            return new PointF(x, y);
        }
    }
}