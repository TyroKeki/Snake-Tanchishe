using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 贪吃蛇.GameObjectNamespace
{

    abstract class GameObject : IDraw
    {
        public Vector2 pos;

        public abstract void Draw();
    }
}
