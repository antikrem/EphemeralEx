using System.Collections.Generic;
using System.Linq;

namespace EphemeralEx.Extensions
{
    public static class Sequence
    {
        public static IEnumerable<T> From<T>(params IEnumerable<T>[] sequences)
            => sequences.SelectMany(sequence => sequence);
    }
}
