using System;
using System.Collections.Generic;


namespace EphemeralEx.Extensions
{
    public static class DictionaryExtensions
    {
        public static TValue AddAndReturn<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key, TValue backup)
            where TKey : notnull
        {
            dictionary.Add(key, backup);
            return backup;
        }

        public static TValue GetOrAdd<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key, TValue alternative)
            where TKey : notnull
            => dictionary.TryGetValue(key, out TValue? val)
                ? val
                : dictionary.AddAndReturn(key, alternative);

        public static TValue GetOrAdd<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key, Func<TValue> generator)
            where TKey : notnull
            => dictionary.TryGetValue(key, out TValue? val)
                ? val
                : dictionary.AddAndReturn(key, generator());
    }
}
