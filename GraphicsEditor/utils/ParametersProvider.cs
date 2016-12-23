using System;
using System.Collections.Generic;
using System.Linq;

namespace GraphicsEditor
{
    public class ParametersProvider
    {
        private readonly IEnumerator<string> enumerator;
        public bool IsEmpty { get; private set; }

        public ParametersProvider(IEnumerable<string> parameters)
        {
            IsEmpty = false;
            enumerator = (parameters ?? new string[0]).GetEnumerator();
        }


        public string GetParameter()
        {
            if (enumerator.MoveNext())
            {
                return enumerator.Current;
            }
            IsEmpty = true;
            throw new ApplicationException("Parameters finished");
        }

        public string[] GetParameters(int count)
        {
			return new string[count].Select(_ => GetParameter()).ToArray();
        }

        public string[] RemainingParameters()
        {
			var result = new List<string>();
            while (enumerator.MoveNext())
            {
                result.Add(enumerator.Current);
            }
	        return result.ToArray();
        }
    }
}