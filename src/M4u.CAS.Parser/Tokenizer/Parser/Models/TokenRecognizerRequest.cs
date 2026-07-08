using M4u.CAS.Common;

namespace M4u.CAS.Parser;

/// <summary>
/// Модель данных, описывающая запрос к распознавателю токенов.
/// </summary>
internal sealed record TokenRecognizerRequest
{
    /// <summary>
    /// Входная строка с математическим выражением.
    /// </summary>
    public string Expression { get; }

    /// <summary>
    /// Индекс в этой строке с которого нужно проверять соответствие:
    /// </summary>
    public int Index { get; }

    /// <summary>
    /// Необязательный токен отмены.
    /// </summary>
    public IM4uCancellationToken? CancellationToken { get; }


    public TokenRecognizerRequest(string expression, int index, IM4uCancellationToken? cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(expression);
        ArgumentOutOfRangeException.ThrowIfNegative(index, nameof(index));
        ArgumentOutOfRangeException.ThrowIfGreaterThanOrEqual(index, expression.Length, nameof(index));

        Expression = expression;
        Index = index;
        CancellationToken = cancellationToken;
    }
}
