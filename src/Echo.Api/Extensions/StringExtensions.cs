using System.Text.RegularExpressions;

namespace Echo.Api.Extensions;

public static class StringExtensions
{
    public static string NormalizeToHttpHeaderName(this string input)
    {
        return Regex.Replace(input, "[^a-zA-Z0-9_\\-]", "", RegexOptions.Compiled);
    }
}