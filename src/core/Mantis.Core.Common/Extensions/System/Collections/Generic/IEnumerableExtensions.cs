namespace Mantis.Core.Common.Extensions.System.Collections.Generic
{
    public static class IEnumerableExtensions
    {
        public static IEnumerable<T> Concat<T>(this IEnumerable<T> collection, T item)
        {
            return collection.Concat([item]);
        }
    }
}