namespace M4u.CAS.Parser;

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

    public TokenizerResult(IReadOnlyList<IToken> resultTokens, int? failIndex, string? failedToRecognizeToken)
    {
        this.ResultTokens = resultTokens;
        this.FailIndex = failIndex;
        this.FailedToRecognizeToken = failedToRecognizeToken;
    }
}
