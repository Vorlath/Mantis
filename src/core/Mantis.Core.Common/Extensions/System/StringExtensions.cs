namespace Mantis.Core.Common.Extensions.System
{
    public static class StringExtensions
    {
        public static string TrimEnd(this string source, string value)
        {
            if (!source.EndsWith(value))
            {
                return source;
            }


            return source[..source.LastIndexOf(value)];
        }

        public static bool IsNullOrEmpty(this string? value)
        {
            return value is null || value == string.Empty;
        }

        public static bool IsNotNullOrEmpty(this string? value)
        {
            return value is not null && value != string.Empty;
        }
    }
}