using System;

    /*Практика «Фильтр Собеля»
    Продолжайте в том же проекте

    Перед преобразованием в черно-белое, хорошо бы каким-то образом выделить границы объектов, 
    чтобы только они стали белыми, а всё остальное черным.

    Оказывается, это не сложно сделать с помощью так называемого фильтра Собеля. 
    Он уже реализован в файле SobelFilterTask.cs. Ваша задача — обобщить этот код. Подробности — в комментариях!

    Выполните эту задачу в файле SobelFilterTask.cs*/

namespace Recognizer
{
    internal static class SobelFilterTask
    {
        public static void WriteGxWith1x1S(double[,] g, double[,] gx, double s)
        {
            var width = g.GetLength(1);
            var height = g.GetLength(0);
            for (int j = 0; j < height; j++)
                for (int i = 0; i < width; i++)
                    gx[j, i] = g[j, i] * s;
        }

        public static void WriteGxWith3x3S(double[,] g, double[,] gx, double[,] s)
        {
            var width = g.GetLength(1);
            var height = g.GetLength(0);
            var grid = new double[3, 3];
            for (int y = 1; y < height - 1; y++)
            {
                for (int x = 1; x < width - 1; x++)
                {
                    for (int j = 0; j < 3; j++)
                        for (int i = 0; i < 3; i++)
                            grid[j, i] = g[y - 1 + j, x - 1 + i] * s[j, i];
                    foreach (var e in grid)
                        gx[y, x] += e;
                    Array.Clear(grid, 0, grid.Length);
                }
            }
        }

        public static void WriteGxWith5x5S(double[,] g, double[,] gx, double[,] s)
        {
            var width = g.GetLength(1);
            var height = g.GetLength(0);
            var grid = new double[5, 5];
            for (int y = 2; y < height - 2; y++)
                for (int x = 2; x < width - 2; x++)
                {
                    for (int j = 0; j < 5; j++)
                        for (int i = 0; i < 5; i++)
                            grid[j, i] = g[y - 2 + j, x - 2 + i] * s[j, i];
                    foreach (var e in grid)
                        gx[y, x] += e;
                    Array.Clear(grid, 0, grid.Length);
                }
        }

        public static double[,] GetSy(double[,] sx)
        {
            var width = sx.GetLength(1);
            var height = sx.GetLength(0);
            var sy = new double[height, width];
            if (width > 1 && (!(sx[0,width/2] == 0) || !(sx[height - 1, width/2] == 0)))
            {
                for (int y = 0; y < height; y++)
                    for (int x = 0; x < width; x++)
                        sy[width - 1 - x, y] = sx[y, x];
            }
            else
            {
                for (int y = 0; y < height; y++)
                    for (int x = 0; x < width; x++)
                        sy[x, width - 1 - y] = sx[y, x];
            }
            return sy;
        }

        public static double[,] Get3x3From5x5(double[,] sx)
        {
            var sx3x3 = new double[3, 3];
            for (int j = 0; j < 3; j++)
                for (int i = 0; i < 3; i++)
                    sx3x3[j, i] = sx[j + 1, i + 1];
            return sx3x3;
        }

        public static void FilterFor1x1Kernel(double[,] g, double[,] gx, double[,] gy, double[,] sx)
        {
            WriteGxWith1x1S(g, gx, sx[0, 0]);
            WriteGxWith1x1S(g, gy, sx[0, 0]);
        }

        public static void FilterFor3x3Kernel(double[,] g, double[,] gx, double[,] gy, double[,] sx)
        {
            var sy = GetSy(sx);
            WriteGxWith1x1S(g, gx, sx[1, 1]);
            WriteGxWith1x1S(g, gy, sy[1, 1]);
            WriteGxWith3x3S(g, gx, sx);
            WriteGxWith3x3S(g, gy, sy);
        }

        public static void FilterFor5x5Kernel(double[,] g, double[,] gx, double[,] gy, double[,] sx)
        {
            var sy = GetSy(sx);
            var sx3x3 = Get3x3From5x5(sx);
            var sy3x3 = Get3x3From5x5(sy);
            WriteGxWith1x1S(g, gx, sx[2, 2]);
            WriteGxWith1x1S(g, gy, sy[2, 2]);
            WriteGxWith5x5S(g, gx, sx);
            WriteGxWith5x5S(g, gy, sy);
        }

        public static double[,] SobelFilter(double[,] g, double[,] sx)
        {
            var width = g.GetLength(1);
            var height = g.GetLength(0);
            var result = new double[height, width];
            var gx = new double[height, width];
            var gy = new double[height, width];
            if (sx.GetLength(0) == 1)
                FilterFor1x1Kernel(g, gx, gy, sx);
            else if (sx.GetLength(0) == 3)
                FilterFor3x3Kernel(g, gx, gy, sx);
            else
                FilterFor5x5Kernel(g, gx, gy, sx);
            for (int j = 0; j < height; j++)
                for (int i = 0; i < width; i++)
                    result[j, i] = Math.Sqrt(gx[j, i] * gx[j, i] + gy[j, i] * gy[j, i]);
            return result;
        }
    }
}