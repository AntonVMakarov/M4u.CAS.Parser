using System.Collections.Immutable;

namespace M4u.CAS.Parser;

/// <summary>
/// Результат токенизации входной строки.
/// </summary>
internal sealed record TokenizerResult
{
    /// <summary>
    /// Результирующий список токенов.
    /// </summary>
    public IReadOnlyList<Token> Tokens { get; }


    /// <summary>
    /// Список диагностической информации в случае ошибки.
    /// </summary>
    public IReadOnlyList<Diagnostic> Diagnostics { get; }


    /// <summary>
    /// Открытый конструктор для создания экземпляра TokenizerResult.
    /// </summary>
    /// <param name="tokens">Результирующий список токенов.</param>
    /// <param name="diagnostics">Список диагностических сообщений в случае ошибки.</param>
    private TokenizerResult(IReadOnlyList<Token> tokens, IReadOnlyList<Diagnostic> diagnostics)
    {
        this.Tokens = tokens;
        this.Diagnostics = diagnostics;
    }


    /// <summary>
    /// Создает успешный результат токенизации с предоставленным списком токенов.
    /// </summary>
    /// <param name="tokens">Результирующий список токенов.</param>
    /// <returns>Экземпляр TokenizerResult, представляющий успешный результат токенизации.</returns>
    public static TokenizerResult Success(IReadOnlyList<Token> tokens)
    {
        return new TokenizerResult(tokens, Array.Empty<Diagnostic>());
    }


    /// <summary>
    /// Создает неуспешный результат токенизации с предоставленным индексом ошибки и строкой, которая не была распознана.
    /// </summary>
    /// <param name="failIndex">Индекс в строке на котором "сломалось" распознование токенов.</param>
    /// <param name="failedToRecognizeToken">"Кусок" строки о который "сломалось" распознавание.</param>
    /// <returns>Экземпляр TokenizerResult, представляющий неуспешный результат токенизации.</returns>
    public static TokenizerResult Failure(IReadOnlyList<Token> tokens, IReadOnlyList<Diagnostic> diagnostics)
    {
        return new TokenizerResult(tokens, diagnostics);
    }
}
