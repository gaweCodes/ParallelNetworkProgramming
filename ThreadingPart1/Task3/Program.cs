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
            var p1 = new Thread(Produce) { Name = "Producer1" };
            var p2 = new Thread(Produce) { Name = "Producer2" };
            var c1 = new Thread(Consume) { Name = "Consumer1" };
            var c2 = new Thread(Consume) { Name = "Consumer2" };
            p1.Start();
            p2.Start();
            c1.Start();
            c2.Start();
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
