using System;
using System.Collections.Generic;
using System.Linq;

    /*Практика «Медианный фильтр»
    Продолжайте в том же проекте

    Перед преобразованием в черно-белое, с изображения лучше бы удалить шум.

    Для этого обработайте его так называемым медианным фильтром. 
    Каждый пиксель изображения нужно заменить медианой всех пикселей в 1-окрестности этого пикселя. 
    То есть для внутреннего пикселя, это будет медиана 9 значений. А для углового — медиана 4 значений.

    Медианой четного количества значений для определённости считайте среднее арифметическое двух средних значений.

    Выполните эту задачу в файле MedianFilterTask.cs*/

namespace Recognizer
{
    internal static class MedianFilterTask
    {
        /* 
		 * Для борьбы с пиксельным шумом, подобным тому, что на изображении,
		 * обычно применяют медианный фильтр, в котором цвет каждого пикселя, 
		 * заменяется на медиану всех цветов в некоторой окрестности пикселя.
		 * https://en.wikipedia.org/wiki/Median_filter
		 * 
		 * Используйте окно размером 3х3 для не граничных пикселей,
		 * Окно размером 2х2 для угловых и 3х2 или 2х3 для граничных.
		 */
        
        public static double GetMedian(List<double> grid)
        {
            grid.Sort();
            switch (grid.Count)
            {
                case 1:
                    return grid[0];
                case 2:
                    return (grid[0] + grid[1]) * 0.5;
                case 3:
                    return grid[1];
                case 4:
                    return (grid[1] + grid[2]) * 0.5;
                case 6:
                    return (grid[2] + grid[3]) * 0.5;
                case 9:
                    return grid[4];
                default:
                    return 0;
            }
        }

        public static void CheckSides(double[,] original, List<double> grid, int i, int j)
        {
            var xLength = original.GetLength(0);
            var yLength = original.GetLength(1);
            if (!(j == 0))
                grid.Add(original[i, j - 1]);
            if (!(j == yLength - 1))
                grid.Add(original[i, j + 1]);
        }

        public static List<double> GetAGrid(double[,] original, int i, int j)
        {
            var xLength = original.GetLength(0);
            var yLength = original.GetLength(1);
            var grid = new List<double>();
            grid.Add(original[i, j]);
            CheckSides(original, grid, i, j);
            if (!(i == 0))
            {
                grid.Add(original[i - 1, j]);
                CheckSides(original, grid, i - 1, j);
            }
            if (!(i == xLength - 1))
            {
                grid.Add(original[i + 1, j]);
                CheckSides(original, grid, i + 1, j);
            }
            return grid;
        }

        public static double[,] MedianFilter(double[,] original)
        {
            var xLength = original.GetLength(0);
            var yLength = original.GetLength(1);
            var filteredOriginal = new double[xLength, yLength];
            for (int i = 0; i < xLength; i++)
            {
                for (int j = 0; j < yLength; j++)
                    filteredOriginal[i, j] = GetMedian(GetAGrid(original, i, j));
            }
            return filteredOriginal;
        }
    }
}