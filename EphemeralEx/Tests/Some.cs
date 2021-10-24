using System;

namespace EphemeralEx.Tests
{
    public static class Some
    {
        static readonly Random _random =  new();

        public static int Int(int bottom = 0, int top = int.MaxValue)
            => _random.Next(bottom, top);
    }
}
