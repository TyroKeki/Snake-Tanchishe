using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using 贪吃蛇.GameObjectNamespace.Block;
using 贪吃蛇.GameObjectNamespace.Entirety;
using 贪吃蛇.GameObjectNamespace.World.Map;
using 贪吃蛇.Main;
using 贪吃蛇.Toolkit;

namespace 贪吃蛇.GameObjectNamespace.World.WorldComponents
{
    internal class WorldMap
    {
        private readonly WorldLayout layout = new WorldLayout(new Vector2(5,5));
        private readonly WorldState state = new WorldState(2, 2);
        public GameObject[,] NowMap => state.NowMap;
        public int WorldX => state.X;
        public int WorldY => state.Y;

        public WorldMap()
        {
            layout.Start();
            state.SetMap(layout);
        }

        private void EnterNewMap(Snake snake)
        {
            // 这一步由地图来传送蛇的位置
            EnterNewMapMove(snake);
            state.SetMap(layout);
        }

        public void CheckEnterNewMap(Snake snake)
        {
            if (BoundaryDetector.isAtCornor(snake.NowPos.x, snake.NowPos.y, out E_Dir? toward))
            {
                switch (toward)
                {
                    case E_Dir.up:
                        state.SetPosition(state.X, state.Y - 1);
                        break;
                    case E_Dir.down:
                        state.SetPosition(state.X, state.Y + 1);
                        break;
                    case E_Dir.left:
                        state.SetPosition(state.X - 1, state.Y);
                        break;
                    case E_Dir.right:
                        state.SetPosition(state.X + 1, state.Y);
                        break;
                    default:
                        throw new InvalidOperationException(
                            $"似乎E_Dir没有赋值");
                }
                EnterNewMap(snake);
            }
        }

        /// <summary>
        /// 这个是初版的边界传送方法，不确定会不会改
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        private void EnterNewMapMove(Snake snake)
        {
            Vector2 headCopyPos = snake.NowPos;
            if (BoundaryDetector.isAtCornor(headCopyPos.x, headCopyPos.y, out E_Dir? toward))
            {
                switch (toward)
                {
                    case E_Dir.up:
                        headCopyPos.y = Game.LogicMapHeight - 1;
                        break;
                    case E_Dir.down:
                        headCopyPos.y = 0;
                        break;
                    case E_Dir.left:
                        headCopyPos.x = Game.LogicMapWidth - 1;
                        break;
                    case E_Dir.right:
                        headCopyPos.x = 0;
                        break;
                    default:
                        throw new InvalidOperationException(
                            $"似乎E_Dir没有赋值");
                }
                snake.TeleportAllTo(headCopyPos.x, headCopyPos.y);
            }
        }


        /// <summary>
        /// 蛇身碰撞和地图碰撞失败检测耦合，暂不打算修改
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        public bool CheckIsEnd(GameObject[,] nowMap, Snake snake)
        {
            Vector2 deltaPos = snake.NowPos;
            // 越界判断
            if (!Collections.InBounds(deltaPos.x, deltaPos.y, nowMap.GetLength(0), nowMap.GetLength(1)))
            {
                // 可能考虑改为true
                return false;
            }

            if (nowMap[deltaPos.x, deltaPos.y] is Wall)
            {
                return true;
            }

            for (int i = 1; i < snake.Bodies.Count; i++)
            {
                if (snake.Bodies[i].Pos == deltaPos)
                {
                    return true;
                }
            }

            return false;
        }
    }
}

