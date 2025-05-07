namespace Mantis.Core.Common.Extensions.System.Collections.Generic
{
    public static class DictionaryExtensions
    {
        public static Dictionary<TKey, TValue> Clone<TKey, TValue>(this Dictionary<TKey, TValue> source)
            where TKey : notnull
        {
            return source.ToDictionary(kvp => kvp.Key, kvp => kvp.Value);
        }

        public static Dictionary<TKey, TValue> Merge<TKey, TValue>(this Dictionary<TKey, TValue> source, Dictionary<TKey, TValue>? value, Func<TValue, TValue, bool>? overwrite = null)
            where TKey : notnull
        {
            if (value is null)
            {
                return source;
            }

            foreach (KeyValuePair<TKey, TValue> kvp in value)
            {
                if (overwrite is null)
                {
                    source[kvp.Key] = kvp.Value;
                    continue;
                }

                if (source.TryGetValue(kvp.Key, out TValue? sourceValue) == false)
                {
                    source[kvp.Key] = kvp.Value;
                    continue;
                }

                if (overwrite(sourceValue, kvp.Value))
                {
                    source[kvp.Key] = kvp.Value;
                    continue;
                }
            }

            return source;
        }
    }
}