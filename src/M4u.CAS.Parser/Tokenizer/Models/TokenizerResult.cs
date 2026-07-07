namespace M4u.CAS.Parser;

/// <summary>
/// Результат токенизации входной строки.
/// </summary>
public sealed record TokenizerResult
{
    /// <summary>
    /// Результирующий список токенов.
    /// </summary>
    public IReadOnlyList<IToken> ResultTokens { get; }


    /// <summary>
    /// Индекс в строке на котором "сломалось" распознование токенов.
    /// </summary>
    public int? FailIndex { get; }


    /// <summary>
    /// "Кусок" строки о который "сломалось" распознавание.
    /// </summary>
    public string? FailedToRecognizeToken { get; }


    /// <summary>
    /// Открытый конструктор для создания экземпляра TokenizerResult.
    /// </summary>
    /// <param name="resultTokens">Результирующий список токенов.</param>
    /// <param name="failIndex">Индекс в строке на котором "сломалось" распознование токенов.</param>
    /// <param name="failedToRecognizeToken">"Кусок" строки о который "сломалось" распознавание.</param>
    private TokenizerResult(IReadOnlyList<IToken> resultTokens, int? failIndex, string? failedToRecognizeToken)
    {
        this.ResultTokens = resultTokens;
        this.FailIndex = failIndex;
        this.FailedToRecognizeToken = failedToRecognizeToken;
    }


    /// <summary>
    /// Создает успешный результат токенизации с предоставленным списком токенов.
    /// </summary>
    /// <param name="resultTokens">Результирующий список токенов.</param>
    /// <returns>Экземпляр TokenizerResult, представляющий успешный результат токенизации.</returns>
    public static TokenizerResult Success(IReadOnlyList<IToken> resultTokens)
    {
        return new TokenizerResult(resultTokens, null, null);
    }


    /// <summary>
    /// Создает неуспешный результат токенизации с предоставленным индексом ошибки и строкой, которая не была распознана.
    /// </summary>
    /// <param name="failIndex">Индекс в строке на котором "сломалось" распознование токенов.</param>
    /// <param name="failedToRecognizeToken">"Кусок" строки о который "сломалось" распознавание.</param>
    /// <returns>Экземпляр TokenizerResult, представляющий неуспешный результат токенизации.</returns>
    public static TokenizerResult Failure(int failIndex, string failedToRecognizeToken)
    {
        return new TokenizerResult(Array.Empty<IToken>(), failIndex, failedToRecognizeToken);
    }
}
