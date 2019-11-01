using System;
using System.Threading;

namespace ThreadAbort
{
    internal class Program
    {
        private static void Main()
        {
            var t = new Thread(M);
            t.Start();
            Thread.Sleep(1000);
            t.Abort();
            t.Join();
            Console.WriteLine("done");
            Console.ReadLine();
        }
        private static void M()
        {
            try
            {
                while (true) { }
            }
            catch (ThreadAbortException)
            {
                Console.WriteLine("aborted");
            }
        }
    }
}
