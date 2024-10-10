using System.Text.RegularExpressions;

namespace Starter.Store.WebApi.Utilities;

public partial class ToKebabParameterTransformer : IOutboundParameterTransformer
{
    /// <summary>
    /// Convert any case to kebab case
    /// </summary>
    public string TransformOutbound(object? value)
    {
        return MatchLowercaseThenUppercase()
            .Replace(value?.ToString() ?? "", "$1-$2")
            .ToLower();
    }

    [GeneratedRegex("([a-z])([A-Z])")]
    private static partial Regex MatchLowercaseThenUppercase();
}
