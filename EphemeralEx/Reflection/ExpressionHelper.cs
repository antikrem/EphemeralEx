using EphemeralEx.Extensions;
using System;
using System.Linq.Expressions;
using System.Reflection;


namespace EphemeralEx.Reflection
{
    public static class ExpressionHelper
    {
        public static PropertyInfo SelectProperty<T>(Expression<Func<T, object>> select)
            => typeof(T).GetProperty(GetMemberName(select)) ?? throw new ExpressionHelperException();

        private static string GetMemberName<T>(Expression<Func<T, object>> selector)
            => (selector.Body as MemberExpression)?.Member.Name ?? throw new ExpressionHelperException();
    }

    public class ExpressionHelperException : Exception
    {
        public ExpressionHelperException()
            : base("Invalid selection function")
        { }
    }
}
