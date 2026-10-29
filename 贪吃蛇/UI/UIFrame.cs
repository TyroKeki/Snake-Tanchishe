using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 贪吃蛇.UI
{
    internal class UIFrame
    {
        public int x;
        public int y;
        public int width;
        public int height;

        public UIFrame(int x,int y,int width,int height)
        {
            this.x = x;
            this.y = y;
            this.width = width;
            this.height = height;
        }
    }
}
