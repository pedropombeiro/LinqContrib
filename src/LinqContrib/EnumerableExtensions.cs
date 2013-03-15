// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EnumerableExtensions.cs" company="Developer In The Flow">
//   © 2013 Pedro Pombeiro
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace LinqContrib
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;

    public static class EnumerableExtensions
    {
        #region Public Methods and Operators

        public static bool Any(this IEnumerable source)
        {
            return Enumerable.Any(source.Cast<object>());
        }

        public static bool AnyOfType<TSource>(this IEnumerable source)
        {
            return source.OfType<TSource>().Any();
        }

        public static TSource FirstOfType<TSource>(this IEnumerable source)
        {
            return source.OfType<TSource>().First();
        }

        public static TSource FirstOrDefaultOfType<TSource>(this IEnumerable source)
        {
            return source.OfType<TSource>().FirstOrDefault();
        }

        public static bool IsCountEqual<TSource>(this IEnumerable<TSource> source, int expectedCount)
        {
            return source.Take(expectedCount + 1).Count() == expectedCount;
        }

        public static bool IsCountEqual<TSource>(this IEnumerable<TSource> source, int expectedCount, Func<TSource, bool> predicate)
        {
            return source.Where(predicate).Take(expectedCount + 1).Count() == expectedCount;
        }

        public static bool IsCountGreater<TSource>(this IEnumerable<TSource> source, int comparisonCount)
        {
            return source.Skip(comparisonCount).Any();
        }

        public static bool IsCountGreater<TSource>(this IEnumerable<TSource> source, int comparisonCount, Func<TSource, bool> predicate)
        {
            return source.Where(predicate).Skip(comparisonCount).Any();
        }

        public static bool IsCountSmaller<TSource>(this IEnumerable<TSource> source, int comparisonCount)
        {
            return !source.Skip(comparisonCount - 1).Any();
        }

        public static bool IsCountSmaller<TSource>(this IEnumerable<TSource> source, int comparisonCount, Func<TSource, bool> predicate)
        {
            return !source.Where(predicate).Skip(comparisonCount - 1).Any();
        }

        public static IEnumerable NotOfType<TSource>(this IEnumerable source)
        {
            return source.Cast<object>().Where(x => !(x is TSource));
        }

        public static IEnumerable<IEnumerable<TSource>> Segment<TSource>(this IEnumerable<TSource> source, int segmentSize)
        {
            var index = 0;

            for (;;)
            {
// ReSharper disable PossibleMultipleEnumeration
                var sourceSegment = source.Skip(index++ * segmentSize)
                                          .Take(segmentSize);
                if (!sourceSegment.Any())
                {
                    yield break;
                }

                yield return sourceSegment;

// ReSharper restore PossibleMultipleEnumeration
            }
        }

        public static TSource Single<TSource, TException>(this IEnumerable<TSource> source, Func<TSource, bool> predicate, Func<TException> exceptionToThrow)
            where TException : Exception
        {
            switch (source.Count(predicate))
            {
                case 1:
                    return source.Single(predicate);
                default:
                    throw exceptionToThrow();
            }
        }

        public static TSource SingleOfType<TSource>(this IEnumerable source)
        {
            return source.OfType<TSource>().Single();
        }

        public static TSource SingleOrDefaultOfType<TSource>(this IEnumerable source)
        {
            return source.OfType<TSource>().SingleOrDefault();
        }

        public static TSource SingleOrThrow<TSource, TException>(this IEnumerable<TSource> source, Func<TException> exceptionToThrow)
            where TException : Exception
        {
            switch (source.Take(2).Count())
            {
                case 1:
                    return source.First();
                default:
                    throw exceptionToThrow();
            }
        }

        #endregion
    }
}