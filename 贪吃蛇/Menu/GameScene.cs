using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using 贪吃蛇.GameObjectSpace.Block;
using 贪吃蛇.GameObjectSpace.Entirety;
using 贪吃蛇.Main;
using 贪吃蛇.Toolkit;

namespace 贪吃蛇.Menu
{
    class GameScene : ISceneUpdate
    {
        // 组合设计，值得学习思考，也许未来可以放到Game里面
        // 通过组合持有地图包
        Maps maps = new Maps();
        // 这个是创建实例，用于持有当前地图指针并推送渲染
        CurrentMap map;

        Snake snake;
        Food food;
        // 时间
        Stopwatch roundTime = Stopwatch.StartNew();
        long nowTime;
        long lastTime;

        public GameScene()
        {
            map = new CurrentMap(maps, E_Map.startMap);

            snake = new Snake(Game.mapWidth/2,Game.mapHeight/2);
            food = new Food(snake);
            MapRenderer.Instance.ResetResolution();
            // 时间
            nowTime = roundTime.ElapsedMilliseconds;
            lastTime = roundTime.ElapsedMilliseconds;
        }

        public void Update()
        {
            nowTime = roundTime.ElapsedMilliseconds;
            if (nowTime - lastTime >= 200)
            {
                lastTime = nowTime;

                snake.Move();
                if (snake.CheckIsEnd(map.nowMap))
                {
                    Game.ChangeScene(E_SceneType.End);
                }
                snake.CheckEatFood(food);

                TimeUI.Instance.FetchTime(roundTime.Elapsed);

                map.ToRenderer();
                snake.ToRenderer();
                food.ToRenderer();

                MapRenderer.Instance.Draw();
                TimeUI.Instance.Draw();
            }

            Thread.Sleep(10);
            if (Console.KeyAvailable)
            {
                switch (Console.ReadKey(true).Key)
                {
                    case ConsoleKey.W:
                        snake.ChangeDir(E_MoveDir.up);
                        break;
                    case ConsoleKey.A:
                        snake.ChangeDir(E_MoveDir.left);
                        break;
                    case ConsoleKey.S:
                        snake.ChangeDir(E_MoveDir.down);
                        break;
                    case ConsoleKey.D:
                        snake.ChangeDir(E_MoveDir.right);
                        break;
                }
            }
        }
    }
}
