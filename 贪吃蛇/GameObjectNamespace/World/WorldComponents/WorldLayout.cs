using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using 贪吃蛇.GameObjectNamespace.World.Map;

namespace 贪吃蛇.GameObjectNamespace.World.WorldComponents
{
    class WorldLayout
    {
        private BaseMap[,] chunks;
        // 这里封装了一个寂寞，照样索引能改，未来在修改吧
        public BaseMap[,] Chunks => chunks;
        private Vector2 size;
        public Vector2 Size => size;
        public WorldLayout(Vector2 capacity)
        {
            size = capacity;
            chunks = new BaseMap[size.x, size.y];
        }

        /// <summary>
        /// 生成最平凡的地图，用于调试，也可用于占位符防止空引用
        /// </summary>
        public void TilePlainMap()
        {
            int row = chunks.GetLength(0);
            int col = chunks.GetLength(1);
            for (int i = 0; i < row; i++)
            {
                for (int j = 0; j < col; j++)
                {
                    chunks[i, j] = new PlainMap();
                }
            }
        }

        public void ChangeMapTile<T>(int x, int y) where T : BaseMap,new()
        {
            chunks[x, y] = new T();
        }
        
        public void Start()
        {
            TilePlainMap();
            ChangeMapTile<HardMazeMap>(2, 1);
        }

        public void Update()
        {

        }
    }
}
