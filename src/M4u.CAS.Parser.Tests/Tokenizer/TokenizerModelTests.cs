namespace M4u.CAS.Parser.Tests;

[TestClass]
public sealed class TokenizerModelTests
{
    [TestMethod]
    public void TokenRecognizerRequest_NullExpression_ThrowsArgumentNullException()
    {
        Assert.ThrowsExactly<ArgumentNullException>(() => new TokenRecognizerRequest(null!, 0, null));
    }

    [TestMethod]
    public void TokenRecognizerRequest_NegativeIndex_ThrowsArgumentOutOfRangeException()
    {
        Assert.ThrowsExactly<ArgumentOutOfRangeException>(() => new TokenRecognizerRequest("x", -1, null));
    }

    [TestMethod]
    public void TokenRecognizerRequest_IndexEqualToExpressionLength_ThrowsArgumentOutOfRangeException()
    {
        Assert.ThrowsExactly<ArgumentOutOfRangeException>(() => new TokenRecognizerRequest("x", 1, null));
    }

    [TestMethod]
    public void TokenRecognizerResult_NoMatch_HasZeroLengthAndIsNotMatch()
    {
        TokenRecognizerResult result = TokenRecognizerResult.NoMatch;

        Assert.IsFalse(result.IsMatch);
        Assert.AreEqual(0, result.Length);
    }

    [TestMethod]
    [DataRow(0)]
    [DataRow(-1)]
    public void TokenRecognizerResult_Success_WhenLengthIsNotPositive_ThrowsArgumentOutOfRangeException(int length)
    {
        Assert.ThrowsExactly<ArgumentOutOfRangeException>(() => TokenRecognizerResult.Success(length));
    }

    [TestMethod]
    public void TextSpan_WhenLengthIsZero_CreatesZeroLengthSpan()
    {
        TextSpan span = new TextSpan(3, 0);

        Assert.AreEqual(3, span.Start);
        Assert.AreEqual(0, span.Length);
    }

    [TestMethod]
    public void TextSpan_WhenStartIsNegative_ThrowsArgumentOutOfRangeException()
    {
        Assert.ThrowsExactly<ArgumentOutOfRangeException>(() => new TextSpan(-1, 1));
    }

    [TestMethod]
    public void TextSpan_WhenLengthIsNegative_ThrowsArgumentOutOfRangeException()
    {
        Assert.ThrowsExactly<ArgumentOutOfRangeException>(() => new TextSpan(0, -1));
    }

    [TestMethod]
    public void TokenizerResult_Success_CopiesTokensAndHasNoDiagnostics()
    {
        List<Token> tokens =
        [
            new Token(TokenKind.Identifier, "x", new TextSpan(0, 1)),
        ];

        TokenizerResult result = TokenizerResult.Success(tokens);
        tokens.Clear();

        Assert.IsTrue(result.IsSuccess);
        Assert.HasCount(1, result.Tokens);
        Assert.AreEqual(TokenKind.Identifier, result.Tokens[0].Kind);
        Assert.AreEqual("x", result.Tokens[0].Value);
        Assert.HasCount(0, result.Diagnostics);
    }

    [TestMethod]
    public void TokenizerResult_Failure_CopiesTokensAndDiagnostics()
    {
        List<Token> tokens =
        [
            new Token(TokenKind.Identifier, "x", new TextSpan(0, 1)),
        ];
        List<Diagnostic> diagnostics =
        [
            new Diagnostic(DiagnosticCode.UnknownCharacter, new TextSpan(1, 1), "message"),
        ];

        TokenizerResult result = TokenizerResult.Failure(tokens, diagnostics);
        tokens.Clear();
        diagnostics.Clear();

        Assert.IsFalse(result.IsSuccess);
        Assert.HasCount(1, result.Tokens);
        Assert.HasCount(1, result.Diagnostics);
        Assert.AreEqual(DiagnosticCode.UnknownCharacter, result.Diagnostics[0].Code);
        Assert.AreEqual(new TextSpan(1, 1), result.Diagnostics[0].Span);
        Assert.AreEqual("message", result.Diagnostics[0].Message);
    }
}
