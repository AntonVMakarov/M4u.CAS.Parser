namespace M4u.CAS.Parser;

/// <summary>Фабрика для создания токенов.</summary>
internal interface ITokenFactory
{
    /// <summary>Создаёт токен заданного типа с заданным значением</summary>
    /// <param name="type">Тип токена</param>
    /// <param name="value">Значение токена</param>
    IToken Create(TokenType type, string value);
}
