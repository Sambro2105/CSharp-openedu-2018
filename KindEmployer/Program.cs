﻿using System;

//    Добрый работодатель
//    Вася до завтра должен написать важную подпрограмму для Доброго Работодателя. Оставалось дописать всего один метод,
//     когда Вася от переутомления крепчайше заснул.
//
//    Напишите метод, который принимает на вход имя и зарплату и возвращает строку вида:
//     Hello, <Name>, your salary is <Salary>.
//
//    Но так как Работодатель Добр, он всегда округляет зарплату до ближайшего целого числа вверх.
//
//    Во многих редакторах и IDE сочетание клавиш Ctrl + Space показывает контекстную подсказку.
//     Тут подсказки также работают, однако внутри Visual Studio они гораздо полнее и удобнее.

namespace KindEmployer
{
    class Program
    {
        private static string GetGreetingMessage(string name, double salary)
        {
            // возвращает "Hello, <name>, your salary is <salary>"
            return "Hello, " + name + ", your salary is " + Math.Ceiling(salary);
        }
        
        public static void Main()
        {
            Console.WriteLine(GetGreetingMessage("Student", 10.01));
            Console.WriteLine(GetGreetingMessage("Bill Gates", 10000000.5));
            Console.WriteLine(GetGreetingMessage("Steve Jobs", 1));
        }
    }
}