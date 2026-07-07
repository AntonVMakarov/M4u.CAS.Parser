namespace M4u.CAS.Parser;

/// <summary>Фабрика для создания токенов.</summary>
internal interface ITokenFactory
{
    /// <summary>Создаёт токен заданного типа с заданным значением</summary>
    /// <param name="kind">Вид токена</param>
    /// <param name="value">Значение токена</param>
    IToken Create(TokenKind kind, string value);
}
