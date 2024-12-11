using System;

namespace Core.Kernel.Helper
{
    public static class HashHelper
    {
        public static string? Encode(this byte[]? data) => data is null ? null : Convert.ToBase64String(data);
        public static byte[]? Decode(this string? data) => string.IsNullOrWhiteSpace(data) ? null : Convert.FromBase64String(data);
    }
}
