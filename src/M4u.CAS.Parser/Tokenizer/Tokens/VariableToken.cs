namespace M4u.CAS.Parser;

/// <summary>конкретная реализация абстрактного класса для описания токена - переменная</summary>
internal class VariableToken : IToken
{
    /// <summary>тип токена - переменная</summary>
    public override TokenKind Type => TokenKind.Variable;

    public VariableToken(string value) : base(value) { }
}
