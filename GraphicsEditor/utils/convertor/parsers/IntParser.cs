using System;

namespace GraphicsEditor
{
    public class IntParser : Parser<int>
    {
        public override int ParametersNeeded => 1;

        protected override int ParseParameter(string[] args)
        {
            int value;
            if (!int.TryParse(args[0], out value))
            {
                throw new ApplicationException($"Неверный параметр \"{args[0]}\"");
            }
            return value;
        }
    }
}