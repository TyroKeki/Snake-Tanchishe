using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 贪吃蛇.GameObjectSpace.Block
{
    internal abstract class SnakeComponent : GameObject
    {
        public Position Pos { get => pos; set => pos = value; }

    }
}
