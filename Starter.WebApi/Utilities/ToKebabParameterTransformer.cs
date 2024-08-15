using System.Text.RegularExpressions;

namespace Starter.WebApi.Utilities;

/// <summary>
/// Use kebab case for route
/// </summary>
public partial class ToKebabParameterTransformer : IOutboundParameterTransformer
{
    /// <summary>
    /// Convert pascal case to kebab case
    /// </summary>
    /// <param name="value">Initial route value</param>
    /// <returns>Route with kebab case as naming convention</returns>
    public string TransformOutbound(object? value)
    {
        return MatchLowercaseThenUppercase()
            .Replace(value?.ToString() ?? "", "$1-$2")
            .ToLower();
    }

    [GeneratedRegex("([a-z])([A-Z])")]
    private static partial Regex MatchLowercaseThenUppercase();
}
