using System;
using System.Collections.Generic;

namespace GraphicsEditor
{
    public static class EnumerableExtensions
    {
        public static void ForEach<T>(this IEnumerable<T> collection, Action<T> action)
        {
            if (collection == null)
            {
                return;
            }
            foreach (var element in collection)
            {
                action(element);
            }
        }  
    }
}