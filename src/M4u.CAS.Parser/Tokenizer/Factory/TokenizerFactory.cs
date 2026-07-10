namespace M4u.CAS.Parser;

/// <inheritdoc />
internal class TokenizerFactory : ITokenizerFactory
{
    public ITokenizer Create()
    {
        return new Tokenizer(
            [
                new IdentifierTokenRecognizer(),
                new NumberTokenRecognizer(),
                new FixedTokenRecognizer(
                    [
                        new FixedTokenDefinition("+", TokenKind.Plus),
                        new FixedTokenDefinition("-", TokenKind.Minus),
                        new FixedTokenDefinition("*", TokenKind.Multiplication),
                        new FixedTokenDefinition(":", TokenKind.Division),
                        new FixedTokenDefinition("/", TokenKind.Division),
                        new FixedTokenDefinition("^", TokenKind.Power),
                        new FixedTokenDefinition("(", TokenKind.OpenParenthesis),
                        new FixedTokenDefinition(")", TokenKind.CloseParenthesis),
                    ])
            ]);
    }
}
