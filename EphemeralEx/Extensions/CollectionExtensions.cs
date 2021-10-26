using System;
using System.Collections.Generic;
using System.Linq;


namespace EphemeralEx.Extensions
{
    public static class CollectionExtensions
    {
        public static void ForEach<T>(this IEnumerable<T> sequence, Action<T> action)
        {
            foreach (T t in sequence)
            {
                action(t);
            }
        }

        public static IEnumerable<T> ToEnumerable<T>(this T element)
            => new List<T> { element };

        public static bool None<T>(this IEnumerable<T> sequence, Func<T, bool> predicate)
            => !sequence.Any(predicate);

        public static T ChainCall<T, S>(this T target, IEnumerable<S> sequence, Func<T, S, T> action)
            => sequence.Any()
                ? action(target, sequence.First())
                    .ChainCall(sequence.Skip(1), action)
                : target;
    }
}
