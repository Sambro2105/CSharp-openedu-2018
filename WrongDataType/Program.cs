﻿using System;

//    Неверный тип данных
//    Неправильно указанный тип данных - частая ошибка в работе начинающего программиста.
// Вот и сейчас программист Вася в замешательстве.
//
//    Исправьте положение — укажите у всех объявленных переменных правильные типы вместо многоточия.
// Не используйте ключевое слово var при решении этой задачи.

namespace WrongDataType
{
    class Program
    {
        public static void Main()
        {
            double num1 = +5.5e-2;
            float num2 = 7.8f;
            int num3 = 0;
            long num4 = 2000000000000L;
            Console.WriteLine(num1);
            Console.WriteLine(num2);
            Console.WriteLine(num3);
            Console.WriteLine(num4);
        }
    }
}