using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 贪吃蛇.GameObject.Block
{
    internal interface ISnakeComponent : IDraw
    {
        Position Pos { get; set; }
        public abstract void Draw();
    }
}
