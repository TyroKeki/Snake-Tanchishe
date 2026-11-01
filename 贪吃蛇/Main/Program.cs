namespace 贪吃蛇.Main
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Game game = new Game(80,20);
            game.Start();
        }
    }
}
