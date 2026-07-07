namespace M4u.CAS.Parser;

public sealed record TokenParserResult
{
    public bool IsParsed { get; }
    public string? ParsedValue { get; }

    private TokenParserResult(bool isParsed, string? parsedValue)
    {
        IsParsed = isParsed;
        ParsedValue = parsedValue;
    }

    public static TokenParserResult Success(string? parsedValue)
    {
        ArgumentException.ThrowIfNullOrEmpty(parsedValue);
        ArgumentException.ThrowIfNullOrWhiteSpace(parsedValue);

        return new TokenParserResult(true, parsedValue);
    }

    public static TokenParserResult Failure()
    {
        return new TokenParserResult(false, null);
    }
}
