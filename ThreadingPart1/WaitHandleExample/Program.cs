using System;
using System.Threading;

namespace WaitHandleExample
{
    internal class Program
    {
        private static void Main()
        {
            var waitHandles = new WaitHandle[] {new AutoResetEvent(false), new AutoResetEvent(false)};
            ThreadPool.QueueUserWorkItem(delegate { PeriodTicker('.', 10, waitHandles[0]); });
            ThreadPool.QueueUserWorkItem(delegate { PeriodTicker('*', 20, waitHandles[1]); });
            WaitHandle.WaitAll(waitHandles);
            // Console.ReadLine();
            Console.WriteLine("Ende");
            Console.ReadLine();
        }

        public static void PeriodTicker(char sign, int intvervallMillis, WaitHandle waitHandle)
        {
            for (var i = 0; i < 100; i++)
            {
                Console.Write(sign);
                Thread.Sleep(intvervallMillis);
            }

            ((EventWaitHandle) waitHandle).Set();
        }

    }

    class ProgramImpl : Program
    {
    }
}
