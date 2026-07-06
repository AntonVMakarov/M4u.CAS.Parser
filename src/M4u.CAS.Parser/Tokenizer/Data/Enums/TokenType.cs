namespace M4u.CAS.Parser;

/// <summary>Перечисление для определения типа токена</summary>
public enum TokenType
{
    Number, 
    Variable, 
    BinaryOperator, PreUnaryOperator, PostUnaryOperator, 
    Function, LeftBracket, RightBracket, Comma, LeftCurlyBracket, RightCurlyBracket
}
