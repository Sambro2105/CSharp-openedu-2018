using System;

    /*Практика «Бильярд»
    Скачайте архив с проектом Billard.

    Реализуйте метод для расчета угла отскока шарика от стены. 
    Считайте, что угол падения равен углу отражения, то есть можно пренебречь всеми физическими эффектами, 
    связанными с кручением шаров, трением шара об стенку и т.п.

    Проверить корректность вашей реализации можно запустив проект.

    Вы можете изучить устройство проекта — это будет полезно, но для выполнения этого задания это совсем не обязательно. 
    Более того, будьте готовы к тому, что в проекте активно используются ещё не пройденные темы.*/

namespace Billiards
{
    public static class BilliardsTask
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="directionRadians">Угол направелния движения шара</param>
        /// <param name="wallInclinationRadians">Угол</param>
        /// <returns></returns>
        public static double BounceWall(double directionRadians, double wallInclinationRadians)
        {
            return 2*wallInclinationRadians-directionRadians;
        }
    }
}