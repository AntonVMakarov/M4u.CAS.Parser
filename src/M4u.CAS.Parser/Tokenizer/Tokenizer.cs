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

        //Dictionary<string, TokenKind> dict = new Dictionary<string, TokenKind>();

        // Список парсеров, которые могут распирсить токен, начиная с
        // i-ого элемента входной строки:
        //List<ITokenParser> listOfParsers = new List<ITokenParser>();

        List<TokenParserResult> listOfParsedResults = new List<TokenParserResult>();

        // Вспомогательная переменная:
        //bool isTokenIdentified;

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

            // Мы еще не сопоставили элементу входной строки expr,
            // начиная с индекса i ни одного токена:
            //isTokenIdentified = false;

            // Проходим по списку парсеров токенов и смотрить какой парсер подходит
            // для i-ого элемента входной строки:
            foreach (ITokenParser parser in _tokenParsers)
            {
                // Проверяем подходит ли нам очередной парсер:
                TokenParserResult parserResult = parser.Match(new TokenParserRequest(expr, index, ct));

                // Да, подходит:
                if (parserResult is not null && parserResult.IsParsed)
                {
                    // Запоминаем то, что он нам распарсил:
                    listOfParsedResults.Add(parserResult);
                }
            }

            // Итак, мы попробовали все парсеры, которые у нас есть для парсинга входной строки в токен, 
            // начиная с i-ой позиции. 
            // Анализируем результат:
            if (listOfParsedResults.Count == 0)
            {
                // Мы не нашли ни одного подходящего парсера для символа, находящегося в i-ой позиции во
                // входной строке:
                return TokenizerResult.Failure(index, expr[index..]);
            }
            else
            {
                // Один или несколько парсеров смогли распарсить в токен символ, который начинается в i-ой позиции
                // во входной строке. Нам нужно выбрать самый первый самый длинный результат:
                TokenParserResult? result = listOfParsedResults.MaxBy(r => r.ParsedValue.Length);

                // Создаём на основе полученных результатов токен:
                Token token = new Token(result.ParsedValue, (TokenKind)result.TokenKind);

                // Добавляем токен в результирующий список:
                listOfTokens.Add(token);

                // Очищаем список с результатами:
                listOfParsedResults.Clear();

                // Теперь вычисляем новый индекс i во входной строке, начиная с которого необходимо 
                // продолжить анализ:
                //           1         2
                // 012345678901234567890123
                // sin(x)+4.3452-5/x=y*2.35
                // пусть i = 7 => начиная с этого индекса мы распарсили
                // число 4.3452, его длина равна 6 =>
                // новый индекс i с которого нам необходимо продолжить парсинг
                // равен: 7+length(4.3452)=7+6=13 =>
                index += token.Value.Length;
            }
        }

        // Возвращаем результат:
        return TokenizerResult.Success(listOfTokens);
    }
}
