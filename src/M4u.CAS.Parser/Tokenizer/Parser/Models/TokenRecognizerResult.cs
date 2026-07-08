namespace M4u.CAS.Parser;


/// <summary>
/// Результат распознавания символов входной строки.
/// </summary>
internal readonly record struct TokenRecognizerResult
{
    /// <summary>
    /// Количество символов, которые парсеру удалось распознать.
    /// </summary>
    public int Length { get; }


    /// <summary>Логическая переменная, показывающая удалось ли выполнить парсинг.</summary>
    public bool IsMatch => Length > 0;


    /// <summary>
    /// Приветный конструктор
    /// </summary>
    /// <param name="length">Количество символов, распознанных парсером.</param>
    private TokenRecognizerResult(int length)
    {
        Length = length;
    }


    /// <summary>
    /// Фабричный метод обозначающий, что совпадения не было (парсинг не состоялся).
    /// </summary>
    public static TokenRecognizerResult NoMatch => new TokenRecognizerResult(0);


    /// <summary>
    /// Фабричный метод обозначающий, что совпадение было (парсинг состоялся).
    /// </summary>
    /// <param name="Length">Длина совпадения</param>
    public static TokenRecognizerResult Success(int Length)
    {
        // Проверяем входной аргумент:
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(Length);

        // Возвращаем результат:
        return new TokenRecognizerResult(Length);
    }
}
