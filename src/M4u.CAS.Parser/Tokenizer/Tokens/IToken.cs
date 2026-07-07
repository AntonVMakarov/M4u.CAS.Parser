namespace M4u.CAS.Parser;

/// <summary>
/// Абстрактный класс, описывающий некоторый токен (символ).
/// Мы будем поддерживать следующие типы токенов:
/// '+'
/// 
/// </summary>
public abstract class IToken
{
    /// <summary>Тип токена</summary>
    public abstract TokenKind Type { get; }

    /// <summary>Значение токена</summary>
    private readonly string Value;

    /// <summary>длина токена</summary>
    public int Length => Value.Length;

    public IToken(string value) => Value = value;
}
