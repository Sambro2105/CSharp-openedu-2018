using System;
using System.Globalization;

//    Преобразование строки в число
//    Вася написал код, прибавляющий к числу единичку, но он опять не работает :(
//
//    Немедленно помогите Васе, иначе он решит, что программирование слишком сложно для него!

namespace ConvertStringToNumber
{
    class Program
    {
        public static void Main()
        {
            string doubleNumber = "894376.243643";
            double number = double.Parse(doubleNumber, CultureInfo.InvariantCulture); // Вася уверен, что ошибка где-то тут
            Console.WriteLine(number + 1);
        }
    }
}