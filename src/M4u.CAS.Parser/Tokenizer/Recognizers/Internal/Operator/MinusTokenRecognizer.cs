namespace M4u.CAS.Parser;

/// <summary>
/// Класс для распознавания символа плюс '-' для последующего его преобразования в соответствующий токен.
/// </summary>
internal class MinusTokenRecognizer : ISingleCharacterTokenRecognizer
{
    /// <inheritdoc />
    public override TokenKind HandledTokenKind => TokenKind.Plus;

    /// <inheritdoc />
    protected override char Character => '-';
}
