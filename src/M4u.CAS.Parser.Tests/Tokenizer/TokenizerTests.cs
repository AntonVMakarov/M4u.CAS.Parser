namespace M4u.CAS.Parser.Tests;

[TestClass]
public sealed class TokenizerTests
{
    private readonly ITokenizer _tokenizer;

    public TokenizerTests()
    {
        ITokenizerFactory factory = new TokenizerFactory();
        _tokenizer = factory.Create();
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

        Assert.IsNotNull(result);
        Assert.IsTrue(result.IsSuccess);
        Assert.HasCount(9, result.Tokens);

        Assert.AreEqual(TokenKind.Number, result.Tokens[0].Kind);
        Assert.AreEqual("1.23", result.Tokens[0].Value);
        Assert.AreEqual(new TextSpan(0, 4), result.Tokens[0].Span);

        Assert.AreEqual(TokenKind.Plus, result.Tokens[1].Kind);
        Assert.AreEqual("+", result.Tokens[1].Value);
        Assert.AreEqual(new TextSpan(4, 1), result.Tokens[1].Span);

        Assert.AreEqual(TokenKind.Identifier, result.Tokens[2].Kind);
        Assert.AreEqual("x", result.Tokens[2].Value);
        Assert.AreEqual(new TextSpan(5, 1), result.Tokens[2].Span);

        Assert.AreEqual(TokenKind.Minus, result.Tokens[3].Kind);
        Assert.AreEqual("-", result.Tokens[3].Value);
        Assert.AreEqual(new TextSpan(6, 1), result.Tokens[3].Span);

        Assert.AreEqual(TokenKind.OpenParenthesis, result.Tokens[4].Kind);
        Assert.AreEqual("(", result.Tokens[4].Value);
        Assert.AreEqual(new TextSpan(7, 1), result.Tokens[4].Span);

        Assert.AreEqual(TokenKind.Number, result.Tokens[5].Kind);
        Assert.AreEqual("15", result.Tokens[5].Value);
        Assert.AreEqual(new TextSpan(8, 2), result.Tokens[5].Span);

        Assert.AreEqual(TokenKind.Minus, result.Tokens[6].Kind);
        Assert.AreEqual("-", result.Tokens[6].Value);
        Assert.AreEqual(new TextSpan(10, 1), result.Tokens[6].Span);

        Assert.AreEqual(TokenKind.Identifier, result.Tokens[7].Kind);
        Assert.AreEqual("y757", result.Tokens[7].Value);
        Assert.AreEqual(new TextSpan(11, 4), result.Tokens[7].Span);

        Assert.AreEqual(TokenKind.CloseParenthesis, result.Tokens[8].Kind);
        Assert.AreEqual(")", result.Tokens[8].Value);
        Assert.AreEqual(new TextSpan(15, 1), result.Tokens[8].Span);
    }
}
