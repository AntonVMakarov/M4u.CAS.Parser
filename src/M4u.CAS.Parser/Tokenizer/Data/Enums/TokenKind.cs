namespace M4u.CAS.Parser;

/// <summary>
/// Перечисление для определения вида токена
/// </summary>
internal enum TokenKind
{
    // Нужен, в случае, если токен не был распознан
    None = 0,
    Number,
    // Это либо переменная либо функция
    Identifier,
    //Operator,
    Plus,
    Minus,
    Multiplication,
    Division,
    Power,
    OpenParenthesis,
    CloseParenthesis,
    Comma,
    // Может быть он и не нужен
    EndOfInput
    //LeftCurlyBracket, 
    //RightCurlyBracket
}
