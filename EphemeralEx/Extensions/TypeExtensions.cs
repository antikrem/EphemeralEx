using System;


namespace EphemeralEx.Extensions
{
    static class TypeExtensions
    {
        public static bool IsSimple(this Type type)
            => type.IsPrimitive
                || type.IsEnum
                || type.Equals(typeof(string))
                || type.Equals(typeof(decimal))
                || type.Equals(typeof(TimeSpan));

        public static bool IsAttributed<T>(this Type type) where T : Attribute
            => type.IsDefined(typeof(T), false);
    }
}
