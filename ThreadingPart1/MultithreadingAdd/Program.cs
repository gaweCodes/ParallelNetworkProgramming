using System;
using System.Threading;

namespace MultithreadingAdd
{
    internal class Program
    {
        private static int _sum;
        private static void Main()
        {
            var threads = new Thread[10];
            for (var n = 0; n < threads.Length; n++)
            {
                threads[n] = new Thread(AddOne);
                threads[n].Start();
            }

            foreach (var t in threads) t.Join();
            Console.WriteLine($"[{Thread.CurrentThread.ManagedThreadId}] sum = {_sum}");
            Console.ReadLine();
        }
#if (true)
        // Buggy version.
        private static void AddOne()
        {
            Console.WriteLine($"[{Thread.CurrentThread.ManagedThreadId}] AddOne called");
            var temp = _sum;
            temp++;
            Thread.Sleep(1);
            _sum = temp;
        }
#else
        // Thread-safe version.
        private static void AddOne() 
        {
            Console.WriteLine($"[{Thread.CurrentThread.ManagedThreadId}] AddOne called");
            Interlocked.Increment(ref sum);
        }
#endif
    }
}
