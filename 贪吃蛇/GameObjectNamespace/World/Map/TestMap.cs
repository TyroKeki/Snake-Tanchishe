using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using 贪吃蛇.GameObjectNamespace.Block;
using 贪吃蛇.Main;
using 贪吃蛇.Toolkit;

namespace 贪吃蛇.GameObjectNamespace.World.Map
{
    internal class TestMap : BaseMap
    {

        public override void CreateMap()
        {
            GameObject[,] grids;
            // col仅偶数位合法
            grids = new GameObject[Game.LogicMapWidth, Game.LogicMapHeight];
            for (int col = 0; col < grids.GetLength(0); col += 1)
            {
                grids[col, 0] = new Wall(col, 0);
                grids[col, Game.LogicMapHeight - 1] = new Wall(col, Game.LogicMapHeight - 1);
            }

            for (int row = 0; row < grids.GetLength(1); row += 1)
            {
                grids[0, row] = new Wall(0, row);
                grids[Game.LogicMapWidth - 1, row] = new Wall(Game.LogicMapWidth - 1, row);
            }
            map = grids;
        }
    }
}
