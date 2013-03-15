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

        public static object SingleOrThrow<T, TException>(this IEnumerable<T> source, Func<Exception, TException> exceptionToThrow ) where TException : Exception
        {
            var singleElement = default(T);
            try
            {
                singleElement = source.Single();
            }
            catch (Exception e)
            {
                exceptionToThrow(e);
            }

            return singleElement;
        }

        public static bool IsCountEqual<T>(this IEnumerable<T> source, int expectedCount)
        {            
            return source.Take(expectedCount + 1).Count() == expectedCount;
        }

        public static bool IsCountGreater<T>(this IEnumerable<T> source, int comparisonCount)
        {           
            return source.Skip(comparisonCount).Any();
        }

        public static bool IsCountSmaller<T>(this IEnumerable<T> source, int comparisonCount)
        {
            return !source.Skip(comparisonCount-1).Any();
        }

        public static bool Any(this IEnumerable source)
        {
            return Enumerable.Any(source.Cast<object>());
        }

        public static bool AnyOfType<T>(this IEnumerable source)
        {
            return source.OfType<T>().Any();
        }

        public static T FirstOfType<T>(this IEnumerable source)
        {
            return source.OfType<T>().First();
        }

        public static T FirstOrDefaultOfType<T>(this IEnumerable source)
        {
            return source.OfType<T>().FirstOrDefault();
        }

        public static IEnumerable NotOfType<T>(this IEnumerable source)
        {
            return source.Cast<object>().Where(x => !(x is T));
        }

        public static IEnumerable<IEnumerable<T>> Segment<T>(this IEnumerable<T> source, int segmentSize)
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

        public static T SingleOfType<T>(this IEnumerable source)
        {
            return source.OfType<T>().Single();
        }

        public static T SingleOrDefaultOfType<T>(this IEnumerable source)
        {
            return source.OfType<T>().SingleOrDefault();
        }

        #endregion
    }
}