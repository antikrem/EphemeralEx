using System;
using System.Reflection;


namespace EphemeralEx.Extensions
{
    public static class MemberInfoExtensions
    {
        public static bool IsAttributed<T>(this MemberInfo memberInfo) where T : Attribute
            => memberInfo.IsDefined(typeof(T), false);
    }
}
