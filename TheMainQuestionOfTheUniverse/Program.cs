using System;

//    Главный вопрос Вселенной
//    Многие знают, что ответ на главный вопрос жизни, вселенной и всего такого — 42. Но Вася хочет большего!
//     Он желает знать квадрат этого числа!
//
//    Вы разделили с Васей работу — он написал главный метод Main,
//     а вам осталось лишь дописать методы Print и GetSquare.
//
//    Создайте два метода Print и GetSquare, так, чтобы код снизу заработал.
//     сли забыли синтаксис определения методов — подсмотрите в видеолекции или в предыдущие задачи.

namespace TheMainQuestionOfTheUniverse
{
    class Program
    {
        private static int GetSquare(int a)
        {
            return a * a;
        }

        private static void Print(int a)
        {
            Console.WriteLine(a);
        }
        
        public static void Main()
        {
            Print(GetSquare(42));
        }
    }
}