using System;

    /*Практика «Два прямоугольника»
    Скачайте проект Rectangles

    Вам даны два прямоугольника на плоскости, со сторонами параллельными осям координат с целочисленными координатами.

    Реализуйте в классе RectanglesTask.cs три метода для работы с прямоугольниками:

    определение, есть ли у двух прямоугольников хотя бы одна общая точка 
    (и граница и внутренность считаются частью прямоугольника);
    вычисление площади пересечения;
    определение, вложен ли один в другой.
    Решите задание без использования библиотечных методов, кроме Min и Max.

    Обратите внимание, что ваше решение должно корректно работать с вырожденными прямоугольниками: 
    у которых длина или ширина равны 0.

    Для проверки своего решения запустите скачанный проект.

    В мире компьютерной графики принято, что верхний левый угол экрана имеет координаты (0, 0), 
    а ось Y направлена вниз, а не вверх, как принято в математике. 
    Поэтому в этой задаче нижний край прямоугольника имеет большую координату, чем верхний. 
    Учитывайте это при решении задачи!*/

namespace Rectangles
{
	public static class RectanglesTask
	{
        public static bool Equals(int a, int b, int c, int d)
        {
            return a == b || a == c || a == d;
        }

        public static int GetA(Rectangle r)
        {
            return r.Left;
        }

        public static int GetB(Rectangle r)
        {
            return r.Top;
        }

        public static int GetC(Rectangle r)
        {
            return r.Left + r.Width;
        }

        public static int GetD(Rectangle r)
        {
            return r.Top + r.Height;
        }

        public static bool IsQuadrant0(int x1, int y1, int x2, int y2)
        {
            return x2 >= x1 && y2 >= y1;
        }

        public static bool IsQuadrant2(int x1, int y1, int x2, int y2)
        {
            return x2 <= x1 && y2 <= y1;
        }

        // Пересекаются ли два прямоугольника (пересечение только по границе также считается пересечением)
        public static bool AreIntersected(Rectangle r1, Rectangle r2)
		{
            // так можно обратиться к координатам левого верхнего угла первого прямоугольника: r1.Left, r1.Top
            bool a = IsQuadrant0(GetA(r1), GetB(r1), GetC(r2), GetD(r2));
            bool b = IsQuadrant2(GetC(r1), GetD(r1), GetA(r2), GetB(r2));
            return a && b;
		}

		// Площадь пересечения прямоугольников
        public static int FindIntersectionWidth(Rectangle r1, Rectangle r2)
        {
            if (AreIntersected(r1, r2))
            {
                if (GetA(r2) < GetA(r1) && GetC(r2) > GetC(r1))
                    return r1.Width;
                else if (GetA(r2) > GetA(r1) && GetC(r2) < GetC(r1))
                    return r2.Width;
                else if (GetA(r2) <= GetA(r1) && GetC(r2) <= GetC(r1))
                    return GetC(r2) - GetA(r1);
                else
                    return GetC(r1) - GetA(r2);
            }
            else
                return 0;
        }

        public static int FindIntersectionHeight(Rectangle r1, Rectangle r2)
        {
            if (AreIntersected(r1, r2))
            {
                if (GetB(r2) < GetB(r1) && GetD(r2) > GetD(r1))
                    return r1.Height;
                else if (GetB(r2) > GetB(r1) && GetD(r2) < GetD(r1))
                    return r2.Height;
                else if (GetB(r2) <= GetB(r1) && GetD(r2) <= GetD(r1))
                    return GetD(r2) - GetB(r1);
                else
                    return GetD(r1) - GetB(r2);
            }
            else
                return 0;
        }

        public static int IntersectionSquare(Rectangle r1, Rectangle r2)
		{
                return FindIntersectionHeight(r1, r2)*FindIntersectionWidth(r1, r2);
		}

		// Если один из прямоугольников целиком находится внутри другого — вернуть номер (с нуля) внутреннего.
		// Иначе вернуть -1
		// Если прямоугольники совпадают, можно вернуть номер любого из них.
        public static int IndexOfInnerRectangle(Rectangle r1, Rectangle r2)
		{
            bool a = IsQuadrant0(GetA(r2), GetB(r2), GetA(r1), GetB(r1));
            bool b = IsQuadrant2(GetC(r2), GetD(r2), GetC(r1), GetD(r1));
            bool c = IsQuadrant0(GetA(r1), GetB(r1), GetA(r2), GetB(r2));
            bool d = IsQuadrant2(GetC(r1), GetD(r1), GetC(r2), GetD(r2));
            if (a && b)
                return 0;
            else if (c && d)
                return 1;
            else
                return -1;
		}
	}
}