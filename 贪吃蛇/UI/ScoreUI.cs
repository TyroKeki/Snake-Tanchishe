using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using 贪吃蛇.GameObjectNamespace;
using 贪吃蛇.Main;

namespace 贪吃蛇.UI
{
    internal class ScoreUI : IDraw
    {
        private static ScoreUI instance = new ScoreUI();
        public static ScoreUI Instance => instance;
        private ScoreUI() { }

        // 指定范围
        private UIFrame frame = new UIFrame(Game.renderMapWidth + 1, 1, 14, 2);
        // 寄存坐标
        int score;

        public void FetchScore(int score)
        {
            this.score = score;
        }

        public void Draw()
        {
            Console.SetCursorPosition(frame.x, frame.y);
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write($"得分为:{score}");
        }
    }
}
