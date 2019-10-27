using System.Collections.Generic;

    /*Практика «Пороговый фильтр»
    Продолжайте в том же проекте

    Пора превратить изображение в черно-белое.

    Сделать это можно с помощью порогового преобразования. Реализуйте его в методе

    public static double[,] ThresholdFilter(double[,] original, double whitePixelsFraction)

    Метод должен заменять пиксели со значением больше либо равному T на белый (1.0), а остальные на черный (0.0).

    Пороговое значение найдите так, чтобы:

    если N — общее количество пикселей изображения, то как минимум (int)(whitePixelsFraction*N) пикселей стали белыми;
    при этом белыми стало как можно меньше пикселей.
    Выполните эту задачу в файле ThresholdFilterTask.cs*/

namespace Recognizer
{
    public static class ThresholdFilterTask
    {
        public static int GetAmountOfWhite(double[,] original)
        {
            var sum = 0;
            foreach (var e in original)
            {
                if (e == 1.0)
                    sum += 1;
            }
            return sum;
        }

        public static void BlackWhiteImage(double[,] original, double t, int minAmountOfWhite)
        {
            var xLength = original.GetLength(0);
            var yLength = original.GetLength(1);
            for (int i = 0; i < xLength; i++)
            {
                for (int j = 0; j < yLength; j++)
                {
                    if (original[i, j] >= t && !(t == -1))
                        original[i, j] = 1.0;
                    else
                        original[i, j] = 0.0;
                }
            }
            var leftToBeWhitened = minAmountOfWhite - GetAmountOfWhite(original);
            if (leftToBeWhitened > 0)
            {
                for (int i = 0; i < xLength; i++)
                {
                    for (int j = 0; j < yLength; j++)
                        original[i, j] = 1.0;
                }
            }
        }

        public static double[,] ThresholdFilter(double[,] original, double whitePixelsFraction)
        {
            var xLength = original.GetLength(0);
            var yLength = original.GetLength(1);
            var n = xLength * yLength;
            var minAmountOfWhite = (int)(n * whitePixelsFraction);
            var allValues = new List<double>();
            foreach (var e in original)
                allValues.Add(e);
            allValues.Sort();
            allValues.Add(-1);
            var t = allValues[n - minAmountOfWhite];
            BlackWhiteImage(original, t, minAmountOfWhite);
            return original;
        }
    }
}