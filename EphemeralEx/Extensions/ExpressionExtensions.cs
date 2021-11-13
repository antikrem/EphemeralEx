using System;


namespace EphemeralEx.Extensions
{
    public static class ExpressionExtensions
    {
        static public T Do<T>(this T obj, Action<T> action)
        {
            action(obj);
            return obj;
        }
    }
}
