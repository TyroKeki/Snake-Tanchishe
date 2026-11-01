using 贪吃蛇.GameObjectNamespace.Entirety;
using 贪吃蛇.GameObjectNamespace.World.WorldComponents;
using 贪吃蛇.Main;
using 贪吃蛇.Menu;
using 贪吃蛇.Toolkit;

namespace 贪吃蛇.GameObjectNamespace.Block
{
    internal class Food : GameObject
    {
        Random rand = new Random();


        public Food(int x, int y)
        {
            pos.x = x; pos.y = y;
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

    }
}
