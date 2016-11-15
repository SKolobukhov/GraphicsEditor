using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace GraphicsEditor
{
    public static class Convertor
    {
        private static readonly Dictionary<string, Parser> parsersMap = new Dictionary<string, Parser>();


        static Convertor()
        {
            var parserType = typeof(Parser);
            var parserTType = typeof(Parser<>);
            Assembly.GetExecutingAssembly()
                .GetTypes()
                .Where(type => type.IsSubclassOf(parserType) && type != parserTType)
                .Select(type => (Parser)type.GetConstructor(new Type[0])?.Invoke(new object[0]))
                .Where(parser => parser != null)
                .ForEach(Bind);
        }

        private static void Bind(Parser parser)
        {
            if (parsersMap.ContainsKey(parser.OutputType.Name))
            {
                throw new ApplicationException($"Parser to {parser.OutputType.Name} has alread binded");
            }
            parsersMap[parser.OutputType.Name] = parser;
        }

        public static int GetParametersNeededCount<T>()
        {
            return GetParametersNeededCount(typeof(T));
        }

        private static int GetParametersNeededCount(Type toType)
        {
            if (parsersMap.ContainsKey(toType.Name))
            {
                return parsersMap[toType.Name].ParametersNeeded;
            }
            return -1;
        }

        public static T Convert<T>(string[] args)
        {
            return (T)Convert(typeof(T), args);
        }

        public static object Convert(Type toType, string[] args)
        {
            if (parsersMap.ContainsKey(toType.Name))
            {
                if (args.Length != GetParametersNeededCount(toType))
                {
                    throw new ApplicationException($"Неверное количество параметров: {args.Length}");
                }
                return parsersMap[toType.Name].Parse(args);
            }
            return null;
        }
    }
}