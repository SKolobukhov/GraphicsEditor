using System;

namespace GraphicsEditor
{
    public abstract class Parser<T>: Parser
    {
        protected Parser()
            : base(typeof(T))
        { }

        public override object Parse(string[] args)
        {
            if (args.Length != ParametersNeeded)
            {
                throw new ApplicationException($"Неверное количество параметров: {args.Length}");
            }
            return ParseParameter(args);
        }

        protected abstract T ParseParameter(string[] args);
    }
}