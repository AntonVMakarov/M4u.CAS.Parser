namespace M4u.CAS.Parser;

/// <summary>
/// Класс для распознавания символа открывающейся скобки '(' и преобразования его в соответствующий токен.
/// </summary>
internal class OpenParenthesisTokenRecognizer : ISingleCharacterTokenRecognizer
{
    /// <inheritdoc />
    public override TokenKind HandledTokenKind => TokenKind.OpenParenthesis;

    /// <inheritdoc />
    protected override char Character => '(';
}
