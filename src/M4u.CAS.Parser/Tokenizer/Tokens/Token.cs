namespace M4u.CAS.Parser;

internal sealed record Token
{
    public string Value { get; }
    public TokenKind Kind { get; }

    public Token(string value, TokenKind kind)
    {
        Value = value;
        Kind = kind;
    }
}
