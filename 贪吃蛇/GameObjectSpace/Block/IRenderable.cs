using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 贪吃蛇.GameObjectSpace.Block
{
    /// <summary>
    /// 渲染图层序，数值小的先画（在下面），大的后画（盖上面）。
    /// 层间留空隙（10 的倍数）便于后续插入新层。
    /// TODO: 配合 IRenderable 使用，当前层序仍靠调用顺序，暂未接入总线排序。
    /// </summary>
    enum RenderLayer
    {
        Map = 0,      // 地图/墙，最底下
        Food = 10,    // 食物
        Snake = 20,   // 蛇，在上面
        UI = 30,      // 分数文字之类，最顶上
    }

    /// <summary>
    /// 渲染项统一契约，供 RenderOnScreen 按 Layer 排序输出。
    /// TODO: 当前层序靠 GameScene.Update 的调用顺序，暂未接入。
    ///       当图层增加到 4 个以上或层序需要动态调整时，让 SnakeBody/Food/Wall 实现本接口，
    ///       并改造 RenderOnScreen.Render() 按 Layer 排序。
    /// </summary>
    internal interface IRenderable
    {
        Position Pos { get; }
        RenderLayer Layer { get; }
        void GetRenderData(out char ch, out ConsoleColor color);  // 或者返回一个结构
    }
}
