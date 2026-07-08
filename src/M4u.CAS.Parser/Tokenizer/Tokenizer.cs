using M4u.CAS.Common;
using System.Linq.Expressions;
using System.Net.Http.Headers;

namespace M4u.CAS.Parser;

/// <inheritdoc />
/// <summary>
/// Токенизатор, преобразующий входную строку в список токенов.
/// </summary>
internal class Tokenizer : ITokenizer
{
    private readonly IReadOnlyList<ITokenParser> _tokenParsers;
    //private readonly ITokenFactory _tokenFactory;

    public Tokenizer(IReadOnlyList<ITokenParser> tokenParsers/*, ITokenFactory tokenFactory*/)
    {
        // Проверяем входные параметры:
        ArgumentNullException.ThrowIfNull(tokenParsers);
        //ArgumentNullException.ThrowIfNull(tokenFactory);

        // Сохраняем ссылки на переданные зависимости:
        _tokenParsers = tokenParsers;
        //_tokenFactory = tokenFactory;
    }


    /// <inheritdoc />
    /// <exception cref="FormatException">Возникает, когда не удалось распасить символ или последовательность символов
    /// в определённый токен из заданного набора.</exception>
    public TokenizerResult TryParse(TokenizerRequest request)
    {
        // Проверяем входные параметры:
        ArgumentNullException.ThrowIfNull(request);
        ArgumentNullException.ThrowIfNull(request.Expression);

        // Получаем строку математического выражения:
        string expr = request.Expression;

        // Получаем токен отмены:
        IM4uCancellationToken? ct = request.CancellationToken;

        // Проверяем токен отмены:
        ct?.ThrowIfCancellationRequested();

        // Список токенов, сформированный на основе входного выражения:
        List<Token> listOfTokens = new List<Token>();

        // Список кортежей (вид токена, длина распознанного выражения) от парсеров, которым
        // удалось что-то распознать:
        List<(TokenKind parsedTokenKind, int parsedLength)> matches = new List<(TokenKind, int)>();

        // Проходим по всем элементам входной строки:
        for (int index = 0; index < request.Expression.Length;)
        {
            // Проверяем токен отмены:
            ct?.ThrowIfCancellationRequested();

            // Сразу пропускаем пробелы:
            if (char.IsWhiteSpace(request.Expression[index]))
            {
                index++;
                continue;
            }

            // Проходим по списку парсеров токенов и смотрить какой парсер подходит
            // для i-ого элемента входной строки:
            foreach (ITokenParser parser in _tokenParsers)
            {
                // Проверяем токен отмены:
                ct?.ThrowIfCancellationRequested();

                // Пытаемся распарсить символы строки (начиная с index) очередным парсером:
                TokenParserResult parserResult = parser.Match(new TokenParserRequest(expr, index, ct));

                // Проверяем распознал что-то парсер или нет:
                if (parserResult.IsMatch)
                {
                    // Этот парсер что-то распознал:
                    matches.Add((parser.HandledKind, parserResult.Length));
                }
            }

            // Итак, мы попробовали все парсеры, которые у нас есть для парсинга входной строки в токен, 
            // начиная с i-ой позиции. 
            // Анализируем результат:
            if (matches.Count == 0)
            {
                // Мы не нашли ни одного подходящего парсера для символа, находящегося в i-ой позиции во
                // входной строке:
                return TokenizerResult.Failure(index, expr[index..]);
            }
            else
            {
                // Один или несколько парсеров смогли распарсить в токен символ, который начинается в i-ой позиции
                // во входной строке. Нам нужно выбрать самый первый самый длинный результат:
                //(TokenKind parsedTokenKind, int parsedLength) = matches.MaxBy(r => r.parsedLength);
                var bestMatch = matches.MaxBy(r => r.parsedLength);

                // Создаём на основе полученных результатов токен:
                Token token = new Token(
                    // Получаем распознанное выражение:
                    expr[index..(index + bestMatch.parsedLength)], 
                    // И соответствующий этому выражению вид токена:
                    bestMatch.parsedTokenKind
                    );

                // Добавляем токен в результирующий список:
                listOfTokens.Add(token);

                // Очищаем список с совпадениями:
                matches.Clear();

                // Теперь вычисляем новый индекс i во входной строке, начиная с которого необходимо 
                // продолжить анализ:
                //           1         2
                // 012345678901234567890123
                // sin(x)+4.3452-5/x=y*2.35
                // пусть i = 7 => начиная с этого индекса мы распарсили
                // число 4.3452, его длина равна 6 =>
                // новый индекс i с которого нам необходимо продолжить парсинг
                // равен: 7+length(4.3452)=7+6=13 =>
                index += bestMatch.parsedLength;
            }
        }

        // Возвращаем результат:
        return TokenizerResult.Success(listOfTokens);
    }
}
