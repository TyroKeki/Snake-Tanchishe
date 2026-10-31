using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using 贪吃蛇.GameObjectNamespace;
using 贪吃蛇.Main;

namespace 贪吃蛇.UI
{
    internal class WorldPosUI : IDraw
    {
        private static WorldPosUI instance = new WorldPosUI();
        public static WorldPosUI Instance => instance;
        private WorldPosUI() { }
        // 指定范围
        private UIFrame frame = new UIFrame(Game.renderMapWidth + 1, 2, 14, 2);
        // 寄存坐标
        int x, y;
        public void FetchWorldPos(int x,int y)
        {
            this.x = x;
            this.y = y;
        }
        public void Draw()
        {
            Console.SetCursorPosition(frame.x, frame.y);
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write($"世界坐标X:{x}");
            Console.SetCursorPosition(frame.x, frame.y + 1);
            Console.Write($"世界坐标Y:{y}");
        }
    }
}
