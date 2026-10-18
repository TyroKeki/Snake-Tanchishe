using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using 贪吃蛇.GameObject.Block;
using 贪吃蛇.GameObject.Entirety;
using 贪吃蛇.Main;

namespace 贪吃蛇.Menu
{
    class GameScene : ISceneUpdate
    {
        Map map;
        Snake snake;
        Food food;
        // 时间
        long nowTime = Game.sw.ElapsedMilliseconds;
        long lastTime = Game.sw.ElapsedMilliseconds;

        public GameScene()
        {
            map = new Map();
            snake = new Snake(Game.width/2,Game.height/2);
            food = new Food(snake);
        }

        public void Update()
        {
            nowTime = Game.sw.ElapsedMilliseconds;
            if (nowTime - lastTime >= 200)
            {
                lastTime = nowTime;

                snake.Move();
                if (snake.CheckIsEnd())
                {
                    Game.ChangeScene(E_SceneType.End);
                }
                snake.CheckEatFood(food);

                map.Draw();
                snake.Draw();
                food.Draw();
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
