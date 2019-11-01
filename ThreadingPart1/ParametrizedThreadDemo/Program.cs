using System;
using System.Threading;

namespace ParametrizedThreadDemo
{
    internal class Program
    {
        private static void Main()
        {
            var t1 = new Thread(delegate () { Tick('.', 10); });
            var t2 = new Thread(delegate () { Tick('*', 20); });
            t1.Start();
            t2.Start();
            t1.Join();
            t2.Join();
            Console.WriteLine("Ende");
        }
        private static void Tick(char c, int intervall)
        {
            for (var i = 0; i < 100; i++)
            {
                Console.Write(c);
                Thread.Sleep(intervall);
            }
        }
    }
}
