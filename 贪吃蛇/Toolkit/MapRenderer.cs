using 贪吃蛇.GameObjectSpace;
using 贪吃蛇.Main;

namespace 贪吃蛇.Toolkit
{
    internal class MapRenderer : IDraw
    {
        private static MapRenderer instance = new MapRenderer();
        public static MapRenderer Instance => instance;
        private MapRenderer() { }

        private GameObject[,] screenPos;
        int width;
        int height;
        /// <summary>
        /// 通常是80 * 20大小
        /// </summary>
        public void ResetResolution()
        {
            width = Game.mapWidth;
            height = Game.mapHeight;
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
            if (x < 0 || x >= width || y < 0 || y >= height)
                throw new ArgumentOutOfRangeException(
                    $"PassInfo 越界: ({x},{y})，地图尺寸 {width}×{height}");
            screenPos[x, y] = obj;
        }
    }
}
