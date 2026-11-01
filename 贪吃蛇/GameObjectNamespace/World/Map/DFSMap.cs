using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using 贪吃蛇.GameObjectNamespace.Block;
using 贪吃蛇.Main;

namespace 贪吃蛇.GameObjectNamespace.World.Map
{
    internal class DFSMap : BaseMap
    {
        public override void CreateMap()
        {
            var rng = new System.Random();
            int W = Game.LogicMapWidth;   // 40
            int H = Game.LogicMapHeight;  // 20
            GameObject[,] grids = new GameObject[W, H];

            // 1. 外围墙 + 四开口
            for (int col = 0; col < W; col++)
            {
                if (17 <= col && col <= 23) continue;
                grids[col, 0] = new Wall(col, 0);
                grids[col, H - 1] = new Wall(col, H - 1);
            }
            for (int row = 0; row < H; row++)
            {
                if (7 <= row && row <= 13) continue;
                grids[0, row] = new Wall(0, row);
                grids[W - 1, row] = new Wall(W - 1, row);
            }

            // 2. 内部全部通道
            for (int x = 1; x < W - 1; x++)
                for (int y = 1; y < H - 1; y++)
                    grids[x, y] = null;

            // ========== 矿脉生成 ==========
            bool[,] ore = new bool[W, H];

            // 2.1 撒种子：这次每个种子初始就是一小块（3x3 随机缺角），避免起点太瘦
            int seedCount = 10;
            var seeds = new List<(int x, int y)>();
            for (int i = 0; i < seedCount; i++)
            {
                int sx = rng.Next(4, W - 4);
                int sy = rng.Next(3, H - 3);
                seeds.Add((sx, sy));
                // 3x3 内随机填 5~8 格，形成小团块起点
                int fill = rng.Next(5, 9);
                var cells = new List<(int, int)>();
                for (int dx = -1; dx <= 1; dx++)
                    for (int dy = -1; dy <= 1; dy++)
                        if (InInner(sx + dx, sy + dy, W, H)) cells.Add((sx + dx, sy + dy));
                // 打乱后取前 fill 个
                for (int k = cells.Count - 1; k > 0; k--)
                {
                    int j = rng.Next(k + 1);
                    (cells[k], cells[j]) = (cells[j], cells[k]);
                }
                for (int k = 0; k < fill && k < cells.Count; k++)
                    ore[cells[k].Item1, cells[k].Item2] = true;
            }

            // 2.2 CA 生长：3 次迭代，让团块更圆润成块
            for (int iter = 0; iter < 3; iter++)
            {
                bool[,] next = (bool[,])ore.Clone();
                for (int x = 1; x < W - 1; x++)
                {
                    for (int y = 1; y < H - 1; y++)
                    {
                        int n = CountOre(ore, x, y, W, H);
                        if (ore[x, y])
                            next[x, y] = n >= 2;        // 矿：太孤单才退化（放宽，让矿更容易存活）
                        else
                            next[x, y] = n >= 4;        // 空：邻居够多才长
                    }
                }
                ore = next;
            }

            // 2.3 补洞：被矿包围的空腔填成矿
            FillHoles(ore, W, H);

            // 2.4 膨胀一次：把紧贴矿的空地变成矿，让团块更"厚"，去掉细线感
            Dilate(ore, W, H, rng, 0.5f);

            // 2.5 主干：在种子间连线，线改成"粗脉"（主线 + 两侧随机补格）
            int veinCount = 3;
            for (int i = 0; i < veinCount; i++)
            {
                var a = seeds[rng.Next(seeds.Count)];
                var b = seeds[rng.Next(seeds.Count)];
                CarveThickVein(ore, a, b, rng, W, H);
            }

            // 2.6 再过一次补洞，防止粗脉内部留空腔
            FillHoles(ore, W, H);

            // 2.7 实例化
            for (int x = 1; x < W - 1; x++)
                for (int y = 1; y < H - 1; y++)
                    if (ore[x, y])
                        grids[x, y] = new Wall(x, y);

            // 3. 强制清空四开口内侧
            for (int x = 17; x <= 23; x++)
            {
                grids[x, 1] = null;
                grids[x, H - 2] = null;
            }
            for (int y = 7; y <= 13; y++)
            {
                grids[1, y] = null;
                grids[W - 2, y] = null;
            }

            map = grids;
        }

