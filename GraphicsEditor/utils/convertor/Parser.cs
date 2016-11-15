using System;

namespace GraphicsEditor
{
    public abstract class Parser
    {
        public readonly Type OutputType;
        public abstract int ParametersNeeded { get; }

        protected Parser(Type outputType)
        {
            OutputType = outputType;
        }


        public abstract object Parse(string[] args);
    }
}