using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using 贪吃蛇.GameObjectSpace.Block;
using 贪吃蛇.Toolkit;

namespace 贪吃蛇.GameObjectSpace.Entirety
{
    enum E_MoveDir
    {
        up, down, left, right
    }
    internal class Snake : IDraw
    {
        List<SnakeComponent> bodies;
        E_MoveDir dir;

        public Snake(int x, int y)
        {
            bodies = new List<SnakeComponent>(32);
            SnakeBody head = new SnakeBody(E_SnakeBody_Type.head, x, y);
            bodies.Add(head);
            EmptySnakeTail cleaner = new EmptySnakeTail(x + 2, y);
            bodies.Add(cleaner);

            GetBodiesInARow(head, 5);
            dir = E_MoveDir.left;
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
                RenderOnScreen.Instance.PassInfo(bodies[i], bodies[i].Pos.x, bodies[i].Pos.y);
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
                case E_MoveDir.up:
                    head.y--;
                    break;
                case E_MoveDir.down:
                    head.y++;
                    break;
                case E_MoveDir.left:
                    head.x -= 2;
                    break;
                case E_MoveDir.right:
                    head.x += 2;
                    break;
                default:
                    break;
            }
            bodies[0].Pos = head;
        }
        private bool CanChangeDir(E_MoveDir newDir)
        {
            if (newDir == this.dir)
            {
                return false;
            }
            else if (bodies.Count > 1 &&
                (this.dir == E_MoveDir.left && newDir == E_MoveDir.right ||
                 this.dir == E_MoveDir.right && newDir == E_MoveDir.left ||
                 this.dir == E_MoveDir.up && newDir == E_MoveDir.down ||
                 this.dir == E_MoveDir.down && newDir == E_MoveDir.up))
            {
                return false;
            }
            return true;
        }

        public void ChangeDir(E_MoveDir newDir)
        {
            if (CanChangeDir(newDir))
            {
                dir = newDir;
            }
        }

        public bool CheckIsEnd()
        {
            Position deltaPos = bodies[0].Pos;
            if (Map.grids[deltaPos.x, deltaPos.y] is Wall)
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

    }
}
