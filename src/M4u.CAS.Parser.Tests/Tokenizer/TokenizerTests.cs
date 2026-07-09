namespace M4u.CAS.Parser.Tests;

[TestClass]
public sealed class TokenizerTests
{
    private readonly ITokenizer _tokenizer;

    public TokenizerTests()
    {
        List<ITokenRecognizer> parsers = new List<ITokenRecognizer>()
        {
            new IdentifierTokenRecognizer(),
            new NumberTokenRecognizer(),
            new OpenParenthesisTokenRecognizer(),
            new CloseParenthesisTokenRecognizer(),
            new PlusTokenRecognizer(),
            new MinusTokenRecognizer(),
        };

        _tokenizer = new Tokenizer(parsers);
    }


    [TestMethod]
    public void TokenizerIdentifier_Test()
    {
        string expr = "x123";
        TokenizerRequest request = new TokenizerRequest(expr);
        TokenizerResult result = _tokenizer.Tokenize(request);

        Assert.IsNotNull(result);
        Assert.IsTrue(result.IsSuccess);
        Assert.HasCount(1, result.Tokens);
        Assert.AreEqual(TokenKind.Identifier, result.Tokens[0].Kind);
        Assert.AreEqual(expr, result.Tokens[0].Value);
    }

    [TestMethod]
    public void TokenizerNumber_Test()
    {
        string expr = "1.23";
        TokenizerRequest request = new TokenizerRequest(expr);
        TokenizerResult result = _tokenizer.Tokenize(request);

        Assert.IsNotNull(result);
        Assert.IsTrue(result.IsSuccess);
        Assert.HasCount(1, result.Tokens);
        Assert.AreEqual(TokenKind.Number, result.Tokens[0].Kind);
        Assert.AreEqual(expr, result.Tokens[0].Value);
    }


    [TestMethod]
    public void TokenizerExpression1_Test()
    {
        string expr = "1.23+x-(15-y757)";
        TokenizerRequest request = new TokenizerRequest(expr);
        TokenizerResult result = _tokenizer.Tokenize(request);

        //Assert.IsNotNull(result);
        //Assert.IsTrue(result.IsSuccess);
        //Assert.HasCount(1, result.Tokens);
        //Assert.AreEqual(TokenKind.Number, result.Tokens[0].Kind);
        //Assert.AreEqual(expr, result.Tokens[0].Value);
    }
}
