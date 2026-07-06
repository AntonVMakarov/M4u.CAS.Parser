using System.Linq.Expressions;

namespace M4u.CAS.Parser;

/// <inheritdoc />
/// <summary>
/// Токенизатор, преобразующий входную строку в список токенов.
/// </summary>
internal class Tokenizer : ITokenizer
{
    private readonly IReadOnlyList<ITokenParser> _tokenParsers;
    private readonly ITokenFactory _tokenFactory;

    public Tokenizer(IReadOnlyList<ITokenParser> tokenParsers, ITokenFactory tokenFactory)
    {
        // Проверяем входные параметры:
        ArgumentNullException.ThrowIfNull(tokenParsers);
        ArgumentNullException.ThrowIfNull(tokenFactory);

        // Сохраняем ссылки на переданные зависимости:
        _tokenParsers = tokenParsers;
        _tokenFactory = tokenFactory;
    }



    /// <inheritdoc />
    /// <exception cref="FormatException">Возникает, когда не удалось распасить символ или последовательность символов
    /// в определённый токен из заданного набора.</exception>
    public TokenizerResult TryParse(TokenizerRequest request)
    {
        // Проверяем входные параметры:
        ArgumentNullException.ThrowIfNull(request);

        // Проверяем токен отмены:
        request.CancellationToken?.ThrowIfCancellationRequested();

        // Список токенов, сформированный на основе входного выражения:
        List<IToken> listOfTokens = new List<IToken>();

        // Вспомогательная переменная:
        bool isTokenIdentified;

        // Проходим по всем элементам входной строки:
        for (int index = 0; index < request.Expression.Length;)
        {            
            // Сразу пропускаем пробелы:
            if (char.IsWhiteSpace(request.Expression[index])) 
            {
                index++;
                continue;
            }

            // Мы еще не сопоставили элементу входной строки expr,
            // начиная с индекса i ни одного токена:
            isTokenIdentified = false;


            // Нужно пройтись по списку парсеров токенов и
            // сопоставить i-элемент входной строки с каждым из поддерживаемых токенов:
            foreach(ITokenParser parser in _tokenParsers)
            {
                // Если мы идентифицировали токен во входной строке начиная с позиции i:
                if (parser.Match(request.Expression, index, out string value))
                {
                    // Добавляем данный токен в результирующий список токенов:
                    listOfTokens.Add(
                        _tokenFactory.Create(parser.HandledType, value)
                        );

                    // вычисляем новый индекс i во входной строке, начиная с которого необходимо 
                    // продолжить анализ:
                    //           1         2
                    // 012345678901234567890123
                    // sin(x)+4.3452-5/x=y*2.35
                    // пусть i = 7 => начиная с этого индекса мы распарсили
                    // число 4.3452, его длина равна 6 =>
                    // новый индекс i с которого нам необходимо продолжить парсинг
                    // равен: 7+length(4.3452)=7+6=13 =>
                    index += listOfTokens[^1].Length;

                    // мы идентифицировали токен:
                    isTokenIdentified = true;

                    // выходим из внутреннего цикла:
                    break;
                }
            }

            // анализируем как мы вышли из внутреннего цикла:
            // если мы так и не сопоставили токен в строке expr, начиная с индекса i
            // ни одному из поддерживаемых токенов:
            if (!isTokenIdentified)
            {
                //throw new UnknownTokenParseException(expr.ToString() + ";" + i.ToString());
                return TokenizerResult.Failure(index, request.Expression[index].ToString());
            }
        }

        // если мы добрались сюда, то мы распарсили входную строку на список токенов =>
        // возвращаем результат:
        return TokenizerResult.Success(listOfTokens);
    }
}
