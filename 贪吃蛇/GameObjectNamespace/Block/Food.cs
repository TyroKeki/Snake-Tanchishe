using 贪吃蛇.GameObjectNamespace.Entirety;
using 贪吃蛇.GameObjectNamespace.World.WorldComponents;
using 贪吃蛇.Main;
using 贪吃蛇.Menu;
using 贪吃蛇.Toolkit;

namespace 贪吃蛇.GameObjectNamespace.Block
{
    internal class Food : GameObject
    {
        Snake snake;
        Random rand = new Random();
        WorldMap worldMap = GameScene.WorldMap;
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
            Console.SetCursorPosition(pos.x * 2, pos.y);
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
            while (true)
            {
                int x = rand.Next(1, Game.LogicMapWidth - 1);
                int y = rand.Next(1, Game.LogicMapHeight - 1);
                tmpPos = new Vector2(x, y);
                if (snake.CheckPositionOccupied(tmpPos))
                {
                    continue;
                }
                GameObject nowBlock = worldMap.NowMap[x, y];
                if(nowBlock is Wall)
                {
                    continue;
                }
                break;
            }

            pos = tmpPos;
        }

        private void SnakeDetect()
        {

        }
    }
}
