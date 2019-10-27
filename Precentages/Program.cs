using System;
using System.Globalization;

/*Практика «Проценты»
    В этой задаче вам нужно самостоятельно создать с нуля консольное приложение, 
    которое рассчитывает банковские проценты.

    Пользователь должен ввести исходные данные с консоли — три числа через пробел: исходную сумму, 
    процентную ставку (в процентах) и срок вклада в месяцах.

    Программа должна вывести накопившуюся сумму на момент окончания вклада.

    Вот порядок действий:

    Создайте новый проект с типом Console Application.
    В методе Main напишите ввод с помощью Console.ReadLine() и вывод с помощью Console.WriteLine().
    Все вычисление вынесите во вспомогательный метод Calculate. Код этого метода и нужно сдать в этой задаче.
    Детали:

    В конце каждого месяца происходит капитализация — к сумме прибавляется накопившийся за месяц процент. 
    Далее процент вычисляется от этой увеличенной суммы.
    Процентная ставка — годовая (то есть в конце месяца сумма на счете увеличивается на одну двенадцатую ставки)
    Считайте, что вклад открыт в первый день месяца, а срок вклада — это целое количество месяцев.
    Код, решающий основную задачу нужно оформить в виде метода Calculate, принимающего строку, введенную пользователем. 
    В этой задаче гарантируется, что ввод корректный.
    Решите эту задачу без использования циклов!
    
    Пример ввода
    100.00 12 1
    Этот ввод соответствует вкладу 100 рублей под 12% годовых на 1 месяц.

    Пример вывода
    101
    Через месяц на 100 рублей добавится 1% (1/12 от годового процента), значит общая сумма будет 101.*/

namespace Precentages
{
    class Program
    {
        public static double Calculate(string userInput)
        {
            int firstSpaceIndex = userInput.IndexOf(' ');
            int secondSpaceIndex = userInput.LastIndexOf(' ');
            int userInputLength = userInput.Length;
            double originalAmount = double.Parse((userInput.Substring(0, firstSpaceIndex)), 
                CultureInfo.InvariantCulture);
            double interestRate = double.Parse((userInput.Substring(firstSpaceIndex + 1, 
                secondSpaceIndex - firstSpaceIndex - 1)));
            int termOfDeposit = Int32.Parse((userInput.Substring(secondSpaceIndex + 1, 
                userInputLength - secondSpaceIndex - 1)));
            double resultingAmount = originalAmount * Math.Pow((1 + interestRate / (100 * 12)), termOfDeposit);
            return resultingAmount;
        }
        
        static void Main(string[] args)
        {
            Console.Write("Enter deposit parameters: ");
            string userInput = Console.ReadLine();
            var result = Calculate(userInput);
            Console.WriteLine("The amount will be " + result);
        }
    }
}