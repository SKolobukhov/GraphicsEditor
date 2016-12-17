using System;
using System.Collections.Generic;
using DrawablesUI;

namespace GraphicsEditor
{
    public class ShapeProvider
    {
        private readonly Dictionary<string, Func<string[], IShape>> bindings = new Dictionary<string, Func<string[], IShape>>();

        public ShapeProvider Bind<T>(string shapeName)
            where T : IShape
        {
            if (bindings.ContainsKey(shapeName))
            {
                throw new ApplicationException("На это имя уже зарегистрирована фигура");
            }
            bindings.Add(shapeName, args => Convertor.Convert<T>(args));
            return this;
        }

        public IShape GetShape(string shapeName, params string[] parameters)
        {
            if (bindings.ContainsKey(shapeName))
            {
                try
                {
                    return bindings[shapeName](parameters);
                }
                catch (Exception exception)
                {
                    throw new ApplicationException($"Невозможно создать фигуру \"{shapeName}\": {exception.Message}", exception);
                }
            }
            throw new ApplicationException($"Неизвестная фигура \"{shapeName}\"");
        }
    }
}