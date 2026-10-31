using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using 贪吃蛇.Toolkit;

namespace 贪吃蛇.GameObjectNamespace.World
{
    internal static class MapToRenderer
    {

        /// <summary>
        /// 传给渲染端
        /// </summary>
        public static void ToRenderer(GameObject[,] nowMap)
        {
            for (int col = 0; col < nowMap.GetLength(0); col += 1)
            {
                for (int row = 0; row < nowMap.GetLength(1); row += 1)
                {
                    if (nowMap[col, row] != null)
                    {
                        MapRenderer.Instance.PassInfo(nowMap[col, row], col, row);
                    }
                }
            }
        }
    }
}
