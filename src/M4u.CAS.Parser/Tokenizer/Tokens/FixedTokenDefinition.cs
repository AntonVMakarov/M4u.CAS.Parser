namespace M4u.CAS.Parser;

/// <summary>
/// Определение токена с фиксированным текстом.
/// </summary>
internal sealed record FixedTokenDefinition
{
    public string Text { get; }

    public TokenKind Kind { get; }

    public FixedTokenDefinition(string text, TokenKind kind)
    {
        Text = text;
        Kind = kind;
    }
}
