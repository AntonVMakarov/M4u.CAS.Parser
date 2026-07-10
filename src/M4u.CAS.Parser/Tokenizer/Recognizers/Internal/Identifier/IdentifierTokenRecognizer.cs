namespace M4u.CAS.Parser;

/// <summary>
/// Парсер токена переменной
/// Переменная должна начинаться с буквы и может содержать как единичный символ (букву), так и 
/// состоять из множества символов (букв и/или цифр):
/// x - подходит
/// xy - подходит
/// x235 - подходит
/// x235y - подходит
/// 234x - не подходит
/// </summary>
internal class IdentifierTokenRecognizer : ITokenRecognizer
{
    /// <summary>
    /// Вид токена, который распознает данный парсер.
    /// </summary>
    public readonly TokenKind _tokenKind = TokenKind.Identifier; 

    /// <inheritdoc />
    public TokenRecognizerResult Match(TokenRecognizerRequest request)
    {
        // Вспомогательные переменные:
        string expr = request.Expression;
        int j, i = request.Index;

        // Начинаем анализировать стартовый индекс в строке на предмет того, что он является переменной или функцией:
        // Нам нужна эта строчка, потому что переменная или функция могут начинаться только с указанных символов:
        if ((expr[i] >= 'a' && expr[i] <= 'z') || (expr[i] >= 'A' && expr[i] <= 'Z'))
        {
            // Идём вперёд по строке:
            for (j = i + 1; j < expr.Length; j++)
            {
                // если мы нашли какой-нибудь символ, не относящийся к переменной или функции:
                if ((expr[j] < 'a' || expr[j] > 'z') && (expr[j] < 'A' || expr[j] > 'Z') && (expr[j] < '0' || expr[j] > '9'))
                {
                    // Прекращаем цикл:
                    break;                    
                }    
            }

            // Мы точно нашли символы, относящиеся к переменной|функции.
            // Извлекаем их:
            //   i  j
            // 0123456789
            // x+x12/5
            // length = 5-2 = j-i = 3
            return TokenRecognizerResult.Success(_tokenKind, j - i);
        }
        else return TokenRecognizerResult.NoMatch;
    }
}
