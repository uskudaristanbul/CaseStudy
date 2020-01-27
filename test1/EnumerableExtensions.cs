using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace test1
{
    public static class EnumerableExtensions
    {
        public static IEnumerable<IEnumerable<T>> CrossProduct<T>(
        this IEnumerable<IEnumerable<T>> source) =>
        source.Aggregate(
            (IEnumerable<IEnumerable<T>>)new[] { Enumerable.Empty<T>() },

            (acc, src) => src.SelectMany(x => acc.Select(a => a.Concat(new[] { x }))) );
        
     
    }
}
