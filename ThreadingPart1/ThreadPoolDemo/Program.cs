using System;
using System.Threading;

namespace ThreadPoolDemo
{
    delegate int AdditionDelegate(int number1, int number2);
    internal class Program
    {
        private static void Main()
        {
            AdditionDelegate del = DoWork;
            // Console.WriteLine($"Without begin invoke {del(10, 20)}");
            Console.WriteLine("Starting with BeginInvoke");
            var result = del.BeginInvoke(10,20, AddCallback, null);
            Console.WriteLine("Waiting on work...");
            var ret = del.EndInvoke(result);
            Console.WriteLine($"{ret}");
            Console.ReadLine();
        }

        private static int DoWork(int number1, int number2)
        {
            Thread.Sleep(2000);
            return number1 + number2;
        }
        private static void AddCallback(IAsyncResult result) => Console.WriteLine($"Callback {result.IsCompleted}");
    }
}
