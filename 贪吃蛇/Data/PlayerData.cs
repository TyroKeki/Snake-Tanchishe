using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using 贪吃蛇.UI;

namespace 贪吃蛇.Data
{
    internal class PlayerData
    {
        private int _score;
        public int Score => _score;

        public void AddScore(int amount)
        {
            if (amount == 0) return;
            _score += amount;
            // 未来:OnScoreChanged?.Invoke(...)
        }
    }
}
