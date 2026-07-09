namespace M4u.CAS.Parser;

/// <summary>
/// Класс для распознавания символа открывающейся скобки '(' и преобразования его в соответствующий токен.
/// </summary>
internal class CloseParenthesisTokenRecognizer : ISingleCharacterTokenRecognizer
{    
    /// <inheritdoc />
    public override TokenKind HandledTokenKind => TokenKind.CloseParenthesis;

    /// <inheritdoc />
    protected override char Character => ')';
}
