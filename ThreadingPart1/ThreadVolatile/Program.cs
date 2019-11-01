using System;
using System.Threading;

namespace ThreadVolatile
{
    internal class Program
    {
        private static void Main()
        {
            var status = new GameStatus {StopFlag = false};
            var gameLoop = new Thread(RunLoop);
            gameLoop.Start(status);
            Thread.Sleep(2000);
            status.StopFlag = true;
            Console.WriteLine("Stopflag has been set.");
            gameLoop.Join();
            Console.ReadLine();
        }
        private static void RunLoop(object obj)
        {
            GameStatus status = (GameStatus)obj;
            Console.WriteLine("Game loop starts now.");

            while (!status.StopFlag) { }
            Console.WriteLine("Game loop stopped.");
        }
    }
}
