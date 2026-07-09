namespace M4u.CAS.Parser;

/// <summary>
/// Структура которая хранит индекс начала и длину.
/// </summary>
public readonly record struct TextSpan
{
    public readonly int Start { get; }
    public readonly int Length { get; }

    public TextSpan(int start, int length)
    {
        ArgumentOutOfRangeException.ThrowIfNegative(start);
        ArgumentOutOfRangeException.ThrowIfNegative(length);

        // Нулевая длина length разрешена.
        // Нулевой диапазон потребуется для ошибок в конце строки:
        // sin(x
        //      ^здесь ожидалась ')'
        // Позиция ошибки будет:
        // new TextSpan(expression.Length, 0)


        Start = start;
        Length = length;
    }
}
