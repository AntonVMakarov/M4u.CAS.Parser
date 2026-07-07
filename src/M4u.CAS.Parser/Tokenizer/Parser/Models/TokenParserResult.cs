namespace M4u.CAS.Parser;

public sealed record TokenParserResult
{
    public bool IsParsed { get; }
    public string? ParsedValue { get; }
    public TokenKind? TokenKind { get; }

    private TokenParserResult(bool isParsed, string? parsedValue, TokenKind? tokenKind)
    {
        IsParsed = isParsed;
        ParsedValue = parsedValue;
        TokenKind = tokenKind;
    }

    public static TokenParserResult Success(string? parsedValue, TokenKind? tokenKind)
    {
        ArgumentException.ThrowIfNullOrEmpty(parsedValue);
        ArgumentException.ThrowIfNullOrWhiteSpace(parsedValue);
        ArgumentNullException.ThrowIfNull(tokenKind);

        return new TokenParserResult(true, parsedValue, tokenKind);
    }

    public static TokenParserResult Failure()
    {
        return new TokenParserResult(false, null, null);
    }
}
