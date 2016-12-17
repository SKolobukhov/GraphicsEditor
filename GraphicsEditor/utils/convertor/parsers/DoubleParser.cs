using System;

namespace GraphicsEditor
{
    public class DoubleParser: Parser<double>
    {
        public override int ParametersNeeded => 1;

        protected override double ParseParameter(string[] args)
        {
            double value;
            if (!double.TryParse(args[0].Replace('.', ','), out value))
            {
                throw new ApplicationException($"Неверный параметр \"{args[0]}\"");
            }
            return value;
        }
    }
}