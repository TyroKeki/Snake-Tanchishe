using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using 贪吃蛇.GameObjectSpace.Block;
using 贪吃蛇.Main;
using 贪吃蛇.Toolkit;

namespace 贪吃蛇.GameObjectSpace.Entirety
{
    internal class CurrentMap : IDraw
    {
        private GameObject[,] nowMap;

        // 通过组合，避免在这里new maps()，避免未来可能的多次相同地图生成
        public CurrentMap(Maps maps, E_Map type)
        {
            nowMap = maps.ChangeMap(type);
        }


        /// <summary>
        /// 建议将信息传递到RenderOnScreen，不建议直接渲染，只建议调试
        /// </summary>
        public void Draw()
        {
            for (int col = 0; col < nowMap.GetLength(0); col += 2)
            {
                for (int row = 0; row < nowMap.GetLength(1); row += 1)
                {
                    nowMap[col, row]?.Draw();
                }
            }
        }

        /// <summary>
        /// 传给渲染端
        /// </summary>
        public void ToRenderer()
        {
            for (int col = 0; col < nowMap.GetLength(0); col += 2)
            {
                for (int row = 0; row < nowMap.GetLength(1); row += 1)
                {
                    if (nowMap[col, row] != null)
                    {
                        MapRenderer.Instance.PassInfo(nowMap[col,row],col,row);
                    }
                }
            }
        }
    }
}
