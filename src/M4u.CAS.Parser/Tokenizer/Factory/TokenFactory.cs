namespace M4u.CAS.Parser;

/// <inheritdoc />
internal class TokenFactory : ITokenFactory
{
    /// <summary>Реестр соответствия типа токена методу (делагату) его создания.</summary>
    private readonly Dictionary<TokenType, Func<string, IToken>> _registry =
        new Dictionary<TokenType, Func<string, IToken>>()
        {
            { TokenType.Variable, val => new VariableToken(val) }
            //{ TokenType.BINARY_OPERATOR => new  }
        };


    /// <inheritdoc />
    /// <exception cref="NotSupportedException">Исключение происходит, если запрошено создание токена тип которого не поддерживается</exception>
    public IToken Create(TokenType type, string value)
    {
        if(_registry.TryGetValue(type, out Func<string, IToken>? factoryMethod))
        {
            return factoryMethod(value);
        }
        throw new NotSupportedException($"Тип токена {type} не поддерживается фабрикой.");
    }
}
