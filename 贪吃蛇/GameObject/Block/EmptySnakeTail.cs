using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 贪吃蛇.GameObject.Block
{
    internal class EmptySnakeTail : GameObject, ISnakeComponent
    {
        public Position Pos { get => pos; set => pos = value; }

        public EmptySnakeTail(int x,int y)
        {
            pos = new Position(x,y);
        }

        public override void Draw()
        {
            Console.SetCursorPosition(pos.x, pos.y);
            Console.Write("  ");
        }
    }
}
