using System;
using System.Threading;

namespace ThreadStorage
{
    internal class Program
    {
        [ThreadStatic]
        private static int _a;
        private static int _b;
        private static void Main()
        {
            for (var i = 0; i < 10; ++i)
            {
                var t = new Thread(DoWorkStatic);
                t.Start(i);
                t.Join();
            }
            Console.WriteLine($"Mainthread: {_a} - {_b}");
            Console.ReadKey();
            Console.WriteLine();
            using (var local = new ThreadLocal<int>(() => 10))
            {
                for (var i = 0; i < 10; ++i)
                {
                    var t = new Thread(DoWorkLocal);
                    t.Start(local);
                    t.Join();
                }

                Console.WriteLine($"Mainthread: {local.Value}");
                Console.ReadKey();
            }
        }
        private static void DoWorkStatic(object data)
        {
            for (var j = 0; j < 10; ++j)
            {
                _a++;
                _b++;
            }
            Console.WriteLine($"Thread #{data}: {_a} - {_b}");
        }
        private static void DoWorkLocal(object data)
        {
            var local = (ThreadLocal<int>)data;
            for (var j = 0; j < 10; ++j) local.Value++;
            Console.WriteLine($"Thread #{Thread.CurrentThread.ManagedThreadId}: {local.Value}");
        }
    }
}
