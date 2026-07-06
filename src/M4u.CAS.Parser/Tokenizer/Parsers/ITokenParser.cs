namespace M4u.CAS.Parser;

/// <summary>
/// Интерфейс для парсинга токена.
/// </summary>
internal interface ITokenParser
{
    /// <summary>Тип токена, который поддерживает данный парсер.</summary>
    TokenType HandledType { get; }

    /// <summary>Проверяет входную строку с заданного индекса на соответствие соответствубщему токену</summary>
    /// <param name="expr">Входная строка</param>
    /// <param name="i">Индекс во входной строке с которого необходимо начать проверку</param>
    /// <param name="value">Результирующее значение (значение распарсенного токена)</param>
    bool Match(string expr, int i, out string value);
}
