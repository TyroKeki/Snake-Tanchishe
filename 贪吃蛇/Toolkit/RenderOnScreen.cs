using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using 贪吃蛇.GameObjectSpace;
using 贪吃蛇.Main;

namespace 贪吃蛇.Toolkit
{
    internal class RenderOnScreen : IDraw
    {
        private static RenderOnScreen instance = new RenderOnScreen();
        public static RenderOnScreen Instance => instance;
        private RenderOnScreen() { }

        public GameObject[,] screenPos;
        int width;
        int height;
        public void ResetResolution()
        {
            width = Game.width;
            height = Game.height;
            screenPos = new GameObject[width, height];
        }

        public void Draw()
        {
            for (int col = 0; col < screenPos.GetLength(0); col += 2)
            {
                for (int row = 0; row < screenPos.GetLength(1); row += 1)
                {
                    screenPos[col, row]?.Draw();
                }
            }
        }

        public void PassInfo(GameObject obj,int x, int y)
        {
            screenPos[x, y] = obj;
        }
    }
}
