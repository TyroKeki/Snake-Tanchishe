using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using 贪吃蛇.GameObjectSpace.Block;
using 贪吃蛇.Main;
using 贪吃蛇.Menu;

namespace 贪吃蛇.GameObjectSpace.Entirety
{
    enum E_Map
    {
        startMap,

    }
    internal class Maps
    {
        private GameObject[,] startMap;

        public Maps()
        {
            startMap = Vanilla();
        }

        /// <summary>
        /// 这是返回的原版地图
        /// </summary>
        /// <returns>固定大小，数据类型为GameObject二维矩阵</returns>
        private GameObject[,] Vanilla()
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

        public GameObject[,] ChangeMap(E_Map mapType)
        {
            switch (mapType)
            {
                case E_Map.startMap:
                    return startMap;
                default:
                    throw new InvalidOperationException("Maps.ChangeMap Line 57出错了");
            }
        }
    }
}
