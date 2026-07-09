namespace M4u.CAS.Parser;

/// <summary>
/// Маппер для преобразования входной строки в список токенов.
/// </summary>
internal interface ITokenizer
{
    /// <summary>Парсит входную строку (содержащую математическое выражение) в список токенов</summary>
    /// <param name="request">Запрос на токенизацию. Содержит строку, содержащую математическое выражение,
    /// которую нужно токенизировать.</param>
    /// <returns>Возвращает результат токенизации</returns>
    TokenizerResult Tokenize(TokenizerRequest request);
}
