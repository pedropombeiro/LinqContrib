// --------------------------------------------------------------------------------------------------------------------
// <copyright file="LinqExtensions.cs" company="Developer In The Flow">
//   © 2012-2013 Pedro Pombeiro
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace Linq.Contrib
{
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;

    public static class LinqExtensions
    {
        #region Public Methods and Operators

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

        #endregion
    }
}