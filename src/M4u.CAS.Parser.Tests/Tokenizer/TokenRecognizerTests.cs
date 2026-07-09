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

        Assert.AreEqual(TokenKind.Identifier, recognizer.HandledTokenKind);
        Assert.IsTrue(result.IsMatch);
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

        Assert.AreEqual(TokenKind.Number, recognizer.HandledTokenKind);
        Assert.IsTrue(result.IsMatch);
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
    public void SingleCharacterTokenRecognizers_Match_WhenCharacterStartsAtIndex_ReturnsOneCharacterToken()
    {
        (ITokenRecognizer recognizer, TokenKind kind, string expression)[] testCases =
        [
            (new PlusTokenRecognizer(), TokenKind.Plus, "+"),
            (new MinusTokenRecognizer(), TokenKind.Minus, "-"),
            (new OpenParenthesisTokenRecognizer(), TokenKind.OpenParenthesis, "("),
            (new CloseParenthesisTokenRecognizer(), TokenKind.CloseParenthesis, ")"),
        ];

        foreach ((ITokenRecognizer recognizer, TokenKind kind, string expression) in testCases)
        {
            TokenRecognizerResult result = recognizer.Match(new TokenRecognizerRequest(expression, 0, null));

            Assert.AreEqual(kind, recognizer.HandledTokenKind);
            Assert.IsTrue(result.IsMatch);
            Assert.AreEqual(1, result.Length);
        }
    }

    [TestMethod]
    public void SingleCharacterTokenRecognizer_Match_WhenDifferentCharacterStartsAtIndex_ReturnsNoMatch()
    {
        PlusTokenRecognizer recognizer = new PlusTokenRecognizer();

        TokenRecognizerResult result = recognizer.Match(new TokenRecognizerRequest("-", 0, null));

        Assert.IsFalse(result.IsMatch);
        Assert.AreEqual(0, result.Length);
    }
}
