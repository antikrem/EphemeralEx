using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;


namespace EphemeralEx.Extensions
{
    public class MatchFirstFailed : Exception { }

    public static class StringExtensions
    {
        public static string MatchFirst(this string value, string regex)
        {
            try
            {
                return Regex.Match(value, regex).Groups[1].Captures[0].Value;
            }
            catch (Exception)
            {
                throw new MatchFirstFailed();
            }
        }

        public static string Filter(this string value, params string[] token)
            => token.Length == 0 ? value : value.Replace(token[0], "").Filter(token[1..]);

        public static string Concat(this IEnumerable<char> chars)
            => string.Concat(chars);

        public static string JoinWith(this IEnumerable<string> strs, string seperator)
            => string.Join(seperator, strs);
    }
}
