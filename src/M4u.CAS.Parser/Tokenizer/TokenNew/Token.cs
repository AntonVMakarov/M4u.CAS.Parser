namespace M4u.CAS.Parser;

public sealed record Token
{
    public string Value { get; }
    public TokenKind Kind { get; }

    public Token(string value, TokenKind kind)
    {
        Value = value;
        Kind = kind;
    }
}
