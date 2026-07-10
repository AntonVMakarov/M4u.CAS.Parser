namespace M4u.CAS.Parser;

/// <summary>
/// Интерфейс распознавания последовательности символов как токена.
/// </summary>
internal interface ITokenRecognizer
{
    /// <summary>Проверяет входную строку с заданного индекса на совпадение заданному токену</summary>
    /// <param name="expr">Входная строка</param>
    /// <param name="i">Индекс во входной строке с которого необходимо начать проверку</param>
    /// <param name="value">Результирующее значение (значение распарсенного токена)</param>
    TokenRecognizerResult Match(TokenRecognizerRequest request);
}
