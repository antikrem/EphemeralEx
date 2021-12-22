using System;
using System.Collections.Generic;
using System.Linq;


namespace EphemeralEx.Extensions
{
    public static class ExpressionExtensions
    {
        static public T Do<T>(this T obj, Action<T> action)
        {
            action(obj);
            return obj;
        }

        static public IEnumerable<S> Collect<T, S>(this T obj, params Func<T, S>[] functions)
            where T : notnull
        {
            return functions.Select(function => function(obj));
        }
    }
}
