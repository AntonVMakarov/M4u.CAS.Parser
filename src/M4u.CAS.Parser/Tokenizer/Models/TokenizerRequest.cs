using M4u.CAS.Common;

namespace M4u.CAS.Parser;


/// <summary>
/// Запрос к токенайзеру. 
/// </summary>
public sealed record TokenizerRequest
{
    /// <summary>
    /// Выражение для токенизации.
    /// </summary>
    public string Expression { get; }

    /// <summary>
    /// Необязательный токен отмены.
    /// </summary>
    public IM4uCancellationToken? CancellationToken { get; }

    
    /// <summary>
    /// Открытый конструктор с параметрами.
    /// </summary>
    /// <param name="expression">Выражение для токенизации</param>
    /// <param name="cancellationToken">Токен отмены</param>
    public TokenizerRequest(string expression, IM4uCancellationToken? cancellationToken = null)
    {
        this.Expression = expression;
        this.CancellationToken = cancellationToken;
    }
}
