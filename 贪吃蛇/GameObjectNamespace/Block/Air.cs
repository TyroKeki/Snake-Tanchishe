using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 贪吃蛇.GameObjectNamespace.Block
{
    internal class Air : GameObject
    {
        public Air(int x,int y)
        {
            pos = new Vector2(x, y);
        }
        public override void Draw()
        {
            Console.SetCursorPosition(pos.x * 2, pos.y);
            Console.Write("  ");
        }
    }
}
