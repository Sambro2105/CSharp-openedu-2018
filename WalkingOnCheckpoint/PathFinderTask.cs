using System;
using System.Collections.Generic;
using System.Drawing;

    /*Практика «Хождение по чекпоинтам»
    Скачайте проект route-planning.

    Роботу нужно проехать через указанные точки, посетив каждую хотя бы один раз. Нужно спланировать маршрут так, 
    чтобы суммарный путь был минимален.

    TSP problem

    В файле PathFinderTask допишите код функции int[] FindBestCheckpointsOrder(Point[] checkpoints).

    Функция принимает массив чекпоинтов. Робот изначально находится в точке checkpoints[0]. 
    Вернуть нужно порядок посещения чекпоинтов. Например, если функция возвращает массив {0,2,1}, это означает, 
    что робот сначала поедет в чекпоинт с индексом 2, а из него в чекпоинт с индексом 1 и на этом закончит свой путь.

    Действуйте как на лекциях, можете адаптировать код с лекций. Функция должна быть рекурсивной.

    Реализуйте следующую оптимизацию (отсечение перебора): прекращайте перебор, если текущая длина пути уже больше, 
    чем минимальный путь, найденный ранее.*/

namespace RoutePlanning
{
	public static class PathFinderTask
	{
        public static int[] FindBestCheckpointsOrder(Point[] checkpoints)
        {
            var length = checkpoints.Length;
            var bestOrder = MakeTrivialPermutation(length);
            var permutations = new List<int[]>();
            var bestRouteLength = GetRouteLength(checkpoints, bestOrder);
            WritePermutations(new int[length - 1], 0, permutations);
			//count i from 1 because permutations[0] is already in bestOrder 
			//before we start the cycle
            for (int i = 1; i < permutations.Count; i++)
            {
                bestRouteLength = GetRouteLength(checkpoints, bestOrder);
                var alternateRouteDistance = 0.0;
                for (int j = 0; j < length - 1; j++)
                {
                    alternateRouteDistance += 
                        GetDistanceBetweenTwoPoints(checkpoints[permutations[i][j]],
													checkpoints[permutations[i][j + 1]]);
                    if (alternateRouteDistance >= bestRouteLength) break;
                }
                if (alternateRouteDistance < bestRouteLength)
                    bestOrder = permutations[i];
            }
            return bestOrder;
        }

        private static int[] MakeTrivialPermutation(int size)
        {
            var bestOrder = new int[size];
            for (int i = 0; i < bestOrder.Length; i++)
                bestOrder[i] = i;
            return bestOrder;
        }

        public static double GetDistanceBetweenTwoPoints(Point a, Point b)
        {
            return Math.Sqrt((a.X - b.X) * (a.X - b.X) + (a.Y - b.Y) * (a.Y - b.Y));
        }

        public static double GetRouteLength(Point[] checkpoints, int[] route)
        {
            var distance = 0.0;
            for (int i = 0; i < checkpoints.Length - 1; i++)
                distance += GetDistanceBetweenTwoPoints(checkpoints[route[i]], checkpoints[route[i + 1]]);
            return distance;
        }

        public static void AddPermutationToList(int[] permutation, List<int[]> result)
        {
            var array = new int[permutation.Length + 1];
            array[0] = 0;
            for (int i = 0; i < permutation.Length; i++)
                array[i + 1] = permutation[i];
            result.Add(array);
        }

        public static void WritePermutations(int[] permutation, int position, List<int[]> result)
        {
            var length = permutation.Length;
            if (position == permutation.Length)
                AddPermutationToList(permutation, result);
            else
            {
                for (int i = 1; i < permutation.Length + 1; i++)
                {
                    var index = Array.IndexOf(permutation, i, 0, position);
                    if (index == -1)
                    {
                        permutation[position] = i;
                        WritePermutations(permutation, position + 1, result);
                    }
                }
            }
        }
    }
}