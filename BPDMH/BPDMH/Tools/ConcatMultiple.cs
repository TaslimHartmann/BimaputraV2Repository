using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BPDMH.Tools
{
    /// <summary>
    /// http://stackoverflow.com/questions/4961910/appending-concatenating-two-ienumerable-sequences
    /// </summary>
    /// 
    public static class ConcatMultiple
    {
        public static IEnumerable<TSource> ConcatMultiples<TSource>(this IEnumerable<TSource> first, params IEnumerable<TSource>[] source)
        {
            if (first == null)
                throw new ArgumentNullException("first");

            if (source.Any(x => (x == null)))
                throw new ArgumentNullException("source");

            return ConcatIterator<TSource>(source);
        }

        private static IEnumerable<TSource> ConcatIterator<TSource>(IEnumerable<TSource> first, params IEnumerable<TSource>[] source)
        {
            foreach (var iteratorVariable in first)
                yield return iteratorVariable;

            foreach (var enumerable in source)
            {
                foreach (var iteratorVariable in enumerable)
                    yield return iteratorVariable;
            }
        }

        /// <summary>
        /// Concatenates multiple sequences
        /// </summary>
        /// <typeparam name="TSource">The type of the elements of the input sequences.</typeparam>        
        /// <param name="source">The sequences to concatenate.</param>
        /// <returns></returns>
        public static IEnumerable<TSource> ConcatMultipless<TSource>(params IEnumerable<TSource>[] source)
        {
            if (source.Any(x => (x == null)))
                throw new ArgumentNullException("source");

            return ConcatIterator<TSource>(source);
        }

        private static IEnumerable<TSource> ConcatIterator<TSource>(params IEnumerable<TSource>[] source)
        {
            foreach (var enumerable in source)
            {
                foreach (var iteratorVariable in enumerable)
                    yield return iteratorVariable;
            }
        }

    }

}
