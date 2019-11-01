using System;
using System.Threading;

namespace ThreadBackground
{
    internal class Program
    {
        private static void Main()
        {
            Console.WriteLine($"[{Thread.CurrentThread.ManagedThreadId}] Main called");
            var t = new Thread(SayHello);
            t.IsBackground = true;
            t.Start(10);
            Console.WriteLine($"[{Thread.CurrentThread.ManagedThreadId}] Main done");
        }
        private static void SayHello(object arg)
        {
            var iterations = (int)arg;
            for (var i = 0; i < iterations; i++)
                Console.WriteLine($"[{Thread.CurrentThread.ManagedThreadId}] Hello, world {i}! ({Thread.CurrentThread.IsBackground})");
        }
    }
}
