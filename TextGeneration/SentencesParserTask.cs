using System.Collections.Generic;
using System.Text;

/*Практика «Парсер предложений»
    Скачайте проект TextAnalysis

    В этом задании нужно реализовать метод в классе SentencesParserTask. Метод должен делать следующее:

    Разделять текст на предложения, а предложения на слова.

    a. Считайте, что слова состоят только из букв (используйте метод char.IsLetter) 
    или символа апострофа ' и отделены друг от друга любыми другими символами.

    b. Предложения состоят из слов и отделены друг от друга одним из следующих символов .!?;:()

    Приводить символы каждого слова в нижний регистр.

    Пропускать предложения, в которых не оказалось слов.

    Метод должен возвращать список предложений, где каждое предложение — это список из одного 
    или более слов в нижнем регистре.*/

namespace TextAnalysis
{
    static class SentencesParserTask
    {
        public static bool IsInASentence(char symbol)
        {
            switch (symbol)
            {
                case '.':
                    return false;
                case '!':
                    return false;
                case '?':
                    return false;
                case ';':
                    return false;
                case ':':
                    return false;
                case '(':
                    return false;
                case ')':
                    return false;
                default:
                    return true;
            }
        }

        public static bool IsInAWord(char symbol)
        {
            return char.IsLetter(symbol) || symbol == '\'';
        }

        public static void AddWordToList(StringBuilder word, List<string> list)
        {
            if (!(word.Length == 0))
            {
                list.Add(word.ToString().ToLower());
                word.Clear();
            }
        }

        public static List<string> ParseASentence(StringBuilder line)
        {
            string str = line.ToString();
            var sentence = new List<string>();
            var word = new StringBuilder();
            foreach (var symbol in str)
            {
                if (IsInAWord(symbol))
                    word.Append(symbol);
                else
                    AddWordToList(word, sentence);
            }
            AddWordToList(word, sentence);
            return sentence;
        }

        public static void AddSentenceToList(List<string> sentence, List<List<string>> list)
        {
            if (!(sentence.Count == 0))
                list.Add(sentence);
        }

        public static List<List<string>> ParseSentences(string text)
        {
            var sentencesList = new List<List<string>>();
            var line = new StringBuilder();
            foreach (var symbol in text)
            {
                if (IsInASentence(symbol))
                    line.Append(symbol);
                else
                {
                    AddSentenceToList(ParseASentence(line), sentencesList);
                    line.Clear();
                }
            }
            AddSentenceToList(ParseASentence(line), sentencesList);
            line.Clear();
            return sentencesList;
        }
    }
}