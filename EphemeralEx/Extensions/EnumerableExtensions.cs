﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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

        public static IReadOnlyDictionary<S, T> IndexBy<T, S>(this IEnumerable<T> sequence, Func<T, S> indexer)
                where S : notnull
            => sequence.ToDictionary(element => indexer(element), element => element);

        public static IEnumerable<T> NotNull<T>(this IEnumerable<T?> sequence)
        {
            foreach (var element in sequence)
            {
                if (element != null)
                    yield return element;
            }
        }

        public static IEnumerable<T> Flatten<T>(this IEnumerable<IEnumerable<T>> sequence)
            => sequence.SelectMany(subSequence => subSequence);

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

        public static IReadOnlyList<T> ToEnumerable<T>(this T element)
            => new List<T> { element };

        public static bool None<T>(this IEnumerable<T> sequence, Func<T, bool> predicate)
            => !sequence.Any(predicate);

        public static T ChainCall<T, S>(this T target, IEnumerable<S> sequence, Func<T, S, T> action)
            => sequence.Any()
                ? action(target, sequence.First())
                    .ChainCall(sequence.Skip(1), action)
                : target;

        public static IEnumerable<T> With<T>(this IEnumerable<T> sequence, T element)
        {
            foreach (var e in sequence)
                yield return e;

            yield return element;
        }

        public static T? FirstOrNull<T>(this IEnumerable<T> sequence)
            => sequence.FirstOrDefault();

        public static T? FirstOrNull<T>(this IEnumerable<T> sequence, Func<T, bool> predicate)
            => sequence.FirstOrDefault(predicate);

        public static T? SingleOrNull<T>(this IEnumerable<T> sequence)
            => sequence.SingleOrDefault();

        public static T? SingleOrNull<T>(this IEnumerable<T> sequence, Func<T, bool> predicate)
            => sequence.SingleOrDefault(predicate);

        // TODO: move to TaskExtensions when more are needed
        public static async Task<IEnumerable<T>> Complete<T>(this IEnumerable<Task<T>> sequence)
            => await Task.WhenAll(sequence);
    }
}
