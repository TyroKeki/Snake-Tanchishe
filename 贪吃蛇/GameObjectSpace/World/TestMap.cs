using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using 贪吃蛇.GameObjectSpace.Block;
using 贪吃蛇.Main;

namespace 贪吃蛇.GameObjectSpace.World
{
    internal class TestMap : BaseMap
    {
        public TestMap(int worldX,int worldY)
        {
            worldPos.x = worldX;
            worldPos.y = worldY;
            map = CreateMap();
        }

        public override GameObject[,] CreateMap()
        {
            GameObject[,] grids;
            // col仅偶数位合法
            grids = new GameObject[Game.mapWidth, Game.mapHeight];
            for (int col = 0; col < grids.GetLength(0); col += 2)
            {
                grids[col, 0] = new Wall(col, 0);
                grids[col, Game.mapHeight - 1] = new Wall(col, Game.mapHeight - 1);
            }

            for (int row = 0; row < grids.GetLength(1); row += 1)
            {
                grids[0, row] = new Wall(0, row);
                grids[Game.mapWidth - 2, row] = new Wall(Game.mapWidth - 2, row);
            }
            return grids;
        }
    }
}
