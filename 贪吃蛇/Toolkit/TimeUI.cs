using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using 贪吃蛇.GameObjectSpace;
using 贪吃蛇.Main;

namespace 贪吃蛇.Toolkit
{
    /// <summary>
    /// 只负责UI，逻辑在Game里！
    /// </summary>
    internal class TimeUI : IDraw
    {
        private static TimeUI instance = new TimeUI();
        public static TimeUI Instance => instance;
        private TimeUI() { }
        TimeSpan time;

        public void FetchTime(TimeSpan time)
        {
            this.time = time;
        }

        public void Draw()
        {
            int startX = Game.mapWidth;
            Console.SetCursorPosition(startX + 1, 0);
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("存活时间：");
            Console.Write((int)time.TotalSeconds + "秒");
        }
    }
}
