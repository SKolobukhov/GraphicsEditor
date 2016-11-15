using System.Linq;

namespace GraphicsEditor
{
    public class StringParser: Parser<string>
    {
        public override int ParametersNeeded => 1;

        protected override string ParseParameter(string[] args)
        {
            return args.First();
        }
    }
}