using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using 贪吃蛇.Main;

namespace 贪吃蛇.Menu
{
    abstract class BeginOrEndBaseScene : ISceneUpdate
    {
        protected int nowSelIndex = 0;
        protected string titleText;
        protected string selectionOneString;
        protected string selectionTwoString;

        public abstract void PressEnterBehavior();

        public void Update()
        {
            // 开始和结束场景的游戏逻辑
            Console.ForegroundColor = ConsoleColor.White;
            // 显示标题
            Console.SetCursorPosition(Game.width / 2 - titleText.Length, 5);
            Console.Write(titleText);
            // 显示下方的选项
            Console.SetCursorPosition(Game.width / 2 - selectionOneString.Length, 10);
            Console.ForegroundColor = nowSelIndex == 0 ? ConsoleColor.Red : ConsoleColor.White;
            Console.Write(selectionOneString);
            Console.SetCursorPosition(Game.width / 2 - selectionTwoString.Length, 13);
            Console.ForegroundColor = nowSelIndex == 1 ? ConsoleColor.Red : ConsoleColor.White;
            Console.Write(selectionTwoString);
            // 检测输入
            switch (Console.ReadKey(true).Key)
            {
                case ConsoleKey.W:
                    nowSelIndex--;
                    if(nowSelIndex < 0)
                    {
                        nowSelIndex = 0;
                    }
                    break;
                case ConsoleKey.S:
                    nowSelIndex++;
                    if (nowSelIndex > 1)
                    {
                        nowSelIndex = 1;
                    }
                    break;
                case ConsoleKey.Enter:
                    PressEnterBehavior();
                    break;
            }
        }
    }
}
