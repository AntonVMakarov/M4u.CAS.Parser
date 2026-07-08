namespace M4u.CAS.Parser;

/// <summary>
/// Интерфейс для парсинга токена.
/// </summary>
internal interface ITokenParser
{
    /// <summary>Вид токена, который поддерживает данный парсер.</summary>
    TokenKind HandledTokenKind { get; }

    /// <summary>Проверяет входную строку с заданного индекса на совпадение заданному токену</summary>
    /// <param name="expr">Входная строка</param>
    /// <param name="i">Индекс во входной строке с которого необходимо начать проверку</param>
    /// <param name="value">Результирующее значение (значение распарсенного токена)</param>
    TokenParserResult Match(TokenParserRequest request);
}
