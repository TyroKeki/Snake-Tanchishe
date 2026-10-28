using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using 贪吃蛇.GameObjectSpace.Block;
using 贪吃蛇.Main;

namespace 贪吃蛇.GameObjectSpace.World
{
    internal class PlainMap : BaseMap
    {
        //注意地图其实是有坐标属性的，但是我没想好怎么写，先存在世界地图的二维坐标里
        public PlainMap()
        {
            map = CreateMap();
        }

        public override GameObject[,] CreateMap()
        {
            GameObject[,] grids;
            // col仅偶数位合法
            grids = new GameObject[Game.mapWidth, Game.mapHeight];
            for (int col = 0; col < grids.GetLength(0); col += 2)
            {
                if (34 <= col && col <= 46)
                {
                    continue;
                }
                grids[col, 0] = new Wall(col, 0);
                grids[col, Game.mapHeight - 1] = new Wall(col, Game.mapHeight - 1);
            }

            for (int row = 0; row < grids.GetLength(1); row += 1)
            {
                if (7 <= row && row <= 13)
                {
                    continue;
                }
                grids[0, row] = new Wall(0, row);
                grids[Game.mapWidth - 2, row] = new Wall(Game.mapWidth - 2, row);
            }
            return grids;
        }
    }
}
