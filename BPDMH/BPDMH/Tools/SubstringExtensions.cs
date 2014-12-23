using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BPDMH.Tools
{
    /// <summary>
    /// http://www.dotnetperls.com/between-before-after
    /// </summary>

    internal static class SubstringExtensions
    {
        /// <summary>
        /// Get string value between [first] a and [last] b.
        /// </summary>
        public static string Between(this string value, string a, string b)
        {
            var posA = value.IndexOf(a);
            var posB = value.LastIndexOf(b);
            if (posA == -1)
            {
                return "";
            }
            if (posB == -1)
            {
                return "";
            }
            int adjustedPosA = posA + a.Length;
            if (adjustedPosA >= posB)
            {
                return "";
            }
            return value.Substring(adjustedPosA, posB - adjustedPosA);
        }

        /// <summary>
        /// Get string value after [first] a.
        /// </summary>
        public static string Before(this string value, string a)
        {
            var posA = value.IndexOf(a);
            return posA == -1 ? "" : value.Substring(0, posA);
        }

        /// <summary>
        /// Get string value after [last] a.
        /// </summary>
        public static string After(this string value, string a)
        {
            var posA = value.LastIndexOf(a);
            if (posA == -1)
            {
                return "";
            }

            var adjustedPosA = posA + a.Length;
            return adjustedPosA >= value.Length ? "" : value.Substring(adjustedPosA);
        }
    }
}
