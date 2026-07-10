using M4u.CAS.Common;

namespace M4u.CAS.Parser.Tests;

[TestClass]
public sealed class TokenizerBehaviorTests
{
    private readonly ITokenizer _tokenizer;

    public TokenizerBehaviorTests()
    {
        ITokenizerFactory factory = new TokenizerFactory();
        _tokenizer = factory.Create();
    }

    [TestMethod]
    [DataRow("")]
    [DataRow("   \t\r\n")]
    public void Tokenize_EmptyOrWhitespaceOnlyExpression_ReturnsSuccessWithoutTokens(string expression)
    {
        TokenizerResult result = _tokenizer.Tokenize(new TokenizerRequest(expression));

        Assert.IsTrue(result.IsSuccess);
        Assert.HasCount(0, result.Tokens);
        Assert.HasCount(0, result.Diagnostics);
    }

    [TestMethod]
    public void Tokenize_SkipsWhitespaceAndKeepsOriginalSpans()
    {
        string expression = "  x \t+\r\n 12 ";

        TokenizerResult result = _tokenizer.Tokenize(new TokenizerRequest(expression));

        Assert.IsTrue(result.IsSuccess);
        Assert.HasCount(3, result.Tokens);
        AssertToken(result.Tokens[0], TokenKind.Identifier, "x", 2, 1);
        AssertToken(result.Tokens[1], TokenKind.Plus, "+", 5, 1);
        AssertToken(result.Tokens[2], TokenKind.Number, "12", 9, 2);
    }

    [TestMethod]
    public void Tokenize_PlusAndMinusAreNotPartOfNumberToken()
    {
        string expression = "-1+ +2";

        TokenizerResult result = _tokenizer.Tokenize(new TokenizerRequest(expression));

        Assert.IsTrue(result.IsSuccess);
        Assert.HasCount(5, result.Tokens);
        AssertToken(result.Tokens[0], TokenKind.Minus, "-", 0, 1);
        AssertToken(result.Tokens[1], TokenKind.Number, "1", 1, 1);
        AssertToken(result.Tokens[2], TokenKind.Plus, "+", 2, 1);
        AssertToken(result.Tokens[3], TokenKind.Plus, "+", 4, 1);
        AssertToken(result.Tokens[4], TokenKind.Number, "2", 5, 1);
    }

    [TestMethod]
    public void Tokenize_NumberImmediatelyFollowedByIdentifier_ReturnsNumberThenIdentifier()
    {
        string expression = "123abc";

        TokenizerResult result = _tokenizer.Tokenize(new TokenizerRequest(expression));

        Assert.IsTrue(result.IsSuccess);
        Assert.HasCount(2, result.Tokens);
        AssertToken(result.Tokens[0], TokenKind.Number, "123", 0, 3);
        AssertToken(result.Tokens[1], TokenKind.Identifier, "abc", 3, 3);
    }

    [TestMethod]
    public void Tokenize_FixedOperatorAndParenthesisCharacters_ReturnsOneTokenPerCharacter()
    {
        string expression = "+-*:/^()";

        TokenizerResult result = _tokenizer.Tokenize(new TokenizerRequest(expression));

        Assert.IsTrue(result.IsSuccess);
        Assert.HasCount(8, result.Tokens);
        AssertToken(result.Tokens[0], TokenKind.Plus, "+", 0, 1);
        AssertToken(result.Tokens[1], TokenKind.Minus, "-", 1, 1);
        AssertToken(result.Tokens[2], TokenKind.Multiplication, "*", 2, 1);
        AssertToken(result.Tokens[3], TokenKind.Division, ":", 3, 1);
        AssertToken(result.Tokens[4], TokenKind.Division, "/", 4, 1);
        AssertToken(result.Tokens[5], TokenKind.Power, "^", 5, 1);
        AssertToken(result.Tokens[6], TokenKind.OpenParenthesis, "(", 6, 1);
        AssertToken(result.Tokens[7], TokenKind.CloseParenthesis, ")", 7, 1);
    }

    [TestMethod]
    public void Tokenize_ExpressionWithNewOperators_ReturnsExpectedTokenKindsAndSpans()
    {
        string expression = "2*x^3/4:5";

        TokenizerResult result = _tokenizer.Tokenize(new TokenizerRequest(expression));

        Assert.IsTrue(result.IsSuccess);
        Assert.HasCount(9, result.Tokens);
        AssertToken(result.Tokens[0], TokenKind.Number, "2", 0, 1);
        AssertToken(result.Tokens[1], TokenKind.Multiplication, "*", 1, 1);
        AssertToken(result.Tokens[2], TokenKind.Identifier, "x", 2, 1);
        AssertToken(result.Tokens[3], TokenKind.Power, "^", 3, 1);
        AssertToken(result.Tokens[4], TokenKind.Number, "3", 4, 1);
        AssertToken(result.Tokens[5], TokenKind.Division, "/", 5, 1);
        AssertToken(result.Tokens[6], TokenKind.Number, "4", 6, 1);
        AssertToken(result.Tokens[7], TokenKind.Division, ":", 7, 1);
        AssertToken(result.Tokens[8], TokenKind.Number, "5", 8, 1);
    }

    [TestMethod]
    public void Tokenize_UnknownCharacter_ReturnsFailureWithTokensBeforeErrorAndDiagnostic()
    {
        string expression = "x+@";

        TokenizerResult result = _tokenizer.Tokenize(new TokenizerRequest(expression));

        Assert.IsFalse(result.IsSuccess);
        Assert.HasCount(2, result.Tokens);
        AssertToken(result.Tokens[0], TokenKind.Identifier, "x", 0, 1);
        AssertToken(result.Tokens[1], TokenKind.Plus, "+", 1, 1);

        Assert.HasCount(1, result.Diagnostics);
        Assert.AreEqual(DiagnosticCode.UnknownCharacter, result.Diagnostics[0].Code);
        Assert.AreEqual(new TextSpan(2, 1), result.Diagnostics[0].Span);
        Assert.AreEqual("Неизвестный символ '@'.", result.Diagnostics[0].Message);
    }

    [TestMethod]
    public void Tokenize_WhenCancellationTokenThrows_PropagatesCancellation()
    {
        ThrowingCancellationToken cancellationToken = new ThrowingCancellationToken();
        TokenizerRequest request = new TokenizerRequest("x", cancellationToken);

        Assert.ThrowsExactly<M4uOperationCanceledException>(() => _tokenizer.Tokenize(request));
    }

    [TestMethod]
    public void Tokenize_NullRequest_ThrowsArgumentNullException()
    {
        Assert.ThrowsExactly<ArgumentNullException>(() => _tokenizer.Tokenize(null!));
    }

    [TestMethod]
    public void Tokenize_NullExpression_ThrowsArgumentNullException()
    {
        TokenizerRequest request = new TokenizerRequest(null!);

        Assert.ThrowsExactly<ArgumentNullException>(() => _tokenizer.Tokenize(request));
    }

    private static void AssertToken(Token token, TokenKind kind, string value, int start, int length)
    {
        Assert.AreEqual(kind, token.Kind);
        Assert.AreEqual(value, token.Value);
        Assert.AreEqual(new TextSpan(start, length), token.Span);
    }

    private sealed class ThrowingCancellationToken : IM4uCancellationToken
    {
        public override bool IsCancellationRequested => true;
    }
}
