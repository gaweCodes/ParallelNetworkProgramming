using System;
using System.Threading;

namespace AppDomainDemo
{
    internal class Program
    {
        private static void Main(string[] args)
        { 
            CreateAndRunThread();
            var ad = AppDomain.CreateDomain("NewDomain");
            ad.DoCallBack(CreateAndRunThread);

            if (args.Length > 0)
                Console.WriteLine($"[{Thread.CurrentThread.ManagedThreadId}, {Thread.GetDomain().FriendlyName}] argument: {args[0]}");
            else
                ad.ExecuteAssembly("AppDomainDemo.exe", new[] { "test" }); //AppDomain.Unload(ad)
            Console.ReadLine();
        }
        private static void CreateAndRunThread()
        {
            Console.WriteLine($"[{Thread.CurrentThread.ManagedThreadId}, {Thread.GetDomain().FriendlyName}] Main called");
            Console.WriteLine($"[{Thread.CurrentThread.ManagedThreadId}, {Environment.ProcessorCount}] Processor/core count = {Thread.GetDomain().FriendlyName}");

            var t = new Thread(SayHello)
            {
                Name = "Hello Thread",
                Priority = ThreadPriority.BelowNormal
            };
            t.Start();
            //t.Join();

            Console.WriteLine("[{0}, {1}] Main done", Thread.CurrentThread.ManagedThreadId, Thread.GetDomain().FriendlyName);
        }

        private static void SayHello()
        {
            Console.WriteLine("[{0}, {1}] Hello, world!", Thread.CurrentThread.ManagedThreadId, Thread.GetDomain().FriendlyName);
            Thread.Sleep(10000);
            Console.WriteLine("[{0}, {1}] SayHello done", Thread.CurrentThread.ManagedThreadId, Thread.GetDomain().FriendlyName);
        }
    }
}
