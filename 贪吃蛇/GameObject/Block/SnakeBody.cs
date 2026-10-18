using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 贪吃蛇.GameObject.Block
{
    enum E_SnakeBody_Type
    {
        head,
        body,
    }

    internal class SnakeBody : GameObject, ISnakeComponent
    {
        E_SnakeBody_Type type;
        public Position Pos { get => pos; set => pos = value; }

        public SnakeBody(E_SnakeBody_Type type,int x,int y)
        {
            this.type = type;
            this.pos = new Position(x,y);
        }


        public override void Draw()
        {
            Console.SetCursorPosition(pos.x, pos.y);
            Console.ForegroundColor = type == E_SnakeBody_Type.head ? ConsoleColor.Yellow : ConsoleColor.Green;
            Console.Write(type == E_SnakeBody_Type.head ? "●" : "●");
        }
    }
}
