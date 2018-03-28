using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Test.Framework
{
    public static class EnumerableExtensions
    {
        public static bool IsNullOrEmpty<T>(this IEnumerable<T> collection)
        {
            return collection == null || collection.Count() == 0;
        }
    }
}
