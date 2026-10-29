using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using 贪吃蛇.GameObjectNamespace.Entirety;
using 贪吃蛇.Main;

namespace 贪吃蛇.GameObjectNamespace.World.WorldComponents
{
    internal static class BoundaryDetector
    {
        /// <summary>
        /// 侦测是否更换地图，目前只针对蛇
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public static bool isAtCornor(int x, int y, out E_Dir? nextDir)
        {
            // 为什么会比边界多一圈，我的理解是要先move -> 越界 -> 更换地图启动 -> 传送蛇 -> 正确渲染
            // 否则move -> 合法 -> 更换地图启动 -> 传送蛇 -> 永远踩不到边界上
            if (x == -2 || y == -1)
            {
                if (x == -2)
                {
                    nextDir = E_Dir.left;
                }
                else
                {
                    nextDir = E_Dir.up;
                }
                return true;
            }
            // 有点魔法数字，无伤大雅
            if (x == Game.mapWidth || y == Game.mapHeight)
            {
                if (x == Game.mapWidth)
                {
                    nextDir = E_Dir.right;
                }
                else
                {
                    nextDir = E_Dir.down;
                }
                return true;
            }
            nextDir = null;
            return false;
        }
    }
}
