using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using 贪吃蛇.Main;

namespace 贪吃蛇.Menu
{
    internal class EndScene : BeginOrEndBaseScene
    {
        public EndScene()
        {
            titleText = "贪吃蛇";
            selectionOneString = "回到开始界面";
            selectionTwoString = "结束游戏";
        }
        public override void PressEnterBehavior()
        {
            if (nowSelIndex == 0)
            {
                Game.ChangeScene(E_SceneType.Start);
            }
            else
            {
                Environment.Exit(0);
            }
        }
    }
}
