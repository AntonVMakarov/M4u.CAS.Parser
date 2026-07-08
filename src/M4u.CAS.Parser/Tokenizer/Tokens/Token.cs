namespace M4u.CAS.Parser;

internal sealed record Token
{
    public TokenKind Kind { get; }
    
    public string Value { get; }

    /// <summary>
    /// Структура, содержащая начальный индекс и длину токена в составе строки.
    /// (Нужна, чтобы если понадобиться можно было получить точное местоположение токена в составе исходной строки).
    /// </summary>
    public TextSpan Span { get; }

    public Token(TokenKind kind, string value, TextSpan span)
    {
        Kind = kind;
        Value = value;
        Span = span;
    }
}
