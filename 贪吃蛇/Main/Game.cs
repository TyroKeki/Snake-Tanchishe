using System.Text;
using 贪吃蛇.Menu;
using System.Diagnostics;

namespace 贪吃蛇.Main
{
    enum E_SceneType
    {
        Start,
        InGame,
        End,
    }

    class Game
    {
        // 游戏窗口宽高
        public static int mapWidth;
        public static int mapHeight;
        // 新增的状态栏独立宽高
        public static int windowWidth;
        public static int windowHeight;
        // 当前选中的场景
        public static ISceneUpdate nowScene;
        // 全局计时
        public static Stopwatch timeFromInit = Stopwatch.StartNew();


        private void ConsoleInit()
        {
            Console.OutputEncoding = Encoding.UTF8;
            Console.CursorVisible = false;
            Console.SetWindowSize(windowWidth, windowHeight);
            Console.SetBufferSize(windowWidth, windowHeight);
            ChangeScene(E_SceneType.Start);
        }

        public Game()
        {
            ConsoleInit();
        }

        // 使用参数实例化
        public Game(int w,int h)
        {
            mapWidth = w;
            mapHeight = h;
            windowWidth = w + 16;
            windowHeight = h;
            ConsoleInit();
        }

        public void Start()
        {
            while (true)
            {
                // 似乎可以去掉?空引用判断
                nowScene.Update();
            }
        }

        public static void ChangeScene(E_SceneType type)
        {
            // 切换场景之前先擦除
            Console.Clear();

            switch (type)
            {
                case E_SceneType.Start:
                    nowScene = new BeginScene();
                    break;
                case E_SceneType.InGame:
                    nowScene = new GameScene();
                    break;
                case E_SceneType.End:
                    nowScene = new EndScene();
                    break;
                default:
                    break;
            }
        }
    }
}
