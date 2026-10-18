using 贪吃蛇.GameObject.Entirety;
using 贪吃蛇.Main;

namespace 贪吃蛇.GameObject.Block
{
    internal class Food : GameObject
    {
        Snake snake;
        Random rand = new Random();

        public Food(Snake s)
        {
            snake = s;
            RandomPos(snake);
        }

        public override void Draw()
        {
            Console.SetCursorPosition(pos.x, pos.y);
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write("🌽");
        }
        // 随机位置的行为 行为和蛇相关 不好写

        public void RandomPos(Snake snake)
        {
            Position tmpPos;
            do
            {
                int x = rand.Next(1, Game.width / 2 - 1) * 2;
                int y = rand.Next(1, Game.height - 1);
                tmpPos = new Position(x, y);
            } while (snake.CheckPositionOccupied(tmpPos));

            pos = tmpPos;
        }
    }
}
