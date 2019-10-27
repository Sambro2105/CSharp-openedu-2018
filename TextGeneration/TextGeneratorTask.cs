using System.Collections.Generic;
using System.Text;

/*Практика «Продолжение текста»
Продолжайте работу в том же проекте.

В классе TextGeneratorTask реализуйте алгоритм продолжения текста по N-граммной модели.

Описание алгоритма:

На вход алгоритму передается словарь nextWords, полученный в предыдущей задаче, 
одно или несколько первых слов фразы phraseBeginning и wordsCount — количество слов, 
которые нужно дописать к phraseBeginning.

Словарь nextWords в качестве ключей содержит либо отдельные слова, либо пары слов, соединённые через пробел. 
По ключу key содержится слово, которым нужно продолжать фразы, заканчивающиеся на key.

Алгоритм должен работать следующим образом:

Итоговая фраза должна начинаться с phraseBeginning.
Алгоритм дописывает по одному слову к фразе wordsCount слов следующим образом:

a. Если фраза содержит как минимум два слова и в словаре есть ключ, состоящий из двух последних слов фразы, 
то продолжать нужно словом, хранящемся в словаре по этому ключу.

b. Иначе, если в словаре есть ключ, состоящий из одного последнего слова фразы, 
то продолжать нужно словом, хранящемся в словаре по этому ключу.

c. Иначе, нужно закончить генерирование фразы.

Проверяющая система сначала запустит эталонный способ разделения исходного текста на предложения и слова, 
потом эталонный способ построения словаря наиболее частотных продолжений из предыдущей задачи, 
а затем вызовет реализованный вами метод. В случае ошибки вы увидите исходный текст, 
на котором запускался процесс тестирования.

Если запустить проект на выполнение, он предложит ввести начало фразы и сгенерирует продолжение. 
Позапускайте алгоритм на разных текстах и разных фразах. Результат может быть интересным!

О применении N-граммных моделей
Подобные N-граммные модели текстов часто используются в самых разных задачах обработки текстов. 
Когда поисковая строка предлагает вам продолжение вашей фразы — скорее всего это результат работы подобного алгоритма.

Сравнивая частоты N-грамм можно сравнивать тексты на похожесть и искать плагиат.

Опираясь на N-граммные модели текстов можно улучшать алгоритмы исправления опечаток или автокоррекции вводимого текста.*/

namespace TextAnalysis
{
    static class TextGeneratorTask
    {
        public static int GetAmountOfWords(string[] phrase)
        {
            int count = 0;
            foreach (var word in phrase)
            {
                if (!(word == null))
                    count += 1;
                else return count;
            }
            return count;
        }

        public static bool HasEmptySpace(string[] phrase)
        {
            foreach (var item in phrase)
            {
                if (item == null)
                    return true;
            }
            return false;
        }

        public static string GetLastTwo(string[] phrase)
        {
            if (!(GetAmountOfWords(phrase) >= 2))
                return null;
            for (int i = 0; i < phrase.Length; i++)
            {
                if (phrase[i] == null)
                    return phrase[i - 2] + ' ' + phrase[i - 1];
            }
            return null;
        }

        public static string GetLastOne(string[] phrase)
        {
            if (!(GetAmountOfWords(phrase) >= 1))
                return null;
            for (int i = 0; i < phrase.Length; i++)
            {
                if (phrase[i] == null)
                    return phrase[i - 1];
            }
            return null;
        }

        public static List<string> ParseASentence(string str)
        {
            var sentence = new List<string>();
            var word = new StringBuilder();
            foreach (var symbol in str)
            {
                if (!(symbol == ' '))
                    word.Append(symbol);
                else
                {
                    sentence.Add(word.ToString());
                    word.Clear();
                }
            }
            if (word.Length > 0)
                sentence.Add(word.ToString());
            return sentence;
        }

        public static void AddAWord (string[] phrase, Dictionary<string, string> nextWords)
        {
            var wordscount = GetAmountOfWords(phrase);
            var lastTwo = GetLastTwo(phrase);
            var lastOne = GetLastOne(phrase);
            if (wordscount > 0 && HasEmptySpace(phrase))
            {
                if (!(lastTwo == null) && nextWords.ContainsKey(lastTwo))
                    phrase[wordscount] = nextWords[lastTwo];
                else if (!(lastOne == null) && nextWords.ContainsKey(lastOne))
                    phrase[wordscount] = nextWords[lastOne];
            }
        }

        public static string ContinuePhrase(
            Dictionary<string, string> nextWords,
            string phraseBeginning,
            int wordsCount)
        {
            string line = null; 
            var beginning = ParseASentence(phraseBeginning);
            var phrase = new string[wordsCount + beginning.Count];
            for (int i = 0; i < beginning.Count; i++)
                phrase[i] = beginning[i];
            for (int i = 0; i < wordsCount; i++)
            {
                AddAWord(phrase, nextWords);
            }
            for (int i = 0; i< phrase.Length; i++)
            {
                if (phrase[i] == null)
                    break;
                line += ' ' + phrase[i];
            }
            line = line.Substring(1);
            return line;
        }
    }
}