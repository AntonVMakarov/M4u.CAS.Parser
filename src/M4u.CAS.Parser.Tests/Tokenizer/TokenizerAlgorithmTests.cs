namespace M4u.CAS.Parser.Tests;

[TestClass]
public sealed class TokenizerAlgorithmTests
{
    [TestMethod]
    public void TokenizerConstructor_NullRecognizers_ThrowsArgumentNullException()
    {
        Assert.ThrowsExactly<ArgumentNullException>(() => new Tokenizer(null!));
    }

    [TestMethod]
    public void Tokenize_WhenSeveralRecognizersMatch_ChoosesLongestMatch()
    {
        Tokenizer tokenizer = new Tokenizer(
            [
                new FixedTextTokenRecognizer(TokenKind.Identifier, "a"),
                new FixedTextTokenRecognizer(TokenKind.Number, "ab"),
            ]);

        TokenizerResult result = tokenizer.Tokenize(new TokenizerRequest("ab"));

        Assert.IsTrue(result.IsSuccess);
        Assert.HasCount(1, result.Tokens);
        Assert.AreEqual(TokenKind.Number, result.Tokens[0].Kind);
        Assert.AreEqual("ab", result.Tokens[0].Value);
        Assert.AreEqual(new TextSpan(0, 2), result.Tokens[0].Span);
    }

    [TestMethod]
    public void Tokenize_WhenSeveralRecognizersMatchSameLongestLength_ThrowsInvalidOperationException()
    {
        Tokenizer tokenizer = new Tokenizer(
            [
                new FixedTextTokenRecognizer(TokenKind.Identifier, "a"),
                new FixedTextTokenRecognizer(TokenKind.Number, "a"),
            ]);

        Assert.ThrowsExactly<InvalidOperationException>(() => tokenizer.Tokenize(new TokenizerRequest("a")));
    }

    private sealed class FixedTextTokenRecognizer : ITokenRecognizer
    {
        private readonly TokenKind _handledTokenKind;
        private readonly string _text;

        public FixedTextTokenRecognizer(TokenKind handledTokenKind, string text)
        {
            _handledTokenKind = handledTokenKind;
            _text = text;
        }

        public TokenRecognizerResult Match(TokenRecognizerRequest request)
        {
            if (request.Expression[request.Index..].StartsWith(_text, StringComparison.Ordinal))
            {
                return TokenRecognizerResult.Success(_handledTokenKind, _text.Length);
            }

            return TokenRecognizerResult.NoMatch;
        }
    }
}
