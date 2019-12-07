using System;
using System.Threading;

namespace Task3
{
    internal class Program
    {
        private static readonly Buffer Buf = new Buffer(4);
        private static readonly Random Rand = new Random();
        private static void Main()
        {
            new Thread(Produce) { Name = "Producer1" }.Start();
            new Thread(Produce) { Name = "Producer2" }.Start();
            new Thread(Consume) { Name = "Consumer1" }.Start();
            new Thread(Consume) { Name = "Consumer2" }.Start();
            Console.ReadLine();
        }

        private static void Produce()
        {
            for (var i = 0; i < 5; i++)
            {
                Buf.Put('x');
                Thread.Sleep(Rand.Next(1000));
            }
        }
        private static void Consume()
        {
            for (var i = 0; i < 5; i++)
            {
                Buf.Get();
                Thread.Sleep(Rand.Next(100));
            }
        }
    }
}
