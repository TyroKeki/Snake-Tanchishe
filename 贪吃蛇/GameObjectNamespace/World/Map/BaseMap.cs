using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 贪吃蛇.GameObjectNamespace.World.Map
{
    internal abstract class BaseMap
    {
        protected GameObject[,] map;
        public GameObject[,] Map
        {
            get
            {
                if (map == null) CreateMap();
                return map;
            }
        }

        public abstract void CreateMap();
    }
}
