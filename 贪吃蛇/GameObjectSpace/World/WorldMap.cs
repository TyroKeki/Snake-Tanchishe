using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using 贪吃蛇.GameObjectSpace.Entirety;
using 贪吃蛇.Main;
using 贪吃蛇.Toolkit;

namespace 贪吃蛇.GameObjectSpace.World
{
    internal class WorldMap
    {
        private GameObject[,] nowMap;
        /// <summary>
        /// 这是当前所在的具体地图引用
        /// </summary>
        public GameObject[,] NowMap => nowMap;
        public BaseMap[,] chunk = new BaseMap[5,5];
        public int x;
        public int y;

        public WorldMap()
        {
            x = 2;
            y = 2;
            tilePlainMap(chunk);
            nowMap = InitalizeMap(x, y);
        }

        public void tilePlainMap(BaseMap[,] chunk)
        {
            int row = chunk.GetLength(0);
            int col = chunk.GetLength(1);
            for(int i = 0; i < row; i++)
            {
                for(int j = 0; j < col; j++)
                {
                    chunk[i,j] = new PlainMap();
                }
            }
        }

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

        private GameObject[,] ChangeMap(Snake snake,int towardX,int towardY)
        {
            snake.EnterNewMapMove();
            return chunk[towardX, towardY].Map;
        }

        private GameObject[,] InitalizeMap(int towardX, int towardY)
        {
            return chunk[towardX, towardY].Map;
        }

        public void CheckChangeMap(Snake snake)
        {
            if (WorldMap.isAtCornor(snake.NowPos.x, snake.NowPos.y, out E_Dir? toward))
            {
                switch (toward)
                {
                    case E_Dir.up:
                        this.y -= 1;
                        break;
                    case E_Dir.down:
                        this.y += 1;
                        break;
                    case E_Dir.left:
                        this.x -= 1;
                        break;
                    case E_Dir.right:
                        this.x += 1;
                        break;
                    default:
                        throw new InvalidOperationException(
                            $"似乎E_Dir没有赋值");
                }
                ChangeMap(snake, this.x, this.y);
            }
        }
    }
}

