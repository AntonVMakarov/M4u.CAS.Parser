namespace M4u.CAS.Parser;

/// <summary>
/// Абстрактный класс для распознавания некоторого символа для последующего его преобразования в соответствующий токен.
/// </summary>
internal abstract class ISingleCharacterTokenRecognizer : ITokenRecognizer
{
    /// <summary>
    /// Символ для распознавания.
    /// </summary>
    protected abstract char Character { get; }


    /// <inheritdoc />
    public abstract TokenKind HandledTokenKind { get; }


    /// <inheritdoc />
    public TokenRecognizerResult Match(TokenRecognizerRequest request)
        =>
            request.Expression[request.Index] == Character ?
            TokenRecognizerResult.Success(1) :
            TokenRecognizerResult.NoMatch;
}
