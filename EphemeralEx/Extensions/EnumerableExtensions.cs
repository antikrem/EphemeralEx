using System;
using System.Collections.Generic;
using System.Linq;

namespace EphemeralEx.Extensions
{
    public static class EnumerableExtensions
    {
        public static IEnumerable<S> OfType<T, S>(this IEnumerable<T> sequence)
            where S : T
        {
            foreach (var element in sequence)
            {
                if (element is S s)
                    yield return s;
            }
        }

        public static IEnumerable<S> SelectSuccessful<E, T, S>(
            this IEnumerable<T> sequence,
            Func<T, S> selector,
            Action<T, E>? errorHandler = null)
            where E : Exception
        {
            var results = new List<S>();

            foreach (var element in sequence)
            {
                try
                {
                    results.Add(selector(element));
                }
                catch (E exception)
                {
                    errorHandler?.Invoke(element, exception);
                }
            }

            return results;
        }

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
