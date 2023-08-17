using System;
using System.Collections.Generic;
using System.Linq;

namespace TUTOR.Biz.Extensions
{
    public static class StringExtension
    {
        public static string ToCamelCase(this string str)
        {
            if (string.IsNullOrEmpty(str) || char.IsLower(str, 0))
            {
                return str;
            }
            else
            {
                return char.ToLowerInvariant(str[0]) + str.Substring(1);
            }
        }

        public static IEnumerable<T> ToArray<T>(this string str)
        {
            if (string.IsNullOrEmpty(str))
            {
                return null;
            }
            else
            {
                return str.Split(',').Select(value =>
                {
                    return (T)Convert.ChangeType(value, typeof(T));
                });
            }
        }

        public static int ToInt(this string str)
        {
            if (string.IsNullOrEmpty(str))
            {
                return default;
            }
            else if (int.TryParse(str, out var i))
            {
                return i;
            }
            else
            {
                return default;
            }
        }

        public static Guid? ToGuid(this string str)
        {
            if (string.IsNullOrEmpty(str))
            {
                return null;
            }
            else
            {
                if (Guid.TryParse(str, out var guid))
                {
                    return guid;
                }
                else
                {
                    return null;
                }
            }
        }

        public static string GetNullIfEmpty(this string str)
        {
            if (string.IsNullOrEmpty(str))
            {
                return null;
            }
            else
            {
                return str;
            }
        }
    }
}