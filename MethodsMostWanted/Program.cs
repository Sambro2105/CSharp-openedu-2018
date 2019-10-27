using System;

//    Разыскиваются методы!
//    Напишите тело метода так, чтобы он возвращал вторую половину строки text, из которой затем удалены пробелы.
//     Можете считать, что text всегда четной длины.
//
//    Всю информацию о доступных методах класса String вы можете прочитать в официальной документации .NET

namespace MethodsMostWanted
{
    class Program
    {
        static string GetLastHalf(string text)
        {
            text = text.Substring(text.Length/2);
            text = text.Replace(" ", null);
            return text;
        }
        
        static void Main(string[] args)
        {
            Console.WriteLine(GetLastHalf("I love CSharp!"));
            Console.WriteLine(GetLastHalf("1234567890"));
            Console.WriteLine(GetLastHalf("до ре ми фа соль ля си"));
        }
    }
}