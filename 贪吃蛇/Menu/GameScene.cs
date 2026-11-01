using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using 贪吃蛇.Data;
using 贪吃蛇.GameObjectNamespace.Block;
using 贪吃蛇.GameObjectNamespace.Entirety;
using 贪吃蛇.GameObjectNamespace.World;
using 贪吃蛇.GameObjectNamespace.World.Directors;
using 贪吃蛇.GameObjectNamespace.World.WorldComponents;
using 贪吃蛇.Main;
using 贪吃蛇.Toolkit;
using 贪吃蛇.UI;

namespace 贪吃蛇.Menu
{
    class GameScene : ISceneUpdate
    {
        WorldMap worldMap = new WorldMap();
        // 要snake信息，很烦
        public Snake snake = new Snake(Game.LogicMapWidth / 2, Game.LogicMapHeight / 2);
        private FoodDirector foodDrct = null;


        // 时间
        Stopwatch roundTime = Stopwatch.StartNew();
        long nowTime;
        long lastTime;
        // 数据存储
        PlayerData playerData;

        public GameScene()
        {

            foodDrct = new FoodDirector(snake, worldMap);
            MapRenderer.Instance.ResetResolution();
            // 时间
            nowTime = roundTime.ElapsedMilliseconds;
            lastTime = roundTime.ElapsedMilliseconds;
            // 数据
            playerData = new PlayerData();
        }

        private void LogicUpdate()
        {
            snake.Move();
            snake.CheckEatFood(foodDrct, playerData);

            if (worldMap.CheckIsEnd(worldMap.NowMap, snake))
            {
                Game.ChangeScene(E_SceneType.End);
            }
            // 需要放在CheckIsEnd下，非常隐式，未来要改
            worldMap.CheckEnterNewMap(snake);
        }

        private void LateUpdate()
        {
            MapToRenderer.ToRenderer(worldMap.NowMap);
            snake.ToRenderer();
            foodDrct.ToRenderer();

            TimeUI.Instance.FetchTime(roundTime.Elapsed);
            WorldPosUI.Instance.FetchWorldPos(worldMap.WorldX, worldMap.WorldY);
            ScoreUI.Instance.FetchScore(playerData.Score);
        }
        private void Render()
        {
            MapRenderer.Instance.Draw();
            TimeUI.Instance.Draw();
            ScoreUI.Instance.Draw();
            WorldPosUI.Instance.Draw();
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
