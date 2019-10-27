using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

    /*Практика «Земля и Диггер»
    Диггер

    Когда-то Digger был одной из самых продвинутых и интересных компьютерных игр. 
    В этом блоке задач мы воссоздадим некоторое её подмножество с помощью ООП.

    Скачайте проект

    Вам предстоит наполнить готовую заготовку игровыми элементами. Каждый элемент должен уметь:

    Возвращать имя файла, в котором лежит соответствующая ему картинка (например, "Terrain.png")
    Сообщать приоритет отрисовки. Чем выше приоритет, 
    тем раньше рисуется соответствующий элемент, это важно для анимации.
    Действовать — возвращать направление перемещения и, 
    если объект во что-то превращается на следующем ходу, то результат превращения.
    Разрешать столкновения двух элементов в одной клетке.
    Terrain
    Сделайте класс Terrain, реализовав ICreature. Сделайте так, чтобы он ничего не делал.

    Player
    Сделайте класс Player, реализовав ICreature.

    Сделайте так, чтобы диггер шагал в разные стороны в зависимости от нажатой клавиши (Game.KeyPressed). 
    Убедитесь, что диггер не покидает пределы игрового поля.

    Сделайте так, чтобы земля исчезала в тех местах, где прошел диггер.

    Запустите проект — игра должна заработать!

    В методе Game.CreateMap вы можете менять карту, на которой будет запускаться игра. 
    Используйте эту возможность для отладки.*/

namespace Digger
{
    public class Player : ICreature
    {
        public CreatureCommand Act(int x, int y)
        {
            var width = Game.MapWidth;
            var height = Game.MapHeight;
            var key = Game.KeyPressed;
            int deltaX = GetOffX(key, x, y);
            int deltaY = GetOffY(key, x, y);
            return new CreatureCommand()
            {
                DeltaX = deltaX,
                DeltaY = deltaY,
                TransformTo = this,
            };
        }

        int GetOffY(Keys key, int x, int y)
        {
            if ((key == Keys.S || key == Keys.Down) && y + 1 < Game.MapHeight && !(Game.Map[x, y + 1] is Sack))
                return 1;
            else if ((key == Keys.W || key == Keys.Up) && y - 1 >= 0 && !(Game.Map[x, y - 1] is Sack))
                return -1;
            else
                return 0;
        }

        int GetOffX(Keys key, int x, int y)
        {
            if ((key == Keys.A || key == Keys.Left) && x - 1 >= 0 && !(Game.Map[x - 1, y] is Sack))
                return -1;
            else if ((key == Keys.D || key == Keys.Right) && x + 1 < Game.MapWidth && !(Game.Map[x + 1, y] is Sack))
                return 1;
            else
                return 0;
        }

        public bool DeadInConflict(ICreature conflictedObject)
        {
            return conflictedObject is Sack || conflictedObject is Monster;
        }

        public int GetDrawingPriority()
        {
            return 0;
        }

        public string GetImageFileName()
        {
            return "Digger.png";
        }
    }

    public class Terrain : ICreature
    {
        public CreatureCommand Act(int x, int y)
        {
            return new CreatureCommand();
        }

        public bool DeadInConflict(ICreature conflictedObject)
        {
            return conflictedObject is Player;
        }

        public int GetDrawingPriority()
        {
            return 4;
        }

        public string GetImageFileName()
        {
            return "Terrain.png";
        }
    }

    public class Sack : ICreature
    {
        public int State = 0;

        CreatureCommand ICreature.Act(int x, int y)
        {
            ICreature transform = this;
            var deltaY = 0;
            if (y + 1 == Game.MapHeight 
                || !(Game.Map[x, y + 1] == null || Game.Map[x, y + 1] is Player || Game.Map[x, y + 1] is Monster))
            {
                if (State >= 2) transform = new Gold();
                else State = 0;
            }
            else if (Game.Map[x, y + 1] == null)
            {
                deltaY++;
                State++;
            }
            else if (State != 0)
            {
                deltaY++;
                State++;
            }
            return GetCommand(0, deltaY, transform);
        }

        CreatureCommand GetCommand(int deltaX, int deltaY, ICreature transformTo)
        {
            return new CreatureCommand()
            {
                DeltaX = deltaX,
                DeltaY = deltaY,
                TransformTo = transformTo,
            };
        }

        public bool DeadInConflict(ICreature conflictedObject)
        {
            return false;
        }

        public int GetDrawingPriority()
        {
            return 3;
        }

        public string GetImageFileName()
        {
            return "Sack.png";
        }
    }

    public class Gold : ICreature
    {
        public CreatureCommand Act(int x, int y)
        {
            return new CreatureCommand()
            {
                DeltaY = 0,
                DeltaX = 0,
                TransformTo = this,

            };
        }

        public bool DeadInConflict(ICreature conflictedObject)
        {
            if (conflictedObject is Player)
                Game.Scores += 10;
            return conflictedObject is Player || conflictedObject is Monster;
        }

        public int GetDrawingPriority()
        {
            return 2;
        }

        public string GetImageFileName()
        {
            return "Gold.png";
        }
    }

    public class Monster : ICreature
    {
        CreatureCommand ICreature.Act(int x, int y)
        {
            var map = Game.Map;
            var digger = FindDigger();
            if (digger == null)
                return GetCommand(0, 0, this);
            var diggerX = digger[0];
            var diggerY = digger[1];
            if (diggerX > x && (map[x + 1, y] == null || map[x + 1, y] is Gold || map[x + 1, y] is Player))
                return GetCommand(1, 0, this);
            else if (diggerX < x && (map[x - 1, y] == null || map[x - 1, y] is Gold || map[x - 1, y] is Player))
                return GetCommand(-1, 0, this);
            else if (diggerY > y && (map[x, y + 1] == null || map[x, y + 1] is Gold || map[x, y + 1] is Player))
                return GetCommand(0, 1, this);
            else if (diggerY < y && (map[x, y - 1] == null || map[x, y - 1] is Gold || map[x, y - 1] is Player))
                return GetCommand(0, -1, this);
            return GetCommand(0, 0, this);
        }
		
        CreatureCommand GetCommand(int deltaX, int deltaY, ICreature transformTo)
        {
            return new CreatureCommand()
            {
                DeltaX = deltaX,
                DeltaY = deltaY,
                TransformTo = transformTo,
            };
        }
		
        int[] FindDigger()
        {
            for (var x = 0; x < Game.MapWidth; x++)
                for (var y = 0; y < Game.MapHeight; y++)
                    if (Game.Map[x, y] is Player)
                        return new int[] { x, y };
            return null;
        }

        bool ICreature.DeadInConflict(ICreature conflictedObject)
        {
            return (conflictedObject is Sack && ((Sack)conflictedObject).State > 0) || conflictedObject is Monster;
        }

        int ICreature.GetDrawingPriority()
        {
            return 1;
        }

        string ICreature.GetImageFileName()
        {
            return "Monster.png";
        }
    }
}