namespace M4u.CAS.Parser;

internal sealed record Token
{
    /// <summary>
    /// Вид токена.
    /// </summary>
    public TokenKind Kind { get; }
    

    /// <summary>
    /// Значение токена.
    /// </summary>
    public string Value { get; }


    /// <summary>
    /// Структура, содержащая начальный индекс и длину токена в составе строки.
    /// (Нужна, чтобы если понадобиться можно было получить точное местоположение токена в составе исходной строки).
    /// </summary>
    public TextSpan Span { get; }

    /// <summary>
    /// Открытый конструктор с параметрами.
    /// </summary>
    /// <param name="kind">Вид токена.</param>
    /// <param name="value">Значение токена.</param>
    /// <param name="span">Структура для определения местоположения токена в исходной строке.</param>
    public Token(TokenKind kind, string value, TextSpan span)
    {
        Kind = kind;
        Value = value;
        Span = span;
    }
}