        private static bool InInner(int x, int y, int W, int H)
            => x > 0 && x < W - 1 && y > 0 && y < H - 1;

        private static int CountOre(bool[,] ore, int x, int y, int W, int H)
        {
            int c = 0;
            for (int dx = -1; dx <= 1; dx++)
                for (int dy = -1; dy <= 1; dy++)
                {
                    if (dx == 0 && dy == 0) continue;
                    int nx = x + dx, ny = y + dy;
                    if (nx < 0 || nx >= W || ny < 0 || ny >= H) continue;
                    if (ore[nx, ny]) c++;
                }
            return c;
        }

        /// <summary>
        /// 补洞：如果一个空格的四邻域（上下左右）里至少有 3 个是矿，
        /// 或者 8 邻域里矿数 >= 6，就把它填成矿。多跑两遍填掉嵌套洞。
        /// </summary>
        private static void FillHoles(bool[,] ore, int W, int H)
        {
            for (int pass = 0; pass < 2; pass++)
            {
                bool changed = false;
                for (int x = 1; x < W - 1; x++)
                {
                    for (int y = 1; y < H - 1; y++)
                    {
                        if (ore[x, y]) continue;
                        int n4 = (ore[x - 1, y] ? 1 : 0) + (ore[x + 1, y] ? 1 : 0)
                               + (ore[x, y - 1] ? 1 : 0) + (ore[x, y + 1] ? 1 : 0);
                        int n8 = CountOre(ore, x, y, W, H);
                        if (n4 >= 3 || n8 >= 6)
                        {
                            ore[x, y] = true;
                            changed = true;
                        }
                    }
                }
                if (!changed) break;
            }
        }

        /// <summary>
        /// 膨胀：每个与矿相邻的空地，以概率 p 变成矿。
        /// 只膨胀一圈，避免把整张图糊住。
        /// </summary>
        private static void Dilate(bool[,] ore, int W, int H, System.Random rng, float p)
        {
            bool[,] add = new bool[W, H];
            for (int x = 1; x < W - 1; x++)
            {
                for (int y = 1; y < H - 1; y++)
                {
                    if (ore[x, y]) continue;
                    // 4 邻域里有矿才考虑膨胀（避免斜角膨胀让边缘太散）
                    bool adj = ore[x - 1, y] || ore[x + 1, y] || ore[x, y - 1] || ore[x, y + 1];
                    if (adj && rng.NextDouble() < p) add[x, y] = true;
                }
            }
            for (int x = 0; x < W; x++)
                for (int y = 0; y < H; y++)
                    if (add[x, y]) ore[x, y] = true;
        }

        /// <summary>
        /// 粗脉：随机游走，每步把当前格和它侧向的 1~2 格也设为矿，
        /// 形成 2~3 格宽的矿带，而不是 1 格细线。
        /// </summary>
        private static void CarveThickVein(bool[,] ore, (int x, int y) a, (int x, int y) b, System.Random rng, int W, int H)
        {
            int x = a.x, y = a.y;
            int guard = 0;
            while ((x != b.x || y != b.y) && guard++ < 500)
            {
                if (InInner(x, y, W, H)) ore[x, y] = true;

                // 侧向加粗：根据当前主方向，在垂直方向补 1~2 格
                // 先用简单策略：随机在 4 邻域补 1~2 格
                int thick = rng.Next(1, 3);
                for (int k = 0; k < thick; k++)
                {
                    int ox = 0, oy = 0;
                    if (rng.Next(2) == 0) ox = rng.Next(-1, 2);
                    else oy = rng.Next(-1, 2);
                    int nx = x + ox, ny = y + oy;
                    if (InInner(nx, ny, W, H)) ore[nx, ny] = true;
                }

                int dx = 0, dy = 0;
                if (rng.NextDouble() < 0.7)
                {
                    if (x != b.x) dx = Math.Sign(b.x - x);
                    else if (y != b.y) dy = Math.Sign(b.y - y);
                }
                else
                {
                    if (rng.Next(2) == 0) dx = rng.Next(-1, 2);
                    else dy = rng.Next(-1, 2);
                }
                x = Math.Clamp(x + dx, 1, W - 2);
                y = Math.Clamp(y + dy, 1, H - 2);
            }
        }
    }
}
