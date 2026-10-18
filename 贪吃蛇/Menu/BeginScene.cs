using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using 贪吃蛇.Main;

namespace 贪吃蛇.Menu
{
    internal class BeginScene : BeginOrEndBaseScene
    {
        public BeginScene()
        {
            titleText = "贪吃蛇";
            selectionOneString = "开始游戏";
            selectionTwoString = "结束游戏";
        }
        public override void PressEnterBehavior()
        {
            if(nowSelIndex == 0)
            {
                Game.ChangeScene(E_SceneType.InGame);
            }
            else
            {
                Environment.Exit(0);
            }
        }
    }
}
