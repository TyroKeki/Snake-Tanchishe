using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using 贪吃蛇.Main;

namespace 贪吃蛇.Toolkit
{
    internal static class Collections
    {
        public static bool InBounds(int i, int j, int row, int col)
        {
            if (i < 0 || i >= row) return false;
            if (j < 0 || j >= col) return false;
            return true;
        }

    }
}
