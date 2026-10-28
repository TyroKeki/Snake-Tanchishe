using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using 贪吃蛇.GameObjectSpace.Block;
using 贪吃蛇.GameObjectSpace.Entirety;
using 贪吃蛇.GameObjectSpace.World;
using 贪吃蛇.Main;
using 贪吃蛇.Toolkit;

namespace 贪吃蛇.Menu
{
    class GameScene : ISceneUpdate
    {
        // 这个是创建实例，用于持有当前地图指针并推送渲染
        WorldMap worldMap;

        Snake snake;
        Food food;
        // 时间
        Stopwatch roundTime = Stopwatch.StartNew();
        long nowTime;
        long lastTime;

        public GameScene()
        {
            worldMap = new WorldMap();

            snake = new Snake(Game.mapWidth/2,Game.mapHeight/2);
            food = new Food(snake);
            MapRenderer.Instance.ResetResolution();
            // 时间
            nowTime = roundTime.ElapsedMilliseconds;
            lastTime = roundTime.ElapsedMilliseconds;
        }

        private void LogicUpdate()
        {
            snake.Move();
            snake.CheckEatFood(food);
            worldMap.CheckChangeMap(snake);

            if (snake.CheckIsEnd(worldMap.NowMap))
            {
                Game.ChangeScene(E_SceneType.End);
            }
        }

        private void LateUpdate()
        {
            TimeUI.Instance.FetchTime(roundTime.Elapsed);

            MapToRenderer.Instance.ToRenderer(worldMap.NowMap);
            snake.ToRenderer();
            food.ToRenderer();
        }
        private void Render()
        {
            MapRenderer.Instance.Draw();
            TimeUI.Instance.Draw();
        }

        public void Update()
        {
            nowTime = roundTime.ElapsedMilliseconds;
            if (nowTime - lastTime >= 200)
            {
                lastTime = nowTime;
                LogicUpdate();
                LateUpdate();
                Render();
            }

            if (Console.KeyAvailable)
            {
                switch (Console.ReadKey(true).Key)
                {
                    case ConsoleKey.W:
                        snake.ChangeDir(E_Dir.up);
                        break;
                    case ConsoleKey.A:
                        snake.ChangeDir(E_Dir.left);
                        break;
                    case ConsoleKey.S:
                        snake.ChangeDir(E_Dir.down);
                        break;
                    case ConsoleKey.D:
                        snake.ChangeDir(E_Dir.right);
                        break;
                }
            }
        }
    }
}
