using System;
using System.Collections.Generic;
using System.Linq;


namespace EphemeralEx.Extensions
{
    public static class CollectionExtensions
    {
        public static IEnumerable<S> OfType<S, T>(this IEnumerable<T> sequence)
            where S : T
        {
            foreach (var element in sequence)
            {
                if (element is S s)
                    yield return s;
            }
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

        public static TValue AddAndReturn<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key, TValue backup)
        {
            dictionary.Add(key, backup);
            return backup;
        }

        public static TValue GetOrAdd<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key, TValue alternative) 
            => dictionary.TryGetValue(key, out TValue val) 
                ? val 
                : dictionary.AddAndReturn(key, alternative);

        public static TValue GetOrAdd<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key, Func<TValue> generator)
            => dictionary.TryGetValue(key, out TValue val)
                ? val
                : dictionary.AddAndReturn(key, generator());
    }
}
