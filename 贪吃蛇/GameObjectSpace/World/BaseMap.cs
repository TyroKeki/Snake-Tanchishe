using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 贪吃蛇.GameObjectSpace.World
{
    internal abstract class BaseMap
    {
        public Position worldPos = new Position();
        protected GameObject[,] map;
        public GameObject[,] Map => map;

        public abstract GameObject[,] CreateMap();
    }
}
