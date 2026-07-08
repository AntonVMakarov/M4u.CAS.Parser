namespace M4u.CAS.Parser;

/// <summary>
/// Структура которая хранит индекс начала и длину.
/// </summary>
internal readonly record struct TextSpan
{
    public readonly int Start { get; }
    public readonly int Length { get; }

    public TextSpan(int start, int length)
    {
        ArgumentOutOfRangeException.ThrowIfNegative(start);
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(length);

        Start = start;
        Length = length;
    }
}
