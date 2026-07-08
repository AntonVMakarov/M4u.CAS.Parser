namespace M4u.CAS.Parser.Tests;

[TestClass]
public sealed class TokenizerTests
{
    [TestMethod]
    public void TryParse_Test()
    {
        List<ITokenRecognizer> parsers = new List<ITokenRecognizer>()
        {
            new IdentifierTokenRecognizer()
        };

        ITokenizer tokenizer = new Tokenizer(parsers);

        string expr = "x123";

        TokenizerRequest request = new TokenizerRequest(expr);

        TokenizerResult result = tokenizer.TryParse(request);

        Assert.IsNotNull(result);
        Assert.HasCount(1, result.ResultTokens);
        Assert.AreEqual(TokenKind.Identifier, result.ResultTokens[0].Kind);
        Assert.AreEqual(expr, result.ResultTokens[0].Value);
    }
}
