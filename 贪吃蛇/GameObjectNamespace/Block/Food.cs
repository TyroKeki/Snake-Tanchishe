using 贪吃蛇.GameObjectNamespace.Entirety;
using 贪吃蛇.Main;
using 贪吃蛇.Toolkit;

namespace 贪吃蛇.GameObjectNamespace.Block
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

        /// <summary>
        /// 注意：这是一个 block
        /// 建议传入 MapRenderer
        /// </summary>
        public override void Draw()
        {
            Console.SetCursorPosition(pos.x, pos.y);
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write("🌽");
        }

        public void ToRenderer()
        {
            MapRenderer.Instance.PassInfo(this, pos.x, pos.y);
        }
        // 随机位置的行为 行为和蛇相关 不好写

        public void RandomPos(Snake snake)
        {
            Vector2 tmpPos;
            do
            {
                int x = rand.Next(1, Game.mapWidth / 2 - 1) * 2;
                int y = rand.Next(1, Game.mapHeight - 1);
                tmpPos = new Vector2(x, y);
            } while (snake.CheckPositionOccupied(tmpPos));

            pos = tmpPos;
        }
    }
}
