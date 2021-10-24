using System;
using System.Linq;

using EphemeralEx.Extensions;


namespace EphemeralEx.Tests
{
    public static class Dummy
    {
        static readonly Random _random =  new();

        public static int Int(int bottom = 0, int top = int.MaxValue)
            => _random.Next(bottom, top);

        public static string String(int length = 16)
            => Enumerable.Range(0, length)
                .Select(_ => (char)_random.Next('a', 'z'))
                .Concat();
    }
}
