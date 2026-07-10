namespace M4u.CAS.Parser;

/// <summary>
/// Класс для распознавания фиксированных символов и преобразования их в соответствующие токены.
/// </summary>
internal class FixedTokenRecognizer : ITokenRecognizer
{
    /// <summary>
    /// Массив определений токенов. Каждый элемент содержит текст и вид токена.
    /// </summary>
    private readonly FixedTokenDefinition[] _definitions;

    /// <summary>
    /// Открытый конструктор с параметром.
    /// </summary>
    /// <param name="definitions">Массив определений токенов.</param>
    public FixedTokenRecognizer(IEnumerable<FixedTokenDefinition> definitions)
    {
        // Проверка на null:
        ArgumentNullException.ThrowIfNull(definitions);

        // Перед получением массива определений токенов, сортируем его по убыванию длины токенов.
        // Это нужно, чтобы в первую очередь сравнивались более длинные строки, например, если в массиве
        // определеней токенов есть элементы:
        // ':' и ':=' и у нас есть выражение 'x:=5', и мы находимся в позиции 1 и сравниваем
        // эту позицию с ':', то распознаватель решить, что ':' в строке 'x:=5' - это деление, тогда как на
        // самом деле - это оператор присваивания ':='. Поэтому и нужна сортировка - для реализации
        // правила максимального совпадения.
        // Преобразование в массив сейчас нужно из-за того, что операция сортировки LINQ работает с отложенным 
        // выполнением и если сразу не преобрзовать в массив, то сортировка произвойдет не сейчас, а когда-то потом
        // и, что самое страшное, будет происходить каждый раз при обращении к массиву. 
        _definitions = definitions.OrderByDescending(d => d.Text.Length).ToArray();
    }


    /// <inheritdoc />
    public TokenRecognizerResult Match(TokenRecognizerRequest request)
    {
        // Проверяем входные данные:
        ArgumentNullException.ThrowIfNull(request);

        // Получаем строку нашего исходного выражение с заданной позиции в виде символов char:
        ReadOnlySpan<char> remaining = request.Expression.AsSpan(request.Index);

        // Проходим по всем определениям:
        foreach (FixedTokenDefinition definition in _definitions) 
        {
            // Проверяем токен отмены:
            request.CancellationToken?.ThrowIfCancellationRequested();

            // Проверяем начинается ли оставшаяся строка (remaining) с текста текущего определения.
            // Ordinal означает точное посимвольное сравнение без учета языковых правил и культуры:
            if (remaining.StartsWith(definition.Text, StringComparison.Ordinal))
            {
                // Совпадение найдено:
                return TokenRecognizerResult.Success(definition.Kind, definition.Text.Length);
            }
        }

        // Совпадений не было:
        return TokenRecognizerResult.NoMatch;
    }
}
