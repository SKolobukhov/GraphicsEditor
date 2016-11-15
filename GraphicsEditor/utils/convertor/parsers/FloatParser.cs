using System;

namespace GraphicsEditor
{
    public class FloatParser: Parser<float>
    {
        public override int ParametersNeeded => 1;

        protected override float ParseParameter(string[] args)
        {
            float value;
            if (!float.TryParse(args[0], out value))
            {
                throw new ApplicationException($"Неверный параметр \"{args[0]}\"");
            }
            return value;
        }
    }
}