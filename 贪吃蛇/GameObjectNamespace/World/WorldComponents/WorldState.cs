using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using 贪吃蛇.GameObjectNamespace.Entirety;
using 贪吃蛇.GameObjectNamespace.World.Map;
using 贪吃蛇.Main;

namespace 贪吃蛇.GameObjectNamespace.World.WorldComponents
{
    internal class WorldState
    {
        private GameObject[,] nowMap;
        public GameObject[,] NowMap => nowMap;
        Vector2 pos;
        public int X => pos.x;
        public int Y => pos.y;
        public Vector2 Pos => pos;

        public WorldState(int x, int y)
        {
            pos = new Vector2(x, y);
        }

        public void SetPosition(int x, int y)
        {
            pos = new Vector2(x, y);
        }

        public void SetMap(WorldLayout layout)
        {
            // 这一步开始加载地图，否则是空的（已修改，没有上述问题）
            nowMap = layout.Chunks[pos.x, pos.y].Map;
        }
    }
}
