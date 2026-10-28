using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using 贪吃蛇.GameObjectSpace.Block;
using 贪吃蛇.GameObjectSpace.World;
using 贪吃蛇.Main;
using 贪吃蛇.Toolkit;

namespace 贪吃蛇.GameObjectSpace.Entirety
{

    internal class Snake : IDraw
    {
        List<SnakeComponent> bodies;
        E_Dir dir;
        public Position NowPos => bodies[0].Pos;


        public Snake(int x, int y)
        {
            bodies = new List<SnakeComponent>(32);
            SnakeBody head = new SnakeBody(E_SnakeBody_Type.head, x, y);
            bodies.Add(head);
            EmptySnakeTail cleaner = new EmptySnakeTail(x + 2, y);
            bodies.Add(cleaner);

            GetBodiesInARow(head, 5);
            dir = E_Dir.left;
        }

        public void GetBody(int x, int y)
        {
            SnakeBody body = new SnakeBody(E_SnakeBody_Type.body, x, y);
            bodies.Insert(bodies.Count - 1, body);
        }

        public void GetBodiesInARow(SnakeBody head, int n)
        {
            Position pos = head.pos;
            for (int i = 1; i <= n; i++)
            {
                GetBody(pos.x + i * 2, pos.y);
            }
        }

        /// <summary>
        /// 建议传入RenderOnScreen
        /// </summary>
        public void Draw()
        {
            for (int i = bodies.Count-1; i>=0; i--)
            {
                bodies[i].Draw();
            }
        }

        public void ToRenderer()
        {
            for (int i = bodies.Count - 1; i >= 0; i--)
            {
                MapRenderer.Instance.PassInfo(bodies[i], bodies[i].Pos.x, bodies[i].Pos.y);
            }
        }

        public void Move()
        {
            for(int i = bodies.Count - 1; i > 0; i--)
            {
                bodies[i].Pos = bodies[i - 1].Pos;
            }

            Position head = bodies[0].Pos;
            switch (dir)
            {
                case E_Dir.up:
                    head.y--;
                    break;
                case E_Dir.down:
                    head.y++;
                    break;
                case E_Dir.left:
                    head.x -= 2;
                    break;
                case E_Dir.right:
                    head.x += 2;
                    break;
                default:
                    break;
            }
            bodies[0].Pos = head;
        }

        // 仅用于内部方法ChangeDir的合法性判断
        private bool CanChangeDir(E_Dir newDir)
        {
            if (newDir == this.dir)
            {
                return false;
            }
            else if (bodies.Count > 1 &&
                (this.dir == E_Dir.left && newDir == E_Dir.right ||
                 this.dir == E_Dir.right && newDir == E_Dir.left ||
                 this.dir == E_Dir.up && newDir == E_Dir.down ||
                 this.dir == E_Dir.down && newDir == E_Dir.up))
            {
                return false;
            }
            return true;
        }

        public void ChangeDir(E_Dir newDir)
        {
            if (CanChangeDir(newDir))
            {
                dir = newDir;
            }
        }

        /// <summary>
        /// 蛇身碰撞和地图碰撞失败检测耦合，暂不打算修改
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        public bool CheckIsEnd(GameObject[,] nowMap)
        {
            Position deltaPos = bodies[0].Pos;
            //if (!Collections.InBounds(deltaPos.x, deltaPos.y, nowMap.GetLength(0), nowMap.GetLength(1)))
            //{
            //    return false;
            //}

            {

            }
            if (nowMap[deltaPos.x, deltaPos.y] is Wall)
            {
                return true;
            }

            for (int i = 1; i < bodies.Count; i++)
            {
                if (bodies[i].Pos == deltaPos)
                {
                    return true;
                }
            }

            return false;
        }

        public bool CheckPositionOccupied(Position p)
        {
            for(int i = 0;i< bodies.Count; i++)
            {
                if(bodies[i].Pos == p)
                {
                    return true;
                }
            }
            return false;
        }

        public void CheckEatFood(Food food)
        {
            if (bodies[0].Pos == food.pos)
            {
                food.RandomPos(this);
                GetBody(bodies[bodies.Count - 1].Pos.x, bodies[bodies.Count - 1].Pos.y);
            }
        }

        /// <summary>
        /// 传送方法，未来可以是扩展方法
        /// </summary>
        /// <param name="snakeObj">指定修改的蛇部件</param>
        /// <param name="x">指定位置x</param>
        /// <param name="y">指定位置y</param>
        public static void TeleportTo(SnakeComponent snakeObj,int x,int y)
        {
            Position snakeObjPos = snakeObj.pos;
            snakeObjPos.x = x;
            snakeObjPos.y = y;
            snakeObj.pos = snakeObjPos;
        }

        /// <summary>
        /// 这个是初版的边界传送方法，不确定会不会改
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public void EnterNewMapMove()
        {
            SnakeComponent head = bodies[0];
            Position headCopyPos = head.pos;
            if(WorldMap.isAtCornor(headCopyPos.x, headCopyPos.y, out E_Dir? toward)){
                switch (toward)
                {
                    case E_Dir.up:
                        headCopyPos.y = Game.mapHeight - 1;
                        break;
                    case E_Dir.down:
                        headCopyPos.y = 0;
                        break;
                    case E_Dir.left:
                        headCopyPos.x = Game.mapWidth - 2;
                        break;
                    case E_Dir.right:
                        headCopyPos.x = 0;
                        break;
                    default:
                        throw new InvalidOperationException(
                            $"似乎E_Dir没有赋值");
                }
                TeleportTo(head, headCopyPos.x, headCopyPos.y);
            }
        }
    }
}
