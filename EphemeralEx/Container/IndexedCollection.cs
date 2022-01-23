using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace EphemeralEx.Container
{
    public class IndexedCollection<T, S>
        where T : notnull
    {
        private readonly Func<S, T> _indexer;
        private readonly Dictionary<T, S> _inner = new();

        public IndexedCollection(Func<S, T> indexer)
        {
            _indexer = indexer;
        }

        public S this[T key]
        {
            get => _inner[key];
        }

        public S Get(T key) => _inner[key];

        public bool TryGet(T key, [MaybeNullWhen(false)] out S value)
        {
            if (_inner.TryGetValue(key, out var innerValue))
            {
                value = innerValue;
                return true;
            }

            value = default;
            return false;
        }

        public ICollection<T> Keys
            => _inner.Keys;

        public ICollection<S> Values
            => _inner.Values;

        public int Count
            => _inner.Count;

        public void Add(S value)
            => _inner.Add(_indexer(value), value);

        public void Clear()
            => _inner.Clear();

        public bool Contains(T index)
            => _inner.ContainsKey(index);

        public bool ContainsItem(S item)
            => _inner.ContainsValue(item);

        public bool Remove(T key)
            => _inner.Remove(key);

        public bool Remove(S item)
            => _inner.Remove(_indexer(item));
    }
}
