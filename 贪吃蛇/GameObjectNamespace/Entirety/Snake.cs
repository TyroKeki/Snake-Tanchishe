using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using 贪吃蛇.Data;
using 贪吃蛇.GameObjectNamespace.Block;
using 贪吃蛇.GameObjectNamespace.World;
using 贪吃蛇.Main;
using 贪吃蛇.Toolkit;

namespace 贪吃蛇.GameObjectNamespace.Entirety
{

    internal class Snake : IDraw
    {
        List<SnakeComponent> bodies;
        E_Dir dir;
        public Vector2 NowPos => bodies[0].Pos;
        public IReadOnlyList<SnakeComponent> Bodies => bodies;

        public Snake(int x, int y)
        {
            bodies = new List<SnakeComponent>(32);
            SnakeBody head = new SnakeBody(E_SnakeBody_Type.head, x, y);
            bodies.Add(head);

            GetBodiesInARow(head, 5);
            dir = E_Dir.left;
        }

        private void GetBody(int x, int y)
        {
            SnakeBody body = new SnakeBody(E_SnakeBody_Type.body, x, y);
            bodies.Insert(bodies.Count, body);
        }

        public void GetBodiesInARow(SnakeBody head, int n)
        {
            Vector2 pos = head.pos;
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

            Vector2 head = bodies[0].Pos;
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
        // 因为约束来自自己，所以放在自己的类里合理
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

        public bool CheckPositionOccupied(Vector2 p)
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

        public void CheckEatFood(Food food, PlayerData playerData)
        {
            if (bodies[0].Pos == food.pos)
            {
                food.RandomPos(this);
                GetBody(bodies[bodies.Count - 1].Pos.x, bodies[bodies.Count - 1].Pos.y);
                playerData.AddScore(1);
            }
        }


        public void TeleportHeadTo(int x,int y)
        {
            SnakeComponent head = bodies[0];
            head.pos = new Vector2(x,y);
        }

        public void TeleportAllTo(int x,int y)
        {
            foreach (SnakeComponent body in bodies)
            {
                body.pos = new Vector2(x, y);
            }
        }

    }
}
