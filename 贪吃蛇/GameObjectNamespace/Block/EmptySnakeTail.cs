using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 贪吃蛇.GameObjectNamespace.Block
{
    internal class EmptySnakeTail : SnakeComponent
    {

        public EmptySnakeTail(int x,int y)
        {
            pos = new Vector2(x,y);
        }

        public override void Draw()
        {
            Console.SetCursorPosition(pos.x, pos.y);
            Console.Write("  ");
        }
    }
}
