namespace M4u.CAS.Parser;

/// <summary>
/// Перечисление для определения вида токена
/// </summary>
internal enum TokenKind
{
    Number,
    // Это либо переменная либо функция
    Identifier,
    Operator,
    LeftParenthesis,
    RightParenthesis,
    Comma,
    // Может быть он и не нужен
    EndOfInput
    //LeftCurlyBracket, 
    //RightCurlyBracket
}
