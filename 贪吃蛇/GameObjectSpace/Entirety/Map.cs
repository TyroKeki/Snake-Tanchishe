using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using 贪吃蛇.GameObjectSpace.Block;
using 贪吃蛇.Main;
using 贪吃蛇.Toolkit;

namespace 贪吃蛇.GameObjectSpace.Entirety
{
    internal class Map : IDraw
    {
        public static GameObject[,] grids;

        public Map()
        {
            // col仅偶数位合法
            grids = new GameObject[Game.width,Game.height];
            for(int col = 0; col < grids.GetLength(0); col+=2)
            {
                grids[col, 0] = new Wall(col,0);
                grids[col, Game.height - 1] = new Wall(col, Game.height - 1);
            }

            for (int row = 0; row < grids.GetLength(1); row += 1)
            {
                grids[0, row] = new Wall(0, row);
                grids[Game.width - 2, row] = new Wall(Game.width - 2, row);
            }
        }


        /// <summary>
        /// 建议将信息传递到RenderOnScreen，不建议直接渲染
        /// </summary>
        public void Draw()
        {
            for (int col = 0; col < grids.GetLength(0); col += 2)
            {
                for (int row = 0; row < grids.GetLength(1); row += 1)
                {
                    grids[col, row]?.Draw();
                }
            }
        }

        /// <summary>
        /// 传给渲染端
        /// </summary>
        public void ToRenderer()
        {
            for (int col = 0; col < grids.GetLength(0); col += 2)
            {
                for (int row = 0; row < grids.GetLength(1); row += 1)
                {
                    if (grids[col, row] != null)
                    {
                        RenderOnScreen.Instance.PassInfo(grids[col,row],col,row);
                    }
                }
            }
        }
    }
}
