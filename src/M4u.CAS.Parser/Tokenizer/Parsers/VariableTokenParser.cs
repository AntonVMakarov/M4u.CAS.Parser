namespace M4u.CAS.Parser;

/// <summary>Парсер токена переменной</summary>
internal class VariableTokenParser : ITokenParser
{
    /// <inheritdoc />
    public TokenType HandledType => TokenType.Variable; 


    /// <inheritdoc />
    public bool Match(string expr, int i, out string value)
    {
        // начинаем анализировать токен на предмет того, что он является переменной:
        if ((expr[i] >= 'a' && expr[i] <= 'z') || (expr[i] >= 'A' && expr[i] <= 'Z'))
        {
            // главное тут отличить переменную от функции, отличие в том, что после имени функции
            // идет открывающаяся скобка, а после имени переменной - нет:

            // продолжаем проверку:
            for (int j = i + 1; j < expr.Length; j++)
            {
                // если в какой-то момент мы нашли открывающуюся скобку, то у нас функция =>
                if (expr[j] == '(') { value = ""; return false; }
                // если мы нашли какой-нибудь символ, не относящийся к переменной:
                else if ((expr[j] < 'a' || expr[j] > 'z') && (expr[j] < 'A' || expr[j] > 'Z') && (expr[j] < '0' || expr[j] > '9'))
                {
                    // значит имя переменной у нас закончилось, и это не функция, т.е. если 
                    // expr[j] == '(', то выполнилось бы первое условие:
                    // => извлекаем имя переменной:
                    //   i  j
                    // 0123456789
                    // x+x12/5
                    // length = 5-2 = j-i = 3
                    value = expr.Substring(i, j - i);
                    // возвращаем результат:
                    return true;
                }
            }

            // получается, что если мы сюда попали, то у нас кончилась строка, но открывающей скобки найдено не было => это не функция =>
            // это переменная => извлекаем её имя:
            //   i  j
            // 012345
            // x+x12
            // length = 5-2 = j-i = 3
            value = expr.Substring(i, expr.Length - i);
            // возвращаем результат:
            return true;
        }
        // шаблон не подходит:
        else { value = ""; return false; }
    }
}
