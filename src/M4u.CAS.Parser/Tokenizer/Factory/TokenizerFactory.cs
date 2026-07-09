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
                new OpenParenthesisTokenRecognizer(),
                new CloseParenthesisTokenRecognizer(),
                new PlusTokenRecognizer(),
                new MinusTokenRecognizer(),
            ]);
    }
}
