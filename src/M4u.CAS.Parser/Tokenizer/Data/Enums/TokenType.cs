namespace M4u.CAS.Parser;

/// <summary>Перечисление для определения типа токена</summary>
public enum TokenType
{
    CONSTANT, 
    Variable, 
    BINARY_OPERATOR, PRE_UNARY_OPERATOR, POST_UNARY_OPERATOR, 
    FUNCTION, LEFT_BRACKET, RIGHT_BRACKET, COMMA, LEFT_CURLY_BRACKET, RIGHT_CURLY_BRACKET
}
