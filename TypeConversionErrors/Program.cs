using System;

//    Ошибки преобразования типов
//    Исправьте все ошибки преобразования типов. Читайте и переводите с английского имена переменных,
// чтобы понять, что от вас требуется.

namespace TypeConversionErrors
{
    class Program
    {
        public static void Main()
        {
            double pi = Math.PI;
            int tenThousand = (int)10000L;
            float tenThousandPi = (float)pi * tenThousand;
            int roundedTenThousandPi = (int)Math.Round(tenThousandPi);
            int integerPartOfTenThousandPi = (int)tenThousandPi;
            Console.WriteLine(integerPartOfTenThousandPi);
            Console.WriteLine(roundedTenThousandPi);
        }
    }
}