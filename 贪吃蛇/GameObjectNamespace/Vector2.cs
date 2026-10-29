using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 贪吃蛇.GameObjectNamespace
{
    internal struct Vector2
    {
        public int x;
        public int y;
        public Vector2(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        public static bool operator ==(Vector2 left, Vector2 right)
        {
            if(left.x == right.x && left.y == right.y)
            {
                return true;
            }
            return false;
        }

        public static bool operator !=(Vector2 left, Vector2 right)
        {
            if (left.x == right.x && left.y == right.y)
            {
                return false;
            }
            return true;
        }
    }
}
