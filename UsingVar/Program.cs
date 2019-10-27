﻿using System;

//    Использование var
//    С прибавлением единицы все оказалось просто. Казалось бы прибавление к числу половинки должно быть не сложнее...
//
//    Подумайте, как так получилось, что казалось бы корректная программа не работает.
//    Исправьте первую строчку программы так, чтобы она компилировалась и выводила на консоль ожидаемый ответ — 5.5.

namespace UsingVar
{
    class Program
    {
        static public void Main()
        {
            double a = 5; // ← исправьте эту строку
            a += 0.5;
            Console.WriteLine(a);
        }
    }
}