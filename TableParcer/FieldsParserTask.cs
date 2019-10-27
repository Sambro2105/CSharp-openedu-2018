using System.Collections.Generic;
using System.Text;

    /*Практика «Парсер таблиц»
    Скачайте проект TableParser

    В классе FieldsParserTask реализуйте метод ParseLine, для которого вы создавали тесты в предыдущей задаче.

    Создайте модульные тесты на это решение и перенесите разработанные 
    в прошлой задаче тестовые случаи в модульные тесты.*/

namespace TableParser
{
	public class FieldsParserTask
	{
        // При решении этой задаче постарайтесь избежать создания методов, длиннее 10 строк.
        // Ниже есть метод ReadField — это подсказка. Найдите класс Token и изучите его.
        // Подумайте как можно использовать ReadField и Token в этой задаче.

        public static List<string> ParseLine(string line)
        {
            CutSpacesAtTheBeginning(line);
            var parse = new List<string>();
            var startIndex = 0;
            while (startIndex < line.Length)
            {
                var token = GetNextField(line, startIndex);
                if (token.Value != null)
                {
                    parse.Add(token.Value);
                    startIndex = token.GetIndexNextToToken() + 1;
                }
                else startIndex += 1;
            }
            return parse;
        }

        public static string DeleteUnshieldedBackslashes(string line)
        {
            var parsedLine = new StringBuilder();
            for (int i = 0; i < line.Length; i++)
            {
                if (line[i] == '\\' && !IsShielded(line, i) && ShieldsSomething(line, i))
                {
                    parsedLine.Append(line[i + 1]);
                    i += 1;
                }
                else
                    parsedLine.Append(line[i]);
            }
            return parsedLine.ToString();
        }

        public static bool ShieldsSomething(string line, int position)
        {
            return position != line.Length && line[position + 1] == '\\'
                || line[position + 1] == '\"' || line[position + 1] == '\'';
        }

        private static bool IsShielded(string line, int position)
        {
            return position != 0 && line[position - 1] == '\\' && !IsShielded(line, position - 1);
        }

        public static void CutSpacesAtTheBeginning(string line)
        {
            if (line.Length != 0)
            {
                while (true)
                {
                    if (line[0] == ' ')
                        line = line.Remove(0, 1);
                    else break;
                }
            }
        }

        private static int FindFirstSeparator(string line, int startIndex)
        {
            for (int i = startIndex; i < line.Length; i++)
            {
                if (line[i] == ' ' || line[i] == '\"' || line[i] == '\'')
                    return i;
            }
            return line.Length;
        }

        private static int FindLastSeparator(string line)
        {
            var length = line.Length;
            for (int i = length - 1; i >= 0; i--)
            {
                if (line[i] == ' ' || line[i] == '\"' && !IsShielded(line, i)
                    || line[i] == '\'' && !IsShielded(line, i))
                    return i;
            }
            return -1;
        }

        private static int FindNextUnshielded(string line, int startIndex, char separator)
        {
            for (int i = startIndex; i < line.Length; i++)
            {
                if (line[i] == separator && !IsShielded(line, i))
                    return i;
            }
            return line.Length;
        }

        private static Token GetNextField(string str, int start)
        {
            if (start == 0 && FindFirstSeparator(str, start) != 0)
                return ParseSimpleField(str, 0);
            else
                return ReadField(str, start);
        }

        private static Token ParseSimpleField(string str, int start)
        {
            var length = FindFirstSeparator(str, start);
            var startIndex = start;
            string line = null;
            if (!(length > str.Length))
                line = str.Substring(start, length);
            length -= 1;
            return new Token(line, startIndex, length);
        }

        private static Token ParseTheEnd(string str)
        {
            var lineStart = FindLastSeparator(str) + 1;
            if (lineStart <= str.Length)
            {
                var length = str.Length - lineStart;
                var line = str.Substring(lineStart, length);
                var startIndex = str.Length + 1;
                return new Token(line, startIndex, length);
            }
            else return new Token(null, 0, 0);
        }

        private static Token ParseAfterSpace(string str, int start)
        {
            var length = FindFirstSeparator(str, start + 1) - start - 1;
            var line = str.Substring(start + 1, length);
            var startIndex = start + 1;
            length -= 1;
            if (line == "")
                line = null;
            return new Token(line, startIndex, length);
        }

        private static Token ParseAfterCommas(string str, int start, char comma)
        {
            var length = 0;
            var startIndex = 0;
            string line = null;
            var lineEnd = FindNextUnshielded(str, start + 1, comma);
            if (lineEnd != str.Length)
            {
                length = lineEnd - start - 1;
                startIndex = start + 1;
                line = DeleteUnshieldedBackslashes(str.Substring(startIndex, length));
            }
            else
            {
                length = str.Length - start - 1;
                startIndex = start + 1;
                line = DeleteUnshieldedBackslashes(str.Substring(startIndex, length));
            }
            return new Token(line, startIndex, length);
        }

        private static Token ReadField(string str, int start)
        {
                var separatorPosition = FindFirstSeparator(str, start);
                if (separatorPosition == str.Length)
                {
                    var token = ParseTheEnd(str);
                    return token;
                }
                else if (str[separatorPosition] == ' ')
                {
                    var token = ParseAfterSpace(str, separatorPosition);
                    return token;
                }
                else if (str[separatorPosition] == '\"')
                {
                    var token = ParseAfterCommas(str, separatorPosition, '\"');
                    return token;
                }
                else if (str[separatorPosition] == '\'')
                {
                    var token = ParseAfterCommas(str, separatorPosition, '\'');
                    return token;
                }
            	return new Token(null, 0, 0);
        }
    }
}