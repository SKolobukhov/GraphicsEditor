using System.Drawing;

namespace GraphicsEditor
{
    public class SizeFParser : Parser<SizeF>
    {
        public override int ParametersNeeded => 2;

        protected override SizeF ParseParameter(string[] args)
        {
            var x = Convertor.Convert<float>(new[] { args[0] });
            var y = Convertor.Convert<float>(new[] { args[1] });
            return new SizeF(x, y);
        }
    }
}