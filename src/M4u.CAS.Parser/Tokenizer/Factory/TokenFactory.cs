namespace M4u.CAS.Parser;

/// <inheritdoc />
internal class TokenFactory : ITokenFactory
{
    /// <summary>Реестр соответствия типа токена методу (делагату) его создания.</summary>
    private readonly Dictionary<TokenKind, Func<string, IToken>> _registry =
        new Dictionary<TokenKind, Func<string, IToken>>()
        {
            { TokenKind.Identifier, val => new Token() }
            //{ TokenType.BINARY_OPERATOR => new  }
        };


    /// <inheritdoc />
    /// <exception cref="NotSupportedException">Исключение происходит, если запрошено создание токена тип которого не поддерживается</exception>
    public IToken Create(TokenKind type, string value)
    {
        if(_registry.TryGetValue(type, out Func<string, IToken>? factoryMethod))
        {
            return factoryMethod(value);
        }
        throw new NotSupportedException($"Тип токена {type} не поддерживается фабрикой.");
    }
}
