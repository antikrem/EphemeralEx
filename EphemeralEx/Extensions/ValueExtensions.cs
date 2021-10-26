using System;


namespace EphemeralEx.Extensions
{
    public static class ValueExtensions
    {
        public static string ToHexString(this ulong value)
            => string.Format("0x{0:X}", value);

        public static ulong ToHexULong(this string value)
            => Convert.ToUInt64(value, 16);

        public static string ToHexString(this byte[] value)
            => BitConverter.ToString(value)
                .Replace("-", "")
                .ToLower();
    }
}
