using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using 贪吃蛇.GameObject.Block;
using 贪吃蛇.Main;

namespace 贪吃蛇.GameObject.Entirety
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
    }
}
