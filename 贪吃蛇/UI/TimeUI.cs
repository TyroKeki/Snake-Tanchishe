using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using 贪吃蛇.GameObjectNamespace;
using 贪吃蛇.Main;

namespace 贪吃蛇.UI
{
    /// <summary>
    /// 只负责UI，逻辑在Game里！
    /// </summary>
    internal class TimeUI : BaseUI<TimeUI>,IDraw
    {
        // 大小指定
        private UIFrame frame = new UIFrame(Game.renderMapWidth + 1, 0, 15, 1);
        // 一个时间寄存器
        TimeSpan time;

        public void FetchTime(TimeSpan time)
        {
            this.time = time;
        }

        public void Draw()
        {
            Console.SetCursorPosition(frame.x, 0);
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("存活时间：");
            Console.Write((int)time.TotalSeconds + "秒");
        }
    }
}
