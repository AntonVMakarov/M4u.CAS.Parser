namespace M4u.CAS.Parser.Tests;

[TestClass]
public sealed class TokenRecognizerTests
{
    [TestMethod]
    [DataRow("x", 1)]
    [DataRow("xy", 2)]
    [DataRow("x235", 4)]
    [DataRow("x235y", 5)]
    [DataRow("x+1", 1)]
    public void IdentifierTokenRecognizer_Match_WhenIdentifierStartsAtIndex_ReturnsIdentifierLength(
        string expression,
        int expectedLength)
    {
        IdentifierTokenRecognizer recognizer = new IdentifierTokenRecognizer();

        TokenRecognizerResult result = recognizer.Match(new TokenRecognizerRequest(expression, 0, null));

        Assert.IsTrue(result.IsMatch);
        Assert.AreEqual(TokenKind.Identifier, result.Kind);
        Assert.AreEqual(expectedLength, result.Length);
    }

    [TestMethod]
    [DataRow("1x")]
    [DataRow("_x")]
    [DataRow(".x")]
    public void IdentifierTokenRecognizer_Match_WhenTokenDoesNotStartWithLetter_ReturnsNoMatch(string expression)
    {
        IdentifierTokenRecognizer recognizer = new IdentifierTokenRecognizer();

        TokenRecognizerResult result = recognizer.Match(new TokenRecognizerRequest(expression, 0, null));

        Assert.IsFalse(result.IsMatch);
        Assert.AreEqual(0, result.Length);
    }

    [TestMethod]
    [DataRow("1", 1)]
    [DataRow("1.23", 4)]
    [DataRow(".5", 2)]
    [DataRow("5.", 2)]
    [DataRow("1.23+x", 4)]
    public void NumberTokenRecognizer_Match_WhenNumberStartsAtIndex_ReturnsNumberLength(
        string expression,
        int expectedLength)
    {
        NumberTokenRecognizer recognizer = new NumberTokenRecognizer();

        TokenRecognizerResult result = recognizer.Match(new TokenRecognizerRequest(expression, 0, null));

        Assert.IsTrue(result.IsMatch);
        Assert.AreEqual(TokenKind.Number, result.Kind);
        Assert.AreEqual(expectedLength, result.Length);
    }

    [TestMethod]
    [DataRow("x1")]
    [DataRow("+1")]
    [DataRow("-1")]
    public void NumberTokenRecognizer_Match_WhenTokenDoesNotStartWithDigitOrDot_ReturnsNoMatch(string expression)
    {
        NumberTokenRecognizer recognizer = new NumberTokenRecognizer();

        TokenRecognizerResult result = recognizer.Match(new TokenRecognizerRequest(expression, 0, null));

        Assert.IsFalse(result.IsMatch);
        Assert.AreEqual(0, result.Length);
    }

    [TestMethod]
    [DataRow("+", nameof(TokenKind.Plus))]
    [DataRow("-", nameof(TokenKind.Minus))]
    [DataRow("*", nameof(TokenKind.Multiplication))]
    [DataRow(":", nameof(TokenKind.Division))]
    [DataRow("/", nameof(TokenKind.Division))]
    [DataRow("^", nameof(TokenKind.Power))]
    [DataRow("(", nameof(TokenKind.OpenParenthesis))]
    [DataRow(")", nameof(TokenKind.CloseParenthesis))]
    public void FixedTokenRecognizer_Match_WhenConfiguredTokenStartsAtIndex_ReturnsConfiguredToken(
        string expression,
        string expectedKindName)
    {
        TokenKind expectedKind = Enum.Parse<TokenKind>(expectedKindName);
        FixedTokenRecognizer recognizer = CreateDefaultFixedTokenRecognizer();

        TokenRecognizerResult result = recognizer.Match(new TokenRecognizerRequest(expression, 0, null));

        Assert.IsTrue(result.IsMatch);
        Assert.AreEqual(expectedKind, result.Kind);
        Assert.AreEqual(1, result.Length);
    }

    [TestMethod]
    public void FixedTokenRecognizer_Match_WhenNoConfiguredTokenStartsAtIndex_ReturnsNoMatch()
    {
        FixedTokenRecognizer recognizer = CreateDefaultFixedTokenRecognizer();

        TokenRecognizerResult result = recognizer.Match(new TokenRecognizerRequest("x", 0, null));

        Assert.IsFalse(result.IsMatch);
        Assert.AreEqual(0, result.Length);
    }

    [TestMethod]
    public void FixedTokenRecognizer_Match_WhenDefinitionsOverlap_ReturnsLongestToken()
    {
        FixedTokenRecognizer recognizer = new FixedTokenRecognizer(
            [
                new FixedTokenDefinition(":", TokenKind.Division),
                new FixedTokenDefinition(":=", TokenKind.Power),
            ]);

        TokenRecognizerResult result = recognizer.Match(new TokenRecognizerRequest(":=x", 0, null));

        Assert.IsTrue(result.IsMatch);
        Assert.AreEqual(TokenKind.Power, result.Kind);
        Assert.AreEqual(2, result.Length);
    }

    [TestMethod]
    public void FixedTokenRecognizer_NullDefinitions_ThrowsArgumentNullException()
    {
        Assert.ThrowsExactly<ArgumentNullException>(() => new FixedTokenRecognizer(null!));
    }

    private static FixedTokenRecognizer CreateDefaultFixedTokenRecognizer()
    {
        return new FixedTokenRecognizer(
            [
                new FixedTokenDefinition("+", TokenKind.Plus),
                new FixedTokenDefinition("-", TokenKind.Minus),
                new FixedTokenDefinition("*", TokenKind.Multiplication),
                new FixedTokenDefinition(":", TokenKind.Division),
                new FixedTokenDefinition("/", TokenKind.Division),
                new FixedTokenDefinition("^", TokenKind.Power),
                new FixedTokenDefinition("(", TokenKind.OpenParenthesis),
                new FixedTokenDefinition(")", TokenKind.CloseParenthesis),
            ]);
    }
}
