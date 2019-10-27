using System;
using System.Collections.Generic;

    /*Практика «Частотность N-грамм»
    Продолжайте работу в том же проекте.

    N-грамма — это N соседних слов в одном предложении. 2-граммы называют биграммами. 3-граммы — триграммами.

    Например, из текста: "She stood up. Then she left." можно выделить следующие биграммы 
    "she stood", "stood up", "then she" и "she left", но не "up then". 
    И две триграммы "she stood up" и "then she left", но не "stood up then".

    По списку предложений, составленному в прошлой задаче, составьте словарь самых частотных 
    продолжений биграмм и триграмм. Это словарь, ключами которого являются все возможные начала биграмм и триграмм, 
    а значениями — их самые частотные продолжения.

    Более формально так:

    Для каждой пары (key, value) из словаря должно выполняться одно из следующих условий:

    В тексте есть хотя бы одна биграмма (key, value), и для любой другой присутствующей 
    в тексте биграммы (key, otherValue), начинающейся с того же слова, 
    value должен быть лексикографически меньше otherValue.

    Либо в тексте есть хотя бы одна триграмма (w1, w2, value), такая что w1 + " " + w2 == key и для 
    любой другой присутствующей в тексте триграммы (w1, w2, otherValue), начинающейся с той же пары слов, 
    value должен быть лексикографически меньше otherValue.

    Для лексикографического сравнения используйте встроенный в .NET способ сравнения Ordinal, 
    например с помощью метода string.CompareOrdinal.

    Такой словарь назовём N-граммной моделью текста.

    Реализуйте этот алгоритм в классе FrequencyAnalysisTask.

    Все вопросы и детали уточняйте с помощью примера ниже и тестов.

    Пример
    По тексту a b c d. b c d. e b c a d. должен быть составлен такой словарь:
        "a": "b"
        "b": "c"
        "c": "d"
        "e": "b"
        "a b": "c"
        "b c": "d"
        "e b": "c"
        "c a": "d"
    
    Обратите внимание:

    -из двух биграмм "a b" и "a d", встречающихся однократно, в словаре есть только пара "a": "b", 
        как лексикографически меньшая.
    -из двух встречающихся в тексте биграмм "c d" и "c a" в словаре есть только более частотная пара "c": "d".
    -из двух триграмм "b c d" и "b c a" в словаре есть только более частотная "b c": "d".*/

namespace TextAnalysis
{
    static class FrequencyAnalysisTask
    {
        public static Dictionary<Tuple<string, string>, int> CountBigramms(List<List<string>> text)
        {
            var count = new Dictionary<Tuple<string, string>, int>();
            foreach (var sentence in text)
            {
                var amountOfWords = sentence.Count;
                if (amountOfWords >= 2)
                {
                    for (int i = 0; i < amountOfWords - 1; i++)
                    {
                        var bigram = new Tuple<string, string>(sentence[i], sentence[i + 1]);
                        if (count.ContainsKey(bigram))
                            count[bigram] += 1;
                        else
                            count.Add(bigram, 1);
                    }
                }
            }
            return count;
        }

        public static Dictionary<Tuple<string, string>, int> CountTrigramms(List<List<string>> text)
        {
            var count = new Dictionary<Tuple<string, string>, int>();
            foreach (var sentence in text)
            {
                var amountOfWords = sentence.Count;
                if (amountOfWords >= 3)
                {
                    for (int i = 0; i < amountOfWords - 2; i++)
                    {
                        var bigram = new Tuple<string, string>(sentence[i] + ' ' + sentence[i + 1], sentence[i + 2]);
                        if (count.ContainsKey(bigram))
                            count[bigram] += 1;
                        else
                            count.Add(bigram, 1);
                    }
                }
            }
            return count;
        }

        public static string ReturnTheLeast(string str1, string str2)
        {
            if (string.CompareOrdinal(str1, str2) <= 0)
                return str1;
            else return str2;
        }

        public static void AddNgramms(Dictionary<Tuple<string, string>, int> count, 
									  Dictionary<string, string> dictionary)
        {
            foreach (var bigram in count)
            {
                if (!dictionary.ContainsKey(bigram.Key.Item1))
                    dictionary.Add(bigram.Key.Item1, bigram.Key.Item2);
                else
                {
                    var pair = Tuple.Create(bigram.Key.Item1, dictionary[bigram.Key.Item1]);
                    if (count[bigram.Key] > count[pair])
                        dictionary[bigram.Key.Item1] = bigram.Key.Item2;
                    else if (count[bigram.Key] == count[pair])
                        dictionary[bigram.Key.Item1] = ReturnTheLeast(bigram.Key.Item2, pair.Item2);
                    else continue;
                }
            }
        }

        public static Dictionary<string, string> GetMostFrequentNextWords(List<List<string>> text)
        {
            var result = new Dictionary<string, string>();
            AddNgramms(CountBigramms(text), result);
            AddNgramms(CountTrigramms(text), result);
            return result;
        }
    }
}